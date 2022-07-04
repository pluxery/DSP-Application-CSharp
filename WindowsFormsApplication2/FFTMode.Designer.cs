using System.ComponentModel;

namespace WindowsApp
{
    partial class FFTMode
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ok_btn = new System.Windows.Forms.Button();
            this.canel_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {"X(0) = |X(1)|", "X(0) = 0", "X(0) = ничего не делать"});
            this.checkedListBox1.Location = new System.Drawing.Point(33, 52);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(251, 52);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.818182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label1.Location = new System.Drawing.Point(33, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберете режим:";
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(61, 122);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 2;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // canel_btn
            // 
            this.canel_btn.Location = new System.Drawing.Point(161, 122);
            this.canel_btn.Name = "canel_btn";
            this.canel_btn.Size = new System.Drawing.Size(75, 23);
            this.canel_btn.TabIndex = 3;
            this.canel_btn.Text = "Canel";
            this.canel_btn.UseVisualStyleBackColor = true;
            this.canel_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // FFTMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 162);
            this.Controls.Add(this.canel_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "FFTMode";
            this.Text = "Режим расчета";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button canel_btn;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.CheckedListBox checkedListBox1;

        #endregion
    }
}