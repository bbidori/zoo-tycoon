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
            this.stats = new System.Windows.Forms.Panel();
            this.labelGarbages = new System.Windows.Forms.Label();
            this.labelAnimals = new System.Windows.Forms.Label();
            this.labelMoney = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.pbGarbages = new System.Windows.Forms.PictureBox();
            this.pbAnimals = new System.Windows.Forms.PictureBox();
            this.pbMoney = new System.Windows.Forms.PictureBox();
            this.pbDate = new System.Windows.Forms.PictureBox();
            this.MainDrawingArea = new System.Windows.Forms.PictureBox();
            this.stats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGarbages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnimals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // stats
            // 
            this.stats.Controls.Add(this.labelGarbages);
            this.stats.Controls.Add(this.pbGarbages);
            this.stats.Controls.Add(this.labelAnimals);
            this.stats.Controls.Add(this.pbAnimals);
            this.stats.Controls.Add(this.labelMoney);
            this.stats.Controls.Add(this.pbMoney);
            this.stats.Controls.Add(this.labelDate);
            this.stats.Controls.Add(this.pbDate);
            this.stats.Location = new System.Drawing.Point(-1, -5);
            this.stats.Margin = new System.Windows.Forms.Padding(4);
            this.stats.Name = "stats";
            this.stats.Size = new System.Drawing.Size(632, 49);
            this.stats.TabIndex = 6;
            this.stats.Visible = false;
            // 
            // labelGarbages
            // 
            this.labelGarbages.AutoSize = true;
            this.labelGarbages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGarbages.Location = new System.Drawing.Point(537, 7);
            this.labelGarbages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGarbages.Name = "labelGarbages";
            this.labelGarbages.Size = new System.Drawing.Size(40, 25);
            this.labelGarbages.TabIndex = 7;
            this.labelGarbages.Text = "gar";
            // 
            // labelAnimals
            // 
            this.labelAnimals.AutoSize = true;
            this.labelAnimals.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAnimals.Location = new System.Drawing.Point(385, 7);
            this.labelAnimals.Margin = new System.Windows.Forms.Padding(4, 0, 40, 0);
            this.labelAnimals.Name = "labelAnimals";
            this.labelAnimals.Size = new System.Drawing.Size(38, 25);
            this.labelAnimals.TabIndex = 5;
            this.labelAnimals.Text = "ani";
            // 
            // labelMoney
            // 
            this.labelMoney.AutoSize = true;
            this.labelMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMoney.Location = new System.Drawing.Point(233, 7);
            this.labelMoney.Margin = new System.Windows.Forms.Padding(4, 0, 40, 0);
            this.labelMoney.Name = "labelMoney";
            this.labelMoney.Size = new System.Drawing.Size(38, 25);
            this.labelMoney.TabIndex = 3;
            this.labelMoney.Text = "bal";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(55, 7);
            this.labelDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(44, 25);
            this.labelDate.TabIndex = 1;
            this.labelDate.Text = "day";
            // 
            // pbGarbages
            // 
            this.pbGarbages.Image = global::zoo_tycoon.Properties.Resources.garbage_icon;
            this.pbGarbages.Location = new System.Drawing.Point(487, 4);
            this.pbGarbages.Margin = new System.Windows.Forms.Padding(4);
            this.pbGarbages.Name = "pbGarbages";
            this.pbGarbages.Size = new System.Drawing.Size(43, 39);
            this.pbGarbages.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGarbages.TabIndex = 6;
            this.pbGarbages.TabStop = false;
            // 
            // pbAnimals
            // 
            this.pbAnimals.Image = global::zoo_tycoon.Properties.Resources.animal_icon;
            this.pbAnimals.Location = new System.Drawing.Point(335, 4);
            this.pbAnimals.Margin = new System.Windows.Forms.Padding(4);
            this.pbAnimals.Name = "pbAnimals";
            this.pbAnimals.Size = new System.Drawing.Size(43, 39);
            this.pbAnimals.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAnimals.TabIndex = 4;
            this.pbAnimals.TabStop = false;
            // 
            // pbMoney
            // 
            this.pbMoney.Image = global::zoo_tycoon.Properties.Resources.money_icon;
            this.pbMoney.Location = new System.Drawing.Point(183, 4);
            this.pbMoney.Margin = new System.Windows.Forms.Padding(4);
            this.pbMoney.Name = "pbMoney";
            this.pbMoney.Size = new System.Drawing.Size(43, 39);
            this.pbMoney.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMoney.TabIndex = 2;
            this.pbMoney.TabStop = false;
            // 
            // pbDate
            // 
            this.pbDate.Image = global::zoo_tycoon.Properties.Resources.icon_calendar;
            this.pbDate.Location = new System.Drawing.Point(4, 4);
            this.pbDate.Margin = new System.Windows.Forms.Padding(4);
            this.pbDate.Name = "pbDate";
            this.pbDate.Size = new System.Drawing.Size(43, 39);
            this.pbDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDate.TabIndex = 0;
            this.pbDate.TabStop = false;
            // 
            // MainDrawingArea
            // 
            this.MainDrawingArea.Location = new System.Drawing.Point(-1, 14);
            this.MainDrawingArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MainDrawingArea.Name = "MainDrawingArea";
            this.MainDrawingArea.Size = new System.Drawing.Size(2139, 1119);
            this.MainDrawingArea.TabIndex = 5;
            this.MainDrawingArea.TabStop = false;
            // 
            // ZooTycoon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1942, 1102);
            this.Controls.Add(this.stats);
            this.Controls.Add(this.MainDrawingArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ZooTycoon";
            this.Text = "Zoo Tycoon";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZooTycoon_FormClosing);
            this.stats.ResumeLayout(false);
            this.stats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGarbages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnimals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawingArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox MainDrawingArea;
        private System.Windows.Forms.Panel stats;
        private System.Windows.Forms.Label labelGarbages;
        private System.Windows.Forms.PictureBox pbGarbages;
        private System.Windows.Forms.Label labelAnimals;
        private System.Windows.Forms.PictureBox pbAnimals;
        private System.Windows.Forms.Label labelMoney;
        private System.Windows.Forms.PictureBox pbMoney;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.PictureBox pbDate;
    }
}

