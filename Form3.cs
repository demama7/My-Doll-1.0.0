using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appV1
{
    public partial class Form3 : Form
    {
        public string UserName { get; private set; }
        public Form3()
        {
            InitializeComponent();

            // 1. אומרים לחלון להתעלם מברירת המחדל של ווינדוס
            this.StartPosition = FormStartPosition.Manual;

            // 2. קובעים את המיקום לפינה השמאלית העליונה (0,0)
            this.Location = new Point(0, 0);
        }


        private void textUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserName = textUserName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
