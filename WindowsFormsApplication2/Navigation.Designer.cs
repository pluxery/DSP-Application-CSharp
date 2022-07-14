namespace WindowsApp
{
    partial class Navigation
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.осцилограммаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dPFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.корреляционныйАнализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.спектральныйАнализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.спектограммаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.осцилограммаToolStripMenuItem, this.dPFToolStripMenuItem, this.корреляционныйАнализToolStripMenuItem, this.спектральныйАнализToolStripMenuItem, this.спектограммаToolStripMenuItem, this.статистикаToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(257, 170);
            // 
            // осцилограммаToolStripMenuItem
            // 
            this.осцилограммаToolStripMenuItem.Name = "осцилограммаToolStripMenuItem";
            this.осцилограммаToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.осцилограммаToolStripMenuItem.Text = "Осцилограмма";
            this.осцилограммаToolStripMenuItem.Click += new System.EventHandler(this.осцилограммаToolStripMenuItem_Click);
            // 
            // dPFToolStripMenuItem
            // 
            this.dPFToolStripMenuItem.Name = "dPFToolStripMenuItem";
            this.dPFToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.dPFToolStripMenuItem.Text = "DPF";
            this.dPFToolStripMenuItem.Visible = false;
            this.dPFToolStripMenuItem.Click += new System.EventHandler(this.dPFToolStripMenuItem_Click);
            // 
            // корреляционныйАнализToolStripMenuItem
            // 
            this.корреляционныйАнализToolStripMenuItem.Name = "корреляционныйАнализToolStripMenuItem";
            this.корреляционныйАнализToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.корреляционныйАнализToolStripMenuItem.Text = "Корреляционный анализ";
            this.корреляционныйАнализToolStripMenuItem.Visible = false;
            this.корреляционныйАнализToolStripMenuItem.Click += new System.EventHandler(this.корреляционныйАнализToolStripMenuItem_Click);
            // 
            // спектральныйАнализToolStripMenuItem
            // 
            this.спектральныйАнализToolStripMenuItem.Name = "спектральныйАнализToolStripMenuItem";
            this.спектральныйАнализToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.спектральныйАнализToolStripMenuItem.Text = "Спектральный анализ";
            this.спектральныйАнализToolStripMenuItem.Click += new System.EventHandler(this.спектральныйАнализToolStripMenuItem_Click);
            // 
            // спектограммаToolStripMenuItem
            // 
            this.спектограммаToolStripMenuItem.Name = "спектограммаToolStripMenuItem";
            this.спектограммаToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.спектограммаToolStripMenuItem.Text = "Спектограмма";
            this.спектограммаToolStripMenuItem.Click += new System.EventHandler(this.спектограммаToolStripMenuItem_Click);
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            this.статистикаToolStripMenuItem.Click += new System.EventHandler(this.статистикаToolStripMenuItem_Click);
            // 
            // Navigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(835, 441);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Navigation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Каналы";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.close);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Exercise_Paint);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem спектограммаToolStripMenuItem;

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem осцилограммаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dPFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem корреляционныйАнализToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem спектральныйАнализToolStripMenuItem;
    }
}