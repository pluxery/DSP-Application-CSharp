namespace WindowsApp
{
    partial class SignalInfo
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
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Len = new System.Windows.Forms.Label();
            this.Kan = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Count = new System.Windows.Forms.Label();
            this.frdis = new System.Windows.Forms.Label();
            this.begin = new System.Windows.Forms.Label();
            this.end = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.table.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 18);
            this.label8.TabIndex = 11;
            this.label8.Text = "Информация о каналах";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "Текущее состояние многоканального сигнала";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // table
            // 
            this.table.ColumnCount = 2;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.68182F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.table.Controls.Add(this.label3, 0, 1);
            this.table.Controls.Add(this.label2, 0, 0);
            this.table.Controls.Add(this.label4, 0, 2);
            this.table.Controls.Add(this.Len, 1, 5);
            this.table.Controls.Add(this.Kan, 1, 0);
            this.table.Controls.Add(this.label5, 0, 3);
            this.table.Controls.Add(this.label6, 0, 4);
            this.table.Controls.Add(this.label7, 0, 5);
            this.table.Controls.Add(this.Count, 1, 1);
            this.table.Controls.Add(this.frdis, 1, 2);
            this.table.Controls.Add(this.begin, 1, 3);
            this.table.Controls.Add(this.end, 1, 4);
            this.table.Location = new System.Drawing.Point(12, 41);
            this.table.Margin = new System.Windows.Forms.Padding(2);
            this.table.Name = "table";
            this.table.RowCount = 6;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.table.Size = new System.Drawing.Size(396, 140);
            this.table.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Общее число отсчётов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Общее число каналов";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Частота дискретизации";
            // 
            // Len
            // 
            this.Len.AutoSize = true;
            this.Len.Location = new System.Drawing.Point(201, 102);
            this.Len.MaximumSize = new System.Drawing.Size(200, 0);
            this.Len.Name = "Len";
            this.Len.Size = new System.Drawing.Size(41, 13);
            this.Len.TabIndex = 13;
            this.Len.Text = "label13";
            // 
            // Kan
            // 
            this.Kan.AutoSize = true;
            this.Kan.Location = new System.Drawing.Point(201, 0);
            this.Kan.Name = "Kan";
            this.Kan.Size = new System.Drawing.Size(35, 13);
            this.Kan.TabIndex = 8;
            this.Kan.Text = "label8";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Дата и время начала записи";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Дата и время окончания записи";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Длительность";
            // 
            // Count
            // 
            this.Count.AutoSize = true;
            this.Count.Location = new System.Drawing.Point(201, 20);
            this.Count.Name = "Count";
            this.Count.Size = new System.Drawing.Size(35, 13);
            this.Count.TabIndex = 9;
            this.Count.Text = "label9";
            // 
            // frdis
            // 
            this.frdis.AutoSize = true;
            this.frdis.Location = new System.Drawing.Point(201, 40);
            this.frdis.Name = "frdis";
            this.frdis.Size = new System.Drawing.Size(41, 13);
            this.frdis.TabIndex = 10;
            this.frdis.Text = "label10";
            // 
            // begin
            // 
            this.begin.AutoSize = true;
            this.begin.Location = new System.Drawing.Point(201, 60);
            this.begin.Name = "begin";
            this.begin.Size = new System.Drawing.Size(41, 13);
            this.begin.TabIndex = 11;
            this.begin.Text = "label11";
            // 
            // end
            // 
            this.end.AutoSize = true;
            this.end.Location = new System.Drawing.Point(201, 81);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(41, 13);
            this.end.TabIndex = 12;
            this.end.Text = "label12";
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HotTracking = true;
            this.listView1.HoverSelection = true;
            this.listView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.listView1.Location = new System.Drawing.Point(12, 218);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(396, 176);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "№";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Имя";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Источник";
            this.columnHeader3.Width = 81;
            // 
            // SignalInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 404);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.table);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SignalInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Информация о сигнале";
           // this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Print_information);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Len;
        private System.Windows.Forms.Label Kan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label Count;
        private System.Windows.Forms.Label frdis;
        private System.Windows.Forms.Label begin;
        private System.Windows.Forms.Label end;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}