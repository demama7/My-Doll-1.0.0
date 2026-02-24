namespace appV1
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnYes = new Button();
            btnNo = new Button();
            lblQuestion = new Label();
            SuspendLayout();
            // 
            // btnYes
            // 
            btnYes.BackColor = Color.OrangeRed;
            btnYes.Location = new Point(519, 387);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(75, 23);
            btnYes.TabIndex = 0;
            btnYes.Text = "כן";
            btnYes.UseVisualStyleBackColor = false;
            btnYes.Click += btnYes_Click_1;
            // 
            // btnNo
            // 
            btnNo.BackColor = Color.OrangeRed;
            btnNo.Location = new Point(259, 387);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(75, 23);
            btnNo.TabIndex = 1;
            btnNo.Text = "לא";
            btnNo.UseVisualStyleBackColor = false;
            btnNo.Click += btnNo_Click;
            // 
            // lblQuestion
            // 
            lblQuestion.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuestion.ForeColor = Color.Red;
            lblQuestion.Location = new Point(12, 9);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(823, 363);
            lblQuestion.TabIndex = 2;
            lblQuestion.Text = "שאלות";
            lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(847, 477);
            Controls.Add(lblQuestion);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form5";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form5";
            TopMost = true;
            Load += Form5_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnYes;
        private Button btnNo;
        private Label lblQuestion;
    }
}