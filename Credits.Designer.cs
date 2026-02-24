namespace appV1
{
    partial class Credits
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
            lblFadingText = new Label();
            SuspendLayout();
            // 
            // lblFadingText
            // 
            lblFadingText.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFadingText.ForeColor = Color.White;
            lblFadingText.Location = new Point(12, 9);
            lblFadingText.Name = "lblFadingText";
            lblFadingText.Size = new Size(776, 432);
            lblFadingText.TabIndex = 0;
            lblFadingText.Text = "label1";
            lblFadingText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Credits
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            Controls.Add(lblFadingText);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Credits";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Credits";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private Label lblFadingText;
    }
}