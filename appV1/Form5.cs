using System;
using System.Drawing;
using System.Windows.Forms;

namespace appV1
{
    public partial class Form5 : Form
    {
        string[] questions = {
            "האם במשפט הזה יש את האות מספר 8 ב-אב ?",
            "אם אבי אומר שיוסי משקר ויוסי אומר שאבי משקר וקובי אומר שיוסי משקר האם אבי אומר את האמת?",
            "האם אחד לעד שלוש כפול 2 פחות 4 שווה 22?",
            "יוסי מהשאלה הקודמת אומר שהתשובה לשאלה הקודמת היא 22?",
            "האם זה שאלה מספר 4?",
            "האם כשאני כותב את זה אני אוכל טוסט ?",          
            "סברס, סכין, סלון, שקית האם סברס הוא יוצא הדופן מכולם?",
            "האם זה שאלה מספר 8 אם מתחילים לספור מ0?",
            "האם כמות הפעמים שאמרתי 8 היא 4?",
            "1 + 1 * 4 * 8 ÷ 2 + 7 = 26?"
        };

        bool[] correctAnswers = { false, true, true, false,false,true,false,true,false,false };
        int currentQuestion = 0;

        public Form5()
        {
            InitializeComponent();
        }

        
        private void Form5_Load(object sender, EventArgs e)
        {
            currentQuestion = 0;
            DisplayQuestion();
        }

        private void DisplayQuestion()
        {
            if (currentQuestion < questions.Length)
            {
                lblQuestion.Text = questions[currentQuestion];
                lblQuestion.ForeColor = Color.Red;
            }
            else
            {
                ScaryMessages.Show("סיימת את החידון... בינתיים.");
                ScaryMessages.Show("אבל יש לך עוד דברים לעשות בהצלחה לך...");
                this.Close();
            }
        }

        private void CheckAnswer(bool userChoseYes)
        {
            if (userChoseYes == correctAnswers[currentQuestion])
            {
                lblQuestion.Text = "נכון...";
                lblQuestion.ForeColor = Color.Green;
            }
            else
            {
                ShakeForm();
                ScaryMessages.disqualifications++;
                ScaryMessages.endGames();
            }

            currentQuestion++;

            
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 1000;
            t.Tick += (s, ev) => {
                t.Stop();
                DisplayQuestion();
                t.Dispose();
            };
            t.Start();
        }

        private void ShakeForm()
        {
            var originalLocation = this.Location;
            var rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                this.Location = new Point(originalLocation.X + rnd.Next(-10, 11), originalLocation.Y + rnd.Next(-10, 11));
                System.Threading.Thread.Sleep(20);
            }
            this.Location = originalLocation;
        }

        private void btnYes_Click_1(object sender, EventArgs e)
        {
            CheckAnswer(true);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            CheckAnswer(false);
        }
    }
}