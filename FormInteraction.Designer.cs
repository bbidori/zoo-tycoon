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
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Thistle;
            this.button1.BackgroundImage = global::zoo_tycoon.Properties.Resources.text12;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(67, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(275, 63);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Thistle;
            this.button3.BackgroundImage = global::zoo_tycoon.Properties.Resources.fermer;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.button3.Location = new System.Drawing.Point(139, 389);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 28);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Thistle;
            this.button2.BackgroundImage = global::zoo_tycoon.Properties.Resources.text21;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Thistle;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(67, 243);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(275, 63);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // FormInteraction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.BackgroundImage = global::zoo_tycoon.Properties.Resources.interaction;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(157, 170);
            this.ClientSize = new System.Drawing.Size(403, 465);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormInteraction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button3;

    }
}