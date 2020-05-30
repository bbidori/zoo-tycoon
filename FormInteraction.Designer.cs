namespace zoo_tycoon
{
    partial class FormInteraction
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
            this.Option1 = new System.Windows.Forms.Button();
            this.Option2 = new System.Windows.Forms.Button();
            this.Option3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Option1
            // 
            this.Option1.BackColor = System.Drawing.Color.Transparent;
            this.Option1.Location = new System.Drawing.Point(12, 12);
            this.Option1.Name = "Option1";
            this.Option1.Size = new System.Drawing.Size(135, 45);
            this.Option1.TabIndex = 0;
            this.Option1.Text = "Items";
            this.Option1.UseVisualStyleBackColor = false;
            // 
            // Option2
            // 
            this.Option2.BackColor = System.Drawing.Color.Transparent;
            this.Option2.Location = new System.Drawing.Point(12, 63);
            this.Option2.Name = "Option2";
            this.Option2.Size = new System.Drawing.Size(135, 44);
            this.Option2.TabIndex = 1;
            this.Option2.Text = "Status";
            this.Option2.UseVisualStyleBackColor = false;
            // 
            // Option3
            // 
            this.Option3.BackColor = System.Drawing.Color.Transparent;
            this.Option3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Option3.Location = new System.Drawing.Point(12, 113);
            this.Option3.Name = "Option3";
            this.Option3.Size = new System.Drawing.Size(135, 44);
            this.Option3.TabIndex = 2;
            this.Option3.Text = "Exit";
            this.Option3.UseVisualStyleBackColor = true;
            // 
            // FormInteraction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(161, 179);
            this.ControlBox = false;
            this.Controls.Add(this.Option3);
            this.Controls.Add(this.Option2);
            this.Controls.Add(this.Option1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "FormInteraction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormInteraction";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button Option1;
        public System.Windows.Forms.Button Option2;
        public System.Windows.Forms.Button Option3;
    }
}