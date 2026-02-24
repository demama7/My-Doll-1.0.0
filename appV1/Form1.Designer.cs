namespace appV1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            btnNextGame = new Button();
            input1 = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Yu Gothic UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(686, 474);
            label1.TabIndex = 0;
            label1.Text = "המשחק מתחיל!";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // btnNextGame
            // 
            btnNextGame.BackColor = SystemColors.ActiveCaptionText;
            btnNextGame.Font = new Font("Segoe UI Historic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNextGame.ForeColor = Color.Red;
            btnNextGame.Location = new Point(242, 418);
            btnNextGame.Name = "btnNextGame";
            btnNextGame.Size = new Size(197, 44);
            btnNextGame.TabIndex = 1;
            btnNextGame.Text = "למשחק הבא";
            btnNextGame.UseVisualStyleBackColor = false;
            btnNextGame.Visible = false;
            btnNextGame.Click += btnNextGame_Click;
            // 
            // input1
            // 
            input1.Location = new Point(220, 334);
            input1.Name = "input1";
            input1.Size = new Size(237, 23);
            input1.TabIndex = 2;
            input1.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.scery;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(686, 474);
            Controls.Add(input1);
            Controls.Add(btnNextGame);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            Shown += Form1_Shown;
            KeyDown += Form1_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private System.Windows.Forms.Timer timer1;
        private Button btnNextGame;
        private TextBox input1;
    }
}
