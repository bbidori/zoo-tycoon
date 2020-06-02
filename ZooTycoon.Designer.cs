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
            this.stats = new System.Windows.Forms.Panel();
            this.labelGarbages = new System.Windows.Forms.Label();
            this.pbGarbages = new System.Windows.Forms.PictureBox();
            this.labelAnimals = new System.Windows.Forms.Label();
            this.pbAnimals = new System.Windows.Forms.PictureBox();
            this.labelMoney = new System.Windows.Forms.Label();
            this.pbMoney = new System.Windows.Forms.PictureBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.pbDate = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawingArea)).BeginInit();
            this.stats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGarbages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnimals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDate)).BeginInit();
            this.SuspendLayout();
            // 
            // MainDrawingArea
            // 
            this.MainDrawingArea.Location = new System.Drawing.Point(-1, -4);
            this.MainDrawingArea.Margin = new System.Windows.Forms.Padding(2);
            this.MainDrawingArea.Name = "MainDrawingArea";
            this.MainDrawingArea.Size = new System.Drawing.Size(1604, 913);
            this.MainDrawingArea.TabIndex = 5;
            this.MainDrawingArea.TabStop = false;
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
            this.stats.Location = new System.Drawing.Point(-1, -4);
            this.stats.Name = "stats";
            this.stats.Size = new System.Drawing.Size(474, 40);
            this.stats.TabIndex = 6;
            this.stats.Visible = false;
            // 
            // labelGarbages
            // 
            this.labelGarbages.AutoSize = true;
            this.labelGarbages.Font = new System.Drawing.Font("Ravie", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGarbages.Location = new System.Drawing.Point(403, 6);
            this.labelGarbages.Name = "labelGarbages";
            this.labelGarbages.Size = new System.Drawing.Size(47, 22);
            this.labelGarbages.TabIndex = 7;
            this.labelGarbages.Text = "gar";
            // 
            // pbGarbages
            // 
            this.pbGarbages.Image = global::zoo_tycoon.Properties.Resources.icon_garbage;
            this.pbGarbages.Location = new System.Drawing.Point(365, 3);
            this.pbGarbages.Name = "pbGarbages";
            this.pbGarbages.Size = new System.Drawing.Size(32, 32);
            this.pbGarbages.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGarbages.TabIndex = 6;
            this.pbGarbages.TabStop = false;
            // 
            // labelAnimals
            // 
            this.labelAnimals.AutoSize = true;
            this.labelAnimals.Font = new System.Drawing.Font("Ravie", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAnimals.Location = new System.Drawing.Point(289, 6);
            this.labelAnimals.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
            this.labelAnimals.Name = "labelAnimals";
            this.labelAnimals.Size = new System.Drawing.Size(43, 22);
            this.labelAnimals.TabIndex = 5;
            this.labelAnimals.Text = "ani";
            // 
            // pbAnimals
            // 
            this.pbAnimals.Image = global::zoo_tycoon.Properties.Resources.icon_paw;
            this.pbAnimals.Location = new System.Drawing.Point(251, 3);
            this.pbAnimals.Name = "pbAnimals";
            this.pbAnimals.Size = new System.Drawing.Size(32, 32);
            this.pbAnimals.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAnimals.TabIndex = 4;
            this.pbAnimals.TabStop = false;
            // 
            // labelMoney
            // 
            this.labelMoney.AutoSize = true;
            this.labelMoney.Font = new System.Drawing.Font("Ravie", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMoney.Location = new System.Drawing.Point(175, 6);
            this.labelMoney.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
            this.labelMoney.Name = "labelMoney";
            this.labelMoney.Size = new System.Drawing.Size(43, 22);
            this.labelMoney.TabIndex = 3;
            this.labelMoney.Text = "bal";
            // 
            // pbMoney
            // 
            this.pbMoney.Image = global::zoo_tycoon.Properties.Resources.icon_money;
            this.pbMoney.Location = new System.Drawing.Point(137, 3);
            this.pbMoney.Name = "pbMoney";
            this.pbMoney.Size = new System.Drawing.Size(32, 32);
            this.pbMoney.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMoney.TabIndex = 2;
            this.pbMoney.TabStop = false;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Ravie", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(41, 6);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(49, 22);
            this.labelDate.TabIndex = 1;
            this.labelDate.Text = "day";
            // 
            // pbDate
            // 
            this.pbDate.Image = global::zoo_tycoon.Properties.Resources.icon_calendar;
            this.pbDate.Location = new System.Drawing.Point(3, 3);
            this.pbDate.Name = "pbDate";
            this.pbDate.Size = new System.Drawing.Size(32, 32);
            this.pbDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDate.TabIndex = 0;
            this.pbDate.TabStop = false;
            // 
            // ZooTycoon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1604, 920);
            this.Controls.Add(this.stats);
            this.Controls.Add(this.MainDrawingArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ZooTycoon";
            this.Text = "Zoo Tycoon";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZooTycoon_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawingArea)).EndInit();
            this.stats.ResumeLayout(false);
            this.stats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGarbages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnimals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDate)).EndInit();
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

