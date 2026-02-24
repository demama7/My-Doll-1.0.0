using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appV1
{
    public partial class FormJumpscare : Form
    {
        public FormJumpscare()
        {
            InitializeComponent();

            // השמעת צרחה מיד עם פתיחת החלון
            try
            {
                SoundPlayer scream = new SoundPlayer(Properties.Resources.scaryScream); // וודא שיש לך קובץ כזה ב-Resources
                scream.Play();
            }
            catch { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
