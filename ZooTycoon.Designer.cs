namespace zoo_tycoon
{
    partial class ZooTycoon
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
            this.MainDrawingArea = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // MainDrawingArea
            // 
            this.MainDrawingArea.Location = new System.Drawing.Point(2, 2);
            this.MainDrawingArea.Margin = new System.Windows.Forms.Padding(2);
            this.MainDrawingArea.Name = "MainDrawingArea";
            this.MainDrawingArea.Size = new System.Drawing.Size(884, 409);
            this.MainDrawingArea.TabIndex = 5;
            this.MainDrawingArea.TabStop = false;
            this.MainDrawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainDrawingArea_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "THIS A TEST";
            // 
            // ZooTycoon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(887, 410);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainDrawingArea);
            this.Name = "ZooTycoon";
            this.Text = "Zoo Tycoon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZooTycoon_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainDrawingArea_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawingArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox MainDrawingArea;
        private System.Windows.Forms.Label label1;
    }
}

