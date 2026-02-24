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

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
