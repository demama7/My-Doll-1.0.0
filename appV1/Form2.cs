using System;
using System.Drawing;
using System.Windows.Forms;

namespace appV1
{
    public partial class Form2 : Form
    {
        private Random rnd = new Random();
        private int stage = 0;

        // משתנים חדשים לספירה לאחור
        private int timeLeft = 15; 
        private int tickCounter = 0;

        public Form2()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Size = new Size(400, 250);
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;

            // אתחול הטיימר - נשאר מהיר בשביל הבריחה מהעכבר
            timerGame.Interval = 30;
            timerGame.Start();

            // אתחול טקסט התחלתי לטיימר (וודא שיש labelTimer ב-Designer)
            if (labelTimer != null)
            {
                labelTimer.Text = "Time Left: 60";
                labelTimer.ForeColor = Color.Lime; // צבע ירוק בהתחלה
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // פונקציה ריקה למניעת שגיאות
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            // 1. חישוב המרחק ובריחה מהעכבר (הקוד הקיים שלך)
            Point mousePos = Cursor.Position;
            Point formCenter = new Point(this.Left + (this.Width / 2), this.Top + (this.Height / 2));
            double distance = Math.Sqrt(Math.Pow(mousePos.X - formCenter.X, 2) + Math.Pow(mousePos.Y - formCenter.Y, 2));

            if (distance < 200)
            {
                int newX = rnd.Next(50, Screen.PrimaryScreen.Bounds.Width - this.Width - 50);
                int newY = rnd.Next(50, Screen.PrimaryScreen.Bounds.Height - this.Height - 50);
                this.Location = new Point(newX, newY);
                System.Media.SystemSounds.Asterisk.Play();
            }

            // 2. עדכון טקסטים קריפיים (כל 40 טיקים - בערך שנייה וחצי)
            stage++;
            if (stage % 40 == 0)
            {
                string[] scaryMessages = {
                    "אתה איטי מדי...",
                    "מערכת הקבצים נסרקת",
                    "המיקום שלך זוהה",
                    "אל תנסה לתפוס אותי",
                    "שולח נתונים לשרת..."
                };
                label1.Text = scaryMessages[rnd.Next(scaryMessages.Length)];
            }

            // 3. ניהול ספירה לאחור (לוגיקה של דקה)
            // מכיוון שהטיימר רץ כל 30ms, שנייה אחת היא בערך 33 טיקים
            tickCounter++;
            if (tickCounter >= 33)
            {
                timeLeft--;
                tickCounter = 0;

                if (labelTimer != null)
                {
                    labelTimer.Text = "Time Left: " + timeLeft;

                    // כשיש פחות מ-10 שניות, נהפוך את הטקסט לאדום מלחיץ
                    if (timeLeft <= 10)
                    {
                        labelTimer.ForeColor = Color.Red;
                        System.Media.SystemSounds.Beep.Play(); // צליל תקתוק בלחץ
                    }
                }
            }

            // 4. סיום המשחק כשהזמן נגמר
            if (timeLeft <= 0)
            {
                timerGame.Stop();
                this.DialogResult = DialogResult.No; // איתות ל-Form1 שהמשחק הסתיים בזמן
                this.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            timerGame.Stop();
            this.DialogResult = DialogResult.OK; // איתות ל-Form1 שהמשחק הסתיים בזמן
            this.Close();
        }
    }
}