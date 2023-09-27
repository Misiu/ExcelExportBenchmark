namespace WinformsExport
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
            button1 = new Button();
            button2 = new Button();
            sheetsNud = new NumericUpDown();
            rowsNud = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)sheetsNud).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rowsNud).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 41);
            button1.Name = "button1";
            button1.Size = new Size(103, 23);
            button1.TabIndex = 0;
            button1.Text = "XlsIO";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(121, 41);
            button2.Name = "button2";
            button2.Size = new Size(122, 23);
            button2.TabIndex = 1;
            button2.Text = "SpreadCheetah";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // sheetsNud
            // 
            sheetsNud.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            sheetsNud.Location = new Point(56, 12);
            sheetsNud.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            sheetsNud.Name = "sheetsNud";
            sheetsNud.Size = new Size(64, 23);
            sheetsNud.TabIndex = 2;
            sheetsNud.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // rowsNud
            // 
            rowsNud.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            rowsNud.Location = new Point(179, 12);
            rowsNud.Maximum = new decimal(new int[] { 600000, 0, 0, 0 });
            rowsNud.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            rowsNud.Name = "rowsNud";
            rowsNud.Size = new Size(64, 23);
            rowsNud.TabIndex = 3;
            rowsNud.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(135, 14);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "Rows:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 14);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 5;
            label2.Text = "Sheets:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(256, 80);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(rowsNud);
            Controls.Add(sheetsNud);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)sheetsNud).EndInit();
            ((System.ComponentModel.ISupportInitialize)rowsNud).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private NumericUpDown sheetsNud;
        private NumericUpDown rowsNud;
        private Label label1;
        private Label label2;
    }
}