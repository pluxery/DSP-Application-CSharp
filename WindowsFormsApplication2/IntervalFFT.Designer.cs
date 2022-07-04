using System.ComponentModel;

namespace WindowsApp
{
    partial class IntervalFFT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.textBoxStart = new System.Windows.Forms.TextBox();
            this.textBoxFinish = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.all_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxStart
            // 
            this.textBoxStart.Location = new System.Drawing.Point(39, 50);
            this.textBoxStart.Name = "textBoxStart";
            this.textBoxStart.Size = new System.Drawing.Size(77, 20);
            this.textBoxStart.TabIndex = 0;
            // 
            // textBoxFinish
            // 
            this.textBoxFinish.Location = new System.Drawing.Point(159, 49);
            this.textBoxFinish.Name = "textBoxFinish";
            this.textBoxFinish.Size = new System.Drawing.Size(75, 20);
            this.textBoxFinish.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label1.Location = new System.Drawing.Point(1, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "от:";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label2.Location = new System.Drawing.Point(122, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "до:";
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(78, 94);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 4;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(159, 94);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 5;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // all_btn
            // 
            this.all_btn.Location = new System.Drawing.Point(241, 49);
            this.all_btn.Name = "all_btn";
            this.all_btn.Size = new System.Drawing.Size(75, 23);
            this.all_btn.TabIndex = 6;
            this.all_btn.Text = "Все";
            this.all_btn.UseVisualStyleBackColor = true;
            this.all_btn.Click += new System.EventHandler(this.all_btn_Click);
            // 
            // IntervalFFT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 129);
            this.Controls.Add(this.all_btn);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFinish);
            this.Controls.Add(this.textBoxStart);
            this.Name = "IntervalFFT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Диапазон";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button all_btn;

        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button cancel_btn;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox textBoxStart;
        private System.Windows.Forms.TextBox textBoxFinish;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}