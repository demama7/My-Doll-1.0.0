using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace appV1
{
    public partial class Form4 : Form
    {
        // --- מערכת ליבה ---
        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        private Random rand = new Random();
        private int score = 0;
        private const int WINNING_SCORE = 30; // יעד הניצחון

        // --- אובייקטים ---
        private List<PointF> topTriangles = new List<PointF>();
        private List<PointF> bottomTriangles = new List<PointF>();
        private List<RectangleF> greenCircles = new List<RectangleF>();

        // --- פרמטרים של קושי ---
        private float triangleSpeed = 2f;    // מהירות התחלתית איטית
        private int ticksCounter = 0;         // מונה זמן כללי
        private int trianglesCreated = 0;     // סך הכל משולשים שנוצרו
        private int maxTriangles = 700;       // הגבלה ל-700 זוגות
        private int ticksUntilNextCircle = 0; // מונה להופעת עיגולים
        private System.Media.SoundPlayer collectPlayer;
        private System.Media.SoundPlayer losePlayer;

        public Form4()
        {
            InitializeComponent();

            this.KeyPreview = true; 
            this.KeyDown += Form4_KeyDown;
            // הגדרות חלון למסך מלא
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true; // מניעת ריצודים בציור

            try
            {
                collectPlayer = new System.Media.SoundPlayer(Properties.Resources.Blip);
                losePlayer = new System.Media.SoundPlayer(Properties.Resources.Hit);
                // טעינה מוקדמת לזיכרון (Pre-load)
                collectPlayer.Load();
                losePlayer.Load();
            }
            catch { /* אם הקבצים לא נמצאו, התוכנה לא תקרוס */ }
            // הגדרת הטיימר המרכזי (30ms)
            gameTimer.Interval = 30;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // חיבור אירוע הציור
            this.Paint += Form4_Paint;

            // הגרלת זמן לעיגול הראשון
            SetNextCircleTime();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void SetNextCircleTime()
        {
            // הגדלת תדירות: בין 15 ל-90 טיקים (בערך 0.5 עד 2.7 שניות)
            ticksUntilNextCircle = rand.Next(15, 90);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            ticksCounter++;
            Point mousePos = this.PointToClient(Cursor.Position);

            // האצת מהירות הדרגתית כל 3 שניות בערך
            if (ticksCounter % 100 == 0 && triangleSpeed < 30)
            {
                triangleSpeed += 0.7f;
            }

            // 1. ניהול משולשים ובדיקת פסילה
            UpdateTrianglesAndCheckCollision(mousePos);

            // 2. ניהול עיגולים ירוקים (יצירה מוגברת)
            HandleGreenCircles(mousePos);

            // 3. בדיקת תנאי ניצחון
            if (score >= WINNING_SCORE)
            {
                GameWin();
            }
            // רענון המסך
            this.Invalidate();
        }
        //דלת סגירה סודית בבדיקת המערכת (לא לשכוח למחוק את זה נועה) למקרה הצורך
        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.W)
            {
                MessageBox.Show("דלת יציאה סודית!");
                GameWin();
                e.SuppressKeyPress = true;
            }
        }

        private void UpdateTrianglesAndCheckCollision(Point mousePos)
        {
            // יצירת משולשים עד למקסימום של 700
            if (trianglesCreated < maxTriangles)
            {
                int spawnThreshold = 95 - (int)(triangleSpeed * 2);
                if (spawnThreshold < 70) spawnThreshold = 70;

                if (rand.Next(0, 100) > spawnThreshold)
                {
                    topTriangles.Add(new PointF(rand.Next(0, this.Width), -50));
                    bottomTriangles.Add(new PointF(rand.Next(0, this.Width), this.Height + 50));
                    trianglesCreated++;
                }
            }

            // תנועה ובדיקת התנגשות - משולשים עליונים
            for (int i = topTriangles.Count - 1; i >= 0; i--)
            {
                PointF p = topTriangles[i];
                p.Y += triangleSpeed;
                topTriangles[i] = p;

                if (CalculateDistance(mousePos, new PointF(p.X, p.Y + 15)) < 22)
                {

                    GameOver();
                    return;
                }
                if (p.Y > this.Height + 50) topTriangles.RemoveAt(i);
            }

            // תנועה ובדיקת התנגשות - משולשים תחתונים
            for (int i = bottomTriangles.Count - 1; i >= 0; i--)
            {
                PointF p = bottomTriangles[i];
                p.Y -= triangleSpeed;
                bottomTriangles[i] = p;

                if (CalculateDistance(mousePos, new PointF(p.X, p.Y - 15)) < 22)
                {
                    
                    GameOver();
                    return;
                }
                if (p.Y < -50) bottomTriangles.RemoveAt(i);
            }
        }

        private void HandleGreenCircles(Point mousePos)
        {
            ticksUntilNextCircle--;
            if (ticksUntilNextCircle <= 0)
            {
                int circlesToSpawn = rand.Next(1, 4);
                for (int i = 0; i < circlesToSpawn; i++)
                {
                    float size = rand.Next(40, 75);
                    float x = rand.Next(50, this.Width - 50);
                    float y = rand.Next(50, this.Height - 50);
                    greenCircles.Add(new RectangleF(x, y, size, size));
                }
                SetNextCircleTime();
            }

            for (int i = greenCircles.Count - 1; i >= 0; i--)
            {
                if (greenCircles[i].Contains(mousePos))
                {
                    greenCircles.RemoveAt(i);
                    score++;
                    System.Threading.Tasks.Task.Run(() => collectPlayer.Play());
                }
            }
        }

        private double CalculateDistance(Point p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private void GameOver()
        {
            gameTimer.Stop();
            using (FormJumpscare jumpscareWindow = new FormJumpscare())
            {
                jumpscareWindow.ShowDialog();
            }
            System.Threading.Tasks.Task.Run(() => losePlayer.Play());
            MessageBox.Show("הפסדת! נגעת במשולש אדום.\nהניקוד שלך: " + score,
                            "System Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void GameWin()
        {
            gameTimer.Stop();
            MessageBox.Show($"כל הכבוד! הגעת ל-{WINNING_SCORE} נקודות וניצחת!",
                            "Access Granted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK; // מחזיר אישור לחלון הראשי
            this.Close();
        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // ציור משולשים אדומים
            using (SolidBrush redBrush = new SolidBrush(Color.Red))
            {
                foreach (PointF p in topTriangles)
                {
                    PointF[] pts = { new PointF(p.X, p.Y + 30), new PointF(p.X - 15, p.Y), new PointF(p.X + 15, p.Y) };
                    g.FillPolygon(redBrush, pts);
                }
                foreach (PointF p in bottomTriangles)
                {
                    PointF[] pts = { new PointF(p.X, p.Y - 30), new PointF(p.X - 15, p.Y), new PointF(p.X + 15, p.Y) };
                    g.FillPolygon(redBrush, pts);
                }
            }

            // ציור עיגולים ירוקים
            using (SolidBrush greenBrush = new SolidBrush(Color.LimeGreen))
            {
                foreach (RectangleF circle in greenCircles)
                {
                    g.FillEllipse(greenBrush, circle);
                }
            }

            // תצוגת נתוני משחק
            using (Font infoFont = new Font("Arial", 20, FontStyle.Bold))
            {
                string info = $"Score: {score}/{WINNING_SCORE} | Speed: {triangleSpeed:0.0}";
                g.DrawString(info, infoFont, Brushes.Black, 22, 22);
                g.DrawString(info, infoFont, Brushes.White, 20, 20);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            gameTimer.Stop();
            gameTimer.Dispose();
            base.OnFormClosing(e);
        }
    }
}