namespace WindowsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.save = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.моделированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сДискретнымиАргументамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.задержанныйЕдиничныйИмпульсToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.задержанныйЕдиничныйСкачокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.меандрСПериодомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пилаСПериодомLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сНепрерывнымиАргументамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.соСлучайнымиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.белыйШумВToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.случайныйСигналАРССpqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.арифметическаяСуперпозицияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мультипликативнаяСуперпозицияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.корреляционныйАнализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.инструментыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SigInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.задатьДиапазонToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.осцилограммаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дПToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.save, this.открытьToolStripMenuItem, this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            this.файлToolStripMenuItem.Click += new System.EventHandler(this.файлToolStripMenuItem_Click);
            // 
            // save
            // 
            this.save.Enabled = false;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(152, 24);
            this.save.Text = "Сохранить";
            this.save.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Enabled = false;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Visible = false;
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // моделированиеToolStripMenuItem
            // 
            this.моделированиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.сДискретнымиАргументамиToolStripMenuItem, this.сНепрерывнымиАргументамиToolStripMenuItem, this.соСлучайнымиToolStripMenuItem});
            this.моделированиеToolStripMenuItem.Name = "моделированиеToolStripMenuItem";
            this.моделированиеToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.моделированиеToolStripMenuItem.Text = "Моделирование";
            this.моделированиеToolStripMenuItem.Click += new System.EventHandler(this.моделированиеToolStripMenuItem_Click);
            // 
            // сДискретнымиАргументамиToolStripMenuItem
            // 
            this.сДискретнымиАргументамиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.задержанныйЕдиничныйИмпульсToolStripMenuItem, this.задержанныйЕдиничныйСкачокToolStripMenuItem, this.дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem, this.дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem, this.меандрСПериодомToolStripMenuItem, this.пилаСПериодомLToolStripMenuItem});
            this.сДискретнымиАргументамиToolStripMenuItem.Name = "сДискретнымиАргументамиToolStripMenuItem";
            this.сДискретнымиАргументамиToolStripMenuItem.Size = new System.Drawing.Size(298, 24);
            this.сДискретнымиАргументамиToolStripMenuItem.Text = "С дискретными аргументами";
            // 
            // задержанныйЕдиничныйИмпульсToolStripMenuItem
            // 
            this.задержанныйЕдиничныйИмпульсToolStripMenuItem.Name = "задержанныйЕдиничныйИмпульсToolStripMenuItem";
            this.задержанныйЕдиничныйИмпульсToolStripMenuItem.Size = new System.Drawing.Size(809, 24);
            this.задержанныйЕдиничныйИмпульсToolStripMenuItem.Text = "Задержанный единичный импульс";
            this.задержанныйЕдиничныйИмпульсToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // задержанныйЕдиничныйСкачокToolStripMenuItem
            // 
            this.задержанныйЕдиничныйСкачокToolStripMenuItem.Name = "задержанныйЕдиничныйСкачокToolStripMenuItem";
            this.задержанныйЕдиничныйСкачокToolStripMenuItem.Size = new System.Drawing.Size(809, 24);
            this.задержанныйЕдиничныйСкачокToolStripMenuItem.Text = "Задержанный единичный скачок";
            this.задержанныйЕдиничныйСкачокToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem
            // 
            this.дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem.Name = "дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem";
            this.дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem.Size = new System.Drawing.Size(809, 24);
            this.дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem.Text = "Дискретизированная убывающая экспонента";
            this.дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem
            // 
            this.дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem.Name = "дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem" + "";
            this.дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem.Size = new System.Drawing.Size(809, 24);
            this.дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem.Text = "Дискретизированная синусоида с заданными амплитудой a, круговой частотой ω и нача" + "льной фазой φ.";
            this.дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // меандрСПериодомToolStripMenuItem
            // 
            this.меандрСПериодомToolStripMenuItem.Name = "меандрСПериодомToolStripMenuItem";
            this.меандрСПериодомToolStripMenuItem.Size = new System.Drawing.Size(809, 24);
            this.меандрСПериодомToolStripMenuItem.Text = "\"Меандр\" с периодом L";
            this.меандрСПериодомToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // пилаСПериодомLToolStripMenuItem
            // 
            this.пилаСПериодомLToolStripMenuItem.Name = "пилаСПериодомLToolStripMenuItem";
            this.пилаСПериодомLToolStripMenuItem.Size = new System.Drawing.Size(809, 24);
            this.пилаСПериодомLToolStripMenuItem.Text = "\"Пила\" с периодом L";
            this.пилаСПериодомLToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // сНепрерывнымиАргументамиToolStripMenuItem
            // 
            this.сНепрерывнымиАргументамиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem, this.cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem, this.cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem, this.сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem});
            this.сНепрерывнымиАргументамиToolStripMenuItem.Name = "сНепрерывнымиАргументамиToolStripMenuItem";
            this.сНепрерывнымиАргументамиToolStripMenuItem.Size = new System.Drawing.Size(298, 24);
            this.сНепрерывнымиАргументамиToolStripMenuItem.Text = "С непрерывными аргументами";
            // 
            // сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem
            // 
            this.сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Name = "сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem";
            this.сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Size = new System.Drawing.Size(548, 24);
            this.сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Text = "Cигнал с экспоненциальной огибающей - амплитудная модуляция";
            this.сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem
            // 
            this.cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Name = "cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem";
            this.cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Size = new System.Drawing.Size(548, 24);
            this.cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Text = "Cигнал с балансной огибающей - амплитудная модуляция";
            this.cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem
            // 
            this.cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Name = "cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem";
            this.cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Size = new System.Drawing.Size(548, 24);
            this.cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Text = "Cигнал с тональной огибающей. - амплитудная модуляция";
            this.cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem
            // 
            this.сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem.Name = "сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem";
            this.сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem.Size = new System.Drawing.Size(548, 24);
            this.сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem.Text = "Сигнал с линейной частотной модуляцией";
            this.сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // соСлучайнымиToolStripMenuItem
            // 
            this.соСлучайнымиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.белыйШумВToolStripMenuItem, this.белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMenuItem, this.случайныйСигналАРССpqToolStripMenuItem});
            this.соСлучайнымиToolStripMenuItem.Name = "соСлучайнымиToolStripMenuItem";
            this.соСлучайнымиToolStripMenuItem.Size = new System.Drawing.Size(298, 24);
            this.соСлучайнымиToolStripMenuItem.Text = "Случайных сигналов";
            // 
            // белыйШумВToolStripMenuItem
            // 
            this.белыйШумВToolStripMenuItem.Name = "белыйШумВToolStripMenuItem";
            this.белыйШумВToolStripMenuItem.Size = new System.Drawing.Size(745, 24);
            this.белыйШумВToolStripMenuItem.Text = "Белый шум равномерный в [a,b]";
            this.белыйШумВToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMenuItem
            // 
            this.белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMenuItem.Name = "белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMen" + "uItem";
            this.белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMenuItem.Size = new System.Drawing.Size(745, 24);
            this.белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMenuItem.Text = "Белый шум распределенный по нормальному закону с заданным средним a, и дисперсией" + " σ ²";
            this.белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // случайныйСигналАРССpqToolStripMenuItem
            // 
            this.случайныйСигналАРССpqToolStripMenuItem.Name = "случайныйСигналАРССpqToolStripMenuItem";
            this.случайныйСигналАРССpqToolStripMenuItem.Size = new System.Drawing.Size(745, 24);
            this.случайныйСигналАРССpqToolStripMenuItem.Text = "Случайный сигнал АРСС (p,q)";
            this.случайныйСигналАРССpqToolStripMenuItem.Click += new System.EventHandler(this.modelling);
            // 
            // фильтрацияToolStripMenuItem
            // 
            this.фильтрацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.арифметическаяСуперпозицияToolStripMenuItem, this.мультипликативнаяСуперпозицияToolStripMenuItem});
            this.фильтрацияToolStripMenuItem.Name = "фильтрацияToolStripMenuItem";
            this.фильтрацияToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.фильтрацияToolStripMenuItem.Text = "Фильтрация";
            // 
            // арифметическаяСуперпозицияToolStripMenuItem
            // 
            this.арифметическаяСуперпозицияToolStripMenuItem.Enabled = false;
            this.арифметическаяСуперпозицияToolStripMenuItem.Name = "арифметическаяСуперпозицияToolStripMenuItem";
            this.арифметическаяСуперпозицияToolStripMenuItem.Size = new System.Drawing.Size(322, 24);
            this.арифметическаяСуперпозицияToolStripMenuItem.Text = "Арифметическая суперпозиция";
            this.арифметическаяСуперпозицияToolStripMenuItem.Click += new System.EventHandler(this.арифметическаяСуперпозицияToolStripMenuItem_Click);
            // 
            // мультипликативнаяСуперпозицияToolStripMenuItem
            // 
            this.мультипликативнаяСуперпозицияToolStripMenuItem.Enabled = false;
            this.мультипликативнаяСуперпозицияToolStripMenuItem.Name = "мультипликативнаяСуперпозицияToolStripMenuItem";
            this.мультипликативнаяСуперпозицияToolStripMenuItem.Size = new System.Drawing.Size(322, 24);
            this.мультипликативнаяСуперпозицияToolStripMenuItem.Text = "Мультипликативная суперпозиция";
            this.мультипликативнаяСуперпозицияToolStripMenuItem.Click += new System.EventHandler(this.мультипликативнаяСуперпозицияToolStripMenuItem_Click);
            // 
            // анализToolStripMenuItem
            // 
            this.анализToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.статистикаToolStripMenuItem, this.корреляционныйАнализToolStripMenuItem});
            this.анализToolStripMenuItem.Name = "анализToolStripMenuItem";
            this.анализToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.анализToolStripMenuItem.Text = "Анализ";
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            // 
            // корреляционныйАнализToolStripMenuItem
            // 
            this.корреляционныйАнализToolStripMenuItem.Name = "корреляционныйАнализToolStripMenuItem";
            this.корреляционныйАнализToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.корреляционныйАнализToolStripMenuItem.Text = "Корреляционный анализ";
            this.корреляционныйАнализToolStripMenuItem.Visible = false;
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.оПрограммеToolStripMenuItem, this.оПрограммеToolStripMenuItem1});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.оПрограммеToolStripMenuItem.Text = "Просмотр справки";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem1
            // 
            this.оПрограммеToolStripMenuItem1.Name = "оПрограммеToolStripMenuItem1";
            this.оПрограммеToolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
            this.оПрограммеToolStripMenuItem1.Text = "О программе";
            this.оПрограммеToolStripMenuItem1.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.файлToolStripMenuItem, this.моделированиеToolStripMenuItem, this.фильтрацияToolStripMenuItem, this.анализToolStripMenuItem, this.инструментыToolStripMenuItem, this.осцилограммаToolStripMenuItem, this.дПToolStripMenuItem, this.настройкиToolStripMenuItem, this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1128, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.моделированиеToolStripMenuItem_Click);
            // 
            // инструментыToolStripMenuItem
            // 
            this.инструментыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.SigInfo, this.задатьДиапазонToolStripMenuItem});
            this.инструментыToolStripMenuItem.Name = "инструментыToolStripMenuItem";
            this.инструментыToolStripMenuItem.Size = new System.Drawing.Size(115, 24);
            this.инструментыToolStripMenuItem.Text = "Инструменты";
            // 
            // SigInfo
            // 
            this.SigInfo.Enabled = false;
            this.SigInfo.Name = "SigInfo";
            this.SigInfo.Size = new System.Drawing.Size(243, 24);
            this.SigInfo.Text = "Информация о сигнале";
            this.SigInfo.Click += new System.EventHandler(this.информацияОСигналеToolStripMenuItem_Click);
            // 
            // задатьДиапазонToolStripMenuItem
            // 
            this.задатьДиапазонToolStripMenuItem.Enabled = false;
            this.задатьДиапазонToolStripMenuItem.Name = "задатьДиапазонToolStripMenuItem";
            this.задатьДиапазонToolStripMenuItem.Size = new System.Drawing.Size(243, 24);
            this.задатьДиапазонToolStripMenuItem.Text = "Задать диапазон";
            this.задатьДиапазонToolStripMenuItem.Click += new System.EventHandler(this.задатьДиапазонToolStripMenuItem_Click);
            // 
            // осцилограммаToolStripMenuItem
            // 
            this.осцилограммаToolStripMenuItem.Name = "осцилограммаToolStripMenuItem";
            this.осцилограммаToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.осцилограммаToolStripMenuItem.Text = "Осцилограмма";
            this.осцилограммаToolStripMenuItem.Click += new System.EventHandler(this.осцилограммаToolStripMenuItem_Click);
            // 
            // дПToolStripMenuItem
            // 
            this.дПToolStripMenuItem.Name = "дПToolStripMenuItem";
            this.дПToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.дПToolStripMenuItem.Text = "БПФ";
            this.дПToolStripMenuItem.Click += new System.EventHandler(this.ДПТToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 604);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DSP - Dranov, Afanaseva";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem моделированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фильтрацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem анализToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem инструментыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SigInfo;
        private System.Windows.Forms.ToolStripMenuItem осцилограммаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem задатьДиапазонToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem save;
        private System.Windows.Forms.ToolStripMenuItem сДискретнымиАргументамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem задержанныйЕдиничныйИмпульсToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem задержанныйЕдиничныйСкачокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дискретизированнаяУбывающаяЭкспонентаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дискретизированнаяСинусоидаСЗаданнымиАмплитудойAКруговойЧастотойToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сНепрерывнымиАргументамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem меандрСПериодомToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пилаСПериодомLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сигналСЭкспоненциальнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cигналСБаланснойОгибающейАмплитуднаяМодуляцияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cигналСТональнойОгибающейАмплитуднаяМодуляцияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сигналСЛинейнойЧастотнойМодуляциейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem соСлучайнымиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem белыйШумВToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem белыйШумРаспределенныйПоНормальномуЗаконуСЗаданнымСреднимAИДисперсиейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem случайныйСигналАРССpqToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem арифметическаяСуперпозицияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem мультипликативнаяСуперпозицияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дПToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem корреляционныйАнализToolStripMenuItem;
    }
}

