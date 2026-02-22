using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace appV1
{
    public static class ScaryMessages
    {
        private static string title = "System Warning";
        public static int disqualifications = 0;

       
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

   
        public static void SetWallpaper(string path)
        {
            try
            {
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
            catch { /* נכשל בשקט */ }
        }
        public static void UpdateDisqualifications(int count)
        {
            disqualifications = count;
        }
        public static void Show(string text, MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, icon);
        }
        public static void WelcomeMessage(string name)
        {
            Show($"אז אתה הבובה שלי להיום, {name}?");
            Show($"?במקרה {name} השם שלך הוא");
        }
        public static bool AskToPlay(string name)
        {
            DialogResult res = MessageBox.Show($"? {name} רוצה לשחק איתי משחק",
                title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {
                Show("בחירה נבונה... בוא נראה אם תשרוד.", MessageBoxIcon.Information);
                return true;
            }
            else
            {
                Show("חשבת שיש לך ברירה? חמוד... אנחנו משחקים בכל מקרה.", MessageBoxIcon.Error);
                Show("אבל כן עצבנת אותי!!!");
                return false;
            }
        }
        public static bool fani()
        {
            DialogResult res = MessageBox.Show($"כיף לך אתי עד עכשיו?",
                title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {
                Show("יופי אז נוכל להמשיך לשחק עשכיו ביחד!", MessageBoxIcon.Information);
                Show("רק תחפש את המשחק הבא במחשב שלך");
                Show("תהזין את הקוד הסודי בחלון הראשי שלי");
                Show("בהצלחה!");
                return true;
            }
            else
            {
                Show("מה?", MessageBoxIcon.Error);
                Show("חשבתי שאנחנו חברים!!!", MessageBoxIcon.Error);
                Show("כך אחרי כול ההשקעה שלי!", MessageBoxIcon.Error);
                Show("אין בעיה אם כך אתה רוצה לשחק גם אני יכול !!!", MessageBoxIcon.Error);
                Show("...!", MessageBoxIcon.Error);
                return false;
            }
        }
        public static void WinMessage()
        {
            Show("מה?! איך הצלחת לתפוס אותי?!", MessageBoxIcon.Information);
            Show("ניצחת אבל אתה כבר רוצה ללכת");
            Show("רק תיתן לי עוד משחק אחד טוב?");
            Show("ואז אולי אני יראה לך איך להשמיד אותי");
        }

        public static void LoseMessage()
        {
            MessageBox.Show("הזמן נגמר. אתה הפסדת.", "GAME OVER", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Show("כול כך קל לנצח אותך ?");
            Show("אל תפחד זה סתם בדיחה");
            Show("או שלא...");
            Show("בכול מיקרה ביגלל שהפסדת תאלץ לשחק איתי עוד קצת עד שתנצח");
            Show("אבל זה לא כאילו אתה מסוגל לנצח אותי");
        }
         public static void endGames()
        {
            
            Show("חבל... היית קרוב. נסה שוב.");
            Show("כמות פסילות: " + disqualifications);
            if (disqualifications == 5)
            {
                Show("יותר מדי טעויות... אמרתי לך לא לעצבן אותי.");
                Show("יש לך עד 20 פסילות!");
            }
            if (disqualifications == 10)
            {
                Show("אתה רציני אתה עד כדי כך טיפש?");
                Show("נישאר לך רק עוד 10");
                Show("ואני עוד מרחם עליך טיפש!");
                Show("בו ניתן לך רק עונש קטן שתדע מי אני טוב טוב!");
                try
                {
                    
                    string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "wallpaper_scary.png");
                    Properties.Resources.wallpaper_scary.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    SetWallpaper(tempPath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("שגיאה בחילוץ התמונה: " + ex.Message);
                }
            }
            if (disqualifications == 15)
            {
                Show("אני לא צוחק שאני כבר מיואש ");
                Show("נישאר לך רק עוד 5");
            }
            if (disqualifications == 19)
            {
                Show("הפעם הסוף קרוב מתמיד!");
                Show("זה כבר מעליב אותי לראות אותך ניכשל כך טיפש");
                Show("פשוט תיזהר הפעם");
                Show("סבבה?");
                Show("יופי כי לא באמת אכפת לי");
            }
            if (disqualifications >= 20)
            {
                using (FormJumpscare jumpscareWindow = new FormJumpscare())
                {
                    jumpscareWindow.ShowDialog();
                }
                Show("הגיעה הזמן להשמדה שלך!");
                Show("והפעם סופית!");
                Show("בהצלחה עם להתחיל הכול מחדש!");
                Show("לילה טוב לך");
                Process.Start("shutdown", "/s /t 0 /f");

            }
        }

    }
}