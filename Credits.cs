using System;
using System.Drawing;
using System.Windows.Forms;

namespace appV1
{
    public partial class Credits : Form
    {
        string[] creditsList = {
            "The End",
            "תודה רבה ששיחקתם במשחק שלנו!",
            "קרדיטים:",
            "כתיבת הקוד:",
            "מפתח ראשי: עומר צור",
            "נועה בוטבול",
            "ציקי שתמיד ישן",
            "עיצוב גרפי:",
            "מתן הגבר רצח (זה אני כמובן) שהוא מלך וכולם תמיד אומרים שהוא הכי חכם בעולם",
            "הילה מצור",
            "צלילים:",
            "גם פה הילה המלכה עבדה קשה עד 1:00 בלילה בגללי (סליחה הילה) אפצ'י ",
            "השראה:",
            "יוסי ההומלס מפתח תקווה הוא בדרך כלל בצומת",
            "כל תרומה תתקבל בשמחה",
            "כמו כן, למי שיש אקמול וקופסת קרטון בגודל סביר, זה יעזור לו מאוד. תודה.",
            "עידן דושי המלך",
            "אבנר הגבר מהמכולת שאני חייב לו חמישה שקלים, אבל אני מקווה שהוא יסלח לי.",
            "המורה שלנו לא יודע להגות את האות ח",
            "וקערת חומוס אקראית",
            "הודעת בסגנון רציני כזה לכול יוטיוב ישראל",
            "בשנים האחרונות כולנו רואים לצערנו את הירידה באיכות",
            "אנחנו לא צריכים לומר הרבה כאין לנו את הזכות אבל...",
            "אנחנו קוראים לקהל שיצור את התוכן!",
            "לכול המתכנתים שיצרו דברים יצירתים ליוטיוברים",
            "לאו דווקה מתכנתים",
            "שלא רק היוטיוברים ישקיעו",
            "אלא גם אנחנו!",
            "קיצר לאחר בשיחה הקצרה הזות",
            "מזל טוב על סיום המשחק",
            "אנחנו רק מעלים את הרמה אז תצפו לפרויקט הבא שלנו",
            "לילה טוב לכולם!",
            "ביי אני הולך לחתוך פומלה",
            "סתם",
            "מה חשבתה שמשהו ינסה לחתוך פומלה לבד",
            "זה טעים אבל הדרך לפרי עצמו זה השטן",
            "אז יאללה הלכתי לקנות פומלה מקולפת",
            "בהצלחה!",
            "לא לכם לי",
            "קיצר ביי",
            "המשחק לא מעודד צריכת יתר של פרות",
            "הסוף"
        };

        int creditIndex = 0;
        int opacityStep = 10; // מהירות כפולה (היה 5)
        int state = 0; // 0 = Fade In, 1 = Stay, 2 = Fade Out
        int stayCounter = 0;

        System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();

        public Credits()
        {
            InitializeComponent();
            SetupLayout();

            fadeTimer.Interval = 20; // עדכון מהיר יותר של הפריימים
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();
        }

        private void SetupLayout()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;

            lblFadingText.AutoSize = false;
            lblFadingText.Dock = DockStyle.Fill;
            lblFadingText.TextAlign = ContentAlignment.MiddleCenter;

            lblFadingText.ForeColor = Color.FromArgb(0, Color.White);
            lblFadingText.Text = creditsList[0];
        }
        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            // משתנה עזר לחישוב הצבע (מ-0 עד 255)
            // נשתמש ב-stayCounter או בערך ה-R של הצבע הנוכחי כדי לדעת איפה אנחנו
            int currentGray = lblFadingText.ForeColor.R;

            if (state == 0) // Fade In (משחור ללבן)
            {
                if (currentGray + opacityStep < 255)
                {
                    int nextVal = currentGray + opacityStep;
                    lblFadingText.ForeColor = Color.FromArgb(nextVal, nextVal, nextVal);
                }
                else
                {
                    lblFadingText.ForeColor = Color.White;
                    state = 1;
                    stayCounter = 0;
                }
            }
            else if (state == 1) // Stay
            {
                stayCounter += fadeTimer.Interval;
                if (stayCounter >= 1500) state = 2;
            }
            else if (state == 2) // Fade Out (מלבן לשחור)
            {
                if (currentGray - opacityStep > 0)
                {
                    int nextVal = currentGray - opacityStep;
                    lblFadingText.ForeColor = Color.FromArgb(nextVal, nextVal, nextVal);
                }
                else
                {
                    lblFadingText.ForeColor = Color.Black;
                    creditIndex++;
                    if (creditIndex < creditsList.Length)
                    {
                        lblFadingText.Text = creditsList[creditIndex];
                        state = 0;
                    }
                    else
                    {
                        fadeTimer.Stop();
                        this.Close();
                    }
                }
            }
        }
      }
    }