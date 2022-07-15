namespace WindowsApp
{
    partial class Fft
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fft));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.grid = new System.Windows.Forms.ToolStripButton();
            this.marks = new System.Windows.Forms.ToolStripButton();
            this.logBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.interv = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.smoothBtn = new System.Windows.Forms.ToolStripButton();
            this.X0_btn = new System.Windows.Forms.ToolStripButton();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.локальныйМасштабToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.глобальныйМасштабToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.grid, this.marks, this.logBtn, this.toolStripSeparator1, this.interv, this.toolStripButton2, this.toolStripButton3, this.smoothBtn, this.X0_btn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(923, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // grid
            // 
            this.grid.Checked = true;
            this.grid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.grid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.grid.Image = ((System.Drawing.Image) (resources.GetObject("grid.Image")));
            this.grid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(24, 24);
            this.grid.Text = "Решетка";
            this.grid.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // marks
            // 
            this.marks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.marks.Image = ((System.Drawing.Image) (resources.GetObject("marks.Image")));
            this.marks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.marks.Name = "marks";
            this.marks.Size = new System.Drawing.Size(24, 24);
            this.marks.Text = "Маркеры";
            this.marks.Click += new System.EventHandler(this.marks_Click);
            // 
            // logBtn
            // 
            this.logBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.logBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logBtn.Name = "logBtn";
            this.logBtn.Size = new System.Drawing.Size(37, 24);
            this.logBtn.Text = "LgY";
            this.logBtn.Click += new System.EventHandler(this.log_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // interv
            // 
            this.interv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.interv.Image = ((System.Drawing.Image) (resources.GetObject("interv.Image")));
            this.interv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.interv.Name = "interv";
            this.interv.Size = new System.Drawing.Size(24, 24);
            this.interv.Text = "Интервал";
            this.interv.Click += new System.EventHandler(this.interv_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton2.Text = "Увеличить";
            this.toolStripButton2.Visible = false;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Enabled = false;
            this.toolStripButton3.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton3.Text = "Уменьшить";
            this.toolStripButton3.Visible = false;
            // 
            // smoothBtn
            // 
            this.smoothBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.smoothBtn.Image = ((System.Drawing.Image) (resources.GetObject("smoothBtn.Image")));
            this.smoothBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.smoothBtn.Name = "smoothBtn";
            this.smoothBtn.Size = new System.Drawing.Size(106, 24);
            this.smoothBtn.Text = "Сглаживание";
            this.smoothBtn.Click += new System.EventHandler(this.smoothBtn_Click);
            // 
            // X0_btn
            // 
            this.X0_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.X0_btn.Image = ((System.Drawing.Image) (resources.GetObject("X0_btn.Image")));
            this.X0_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.X0_btn.Name = "X0_btn";
            this.X0_btn.Size = new System.Drawing.Size(40, 24);
            this.X0_btn.Text = "X(0)";
            this.X0_btn.Click += new System.EventHandler(this.X0_btn_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // локальныйМасштабToolStripMenuItem
            // 
            this.локальныйМасштабToolStripMenuItem.Name = "локальныйМасштабToolStripMenuItem";
            this.локальныйМасштабToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // глобальныйМасштабToolStripMenuItem
            // 
            this.глобальныйМасштабToolStripMenuItem.Name = "глобальныйМасштабToolStripMenuItem";
            this.глобальныйМасштабToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // FFT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(923, 436);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Fft";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Быстрое преобразование Фурье";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Close);
            this.Load += new System.EventHandler(this.DFT_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Position2);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Position1);
            this.Resize += new System.EventHandler(this.resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripButton X0_btn;

        private System.Windows.Forms.ToolStripButton smoothBtn;

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton interv;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton grid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem локальныйМасштабToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem глобальныйМасштабToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton logBtn;
        private System.Windows.Forms.ToolStripButton marks;
    }
}