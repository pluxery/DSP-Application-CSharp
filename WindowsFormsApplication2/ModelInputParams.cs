using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsApp
{
    public partial class ModelInputParams : Form
    {
        Signal signal = Signal.GetInstance();
        static string str = "-,.";
        static char[] symb = str.ToCharArray(0, 3);

        private Label label = new Label();
        List<Label> labels = new List<Label>();
        List<TextBox> texts = new List<TextBox>();

        private Button buttonOK = new Button();
        private Button buttonFormula = new Button();

        int margin_y = 40;
        int textsCount;
        Model model;

        MyDelegate[] check = new MyDelegate[10];

        delegate void MyDelegate(object sender, EventArgs e);

        Navigation newForm;
        String textmod;
        ModelInputParams modellng;
        private int modelNum;

        public ModelInputParams()
        {
            CreateModelParamWindow();
        }

        private void CreateModelParamWindow()
        {
            InitializeComponent();
            check[0] = check1;
            check[1] = check2;
            check[2] = check3;
            check[3] = check4;
            check[4] = check5;
            check[5] = check6;
            check[6] = check7;
            check[7] = check8;
            check[8] = check9;
            check[9] = check10;
            label.AutoSize = true;
            label.Location = new Point(14, 11);
            label.Name = "label";
            textmod = signal.Textmod;
            label.Text = textmod;
            label.Margin = new Padding(2, 0, 2, 0);
            label.TabIndex = 10;
            label.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(label);
            if (signal.CountOfSamples == 0 || signal.Frequency == 0)
            {
                CreateTextLabel("n - кол-во отсчетов", 7);
                CreateTextLabel("fd - частота дискретизации", 1);
            }

            CreateModelFields();
        }

        private void CreateModel(object sender, EventArgs e)
        {
            Boolean isAllFieldsFilled = true;
            int i = 0;
            while (isAllFieldsFilled & (i < texts.Count))
            {
                if (texts[i].Text == String.Empty)
                    isAllFieldsFilled = false;
                i++;
            }

            if (isAllFieldsFilled)
            {
                if (texts[0].Name == "n - кол-во отсчетов")
                {
                    signal.CountOfSamples = Convert.ToInt32(texts[0].Text);
                    texts.RemoveAt(0);
                }

                if (texts[0].Name == "fd - частота дискретизации")
                {
                    signal.Frequency = Convert.ToDouble(texts[0].Text);
                    texts.RemoveAt(0);
                }

                if (!signal.CheckHash("modell_t") && textmod.Trim() == "Случайный сигнал АРСС (p,q)")
                {
                    if (signal.CheckHash("modell"))
                    {
                        modellng = (ModelInputParams) signal.GetHash("modell");
                        modellng.Close();
                    }

                    signal.SetHash("modell_t", texts);
                    modellng = new ModelInputParams();
                    modellng.MdiParent = signal.MainForm;
                    modellng.Show();
                    signal.SetHash("modell", modellng);
                }
                else
                {
                    if (signal.Model != null)
                    {
                        signal.Model.Close();
                    }

                    model = new Model(signal.MainForm);
                    signal.SetHash("model", texts);
                    signal.SetModel(model);
                    signal.Model.Init(0);
                    signal.Model.Show();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите значения во все поля");
            }
        }

        private void CreateTextLabel(string name, int i_check) // название + номер обработчика ввода в поле
        {
            labels.Add(new Label());
            labels[labels.Count - 1].AutoSize = true;
            labels[labels.Count - 1].Location = new Point(20, 20 + labels.Count * margin_y);
            labels[labels.Count - 1].Name = name;
            labels[labels.Count - 1].Text = name;
            labels[labels.Count - 1].Margin = new Padding(0, 0, 0, 0);
            labels[labels.Count - 1].TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(labels[labels.Count - 1]);
            texts.Add(new TextBox());

            if (name == "n - кол-во отсчетов")
                texts[texts.Count - 1].Text = "1000";
            else if (name == "fd - частота дискретизации")
                texts[texts.Count - 1].Text = "1";

            texts[texts.Count - 1].Location = new Point(20, 35 + texts.Count * margin_y);
            texts[texts.Count - 1].Name = name;
            texts[texts.Count - 1].Margin = new Padding(0, 0, 0, 0);
            Controls.Add(texts[texts.Count - 1]);
            texts[texts.Count - 1].KeyPress += Start_KeyPress;
            if (i_check != -1)
                texts[texts.Count - 1].LostFocus += new EventHandler(check[i_check]);
            textsCount = texts.Count + 1;
        }

        private void CreateModelFields()
        {
            switch (textmod.Trim())
            {
                case "Задержанный единичный импульс":
                    signal.SetHash("model_k", 1);
                    modelNum = 1;
                    CreateTextLabel("n0 – задержка импульса", 6);
                    texts[texts.Count - 1].Text = "500";
                    break;
                case "Задержанный единичный скачок":
                    signal.SetHash("model_k", 2);
                    modelNum = 2;
                    CreateTextLabel("n0 – задержка скачка", 4);
                    texts[texts.Count - 1].Text = "500";
                    break;
                case "Дискретизированная убывающая экспонента":
                    signal.SetHash("model_k", 3);
                    modelNum = 3;
                    CreateTextLabel("a [0, 1]", 0);
                    texts[texts.Count - 1].Text = "0,995";
                    break;
                case
                    "Дискретизированная синусоида с заданными амплитудой a, круговой частотой \u03C9 и начальной фазой \u03C6."
                    :
                    signal.SetHash("model_k", 4);
                    modelNum = 4;
                    CreateTextLabel("a - амплитуда", 4);
                    texts[texts.Count - 1].Text = "1";
                    CreateTextLabel("\u03C9 - круговая частота [0, 180]", 2);
                    texts[texts.Count - 1].Text = "4";
                    CreateTextLabel("\u03C6 - начальная фаза [0, 360]", 3);
                    texts[texts.Count - 1].Text = "0";
                    break;
                case "\"Меандр\" с периодом L":
                    signal.SetHash("model_k", 5);
                    modelNum = 5;
                    CreateTextLabel("L (>0)", 5);
                    texts[texts.Count - 1].Text = "100";
                    break;
                case "\"Пила\" с периодом L":
                    modelNum = 6;
                    signal.SetHash("model_k", 6);
                    CreateTextLabel("L - период", 5);
                    texts[texts.Count - 1].Text = "100";
                    break;
                case "Cигнал с экспоненциальной огибающей - амплитудная модуляция":
                    modelNum = 7;
                    signal.SetHash("model_k", 7);
                    CreateTextLabel("a - амплитуда сигнала", 4);
                    texts[texts.Count - 1].Text = "1";
                    CreateTextLabel("\u03C4-параметр ширины огибающей", 8);
                    CreateTextLabel("\u0192 - частота несущей[0,0.5\u0192d]", 9);
                    CreateTextLabel("\u03C6 - начальная фаза несущей", 3);
                    texts[texts.Count - 1].Text = "0";
                    break;
                case "Cигнал с балансной огибающей - амплитудная модуляция":
                    modelNum = 8;
                    signal.SetHash("model_k", 8);
                    CreateTextLabel("a - амплитуда сигнала", 4);
                    texts[texts.Count - 1].Text = "1";
                    CreateTextLabel("\u0192 \u2080 - частота огибающей", 4);
                    CreateTextLabel("\u0192 \u2099 - частота несущей", 4);
                    CreateTextLabel("\u03C6 - начальная фаза несущей", 3);
                    texts[texts.Count - 1].Text = "0";
                    break;
                case "Cигнал с тональной огибающей. - амплитудная модуляция":
                    signal.SetHash("model_k", 9);
                    modelNum = 9;
                    CreateTextLabel("a - амплитуда сигнала", 4);
                    texts[texts.Count - 1].Text = "1";
                    CreateTextLabel("m-индекс глубины модуляции[0,1]", 0);
                    CreateTextLabel("\u0192 \u2080 - частота огибающей", 4);
                    CreateTextLabel("\u0192 \u2096 - частота несущей", 4);
                    CreateTextLabel("\u03C6 - начальная фаза несущей", 3);
                    texts[texts.Count - 1].Text = "0";
                    break;

                case "Сигнал с линейной частотной модуляцией":
                    signal.SetHash("model_k", 15); //ЭТО 10 МОДЕЛЬ!
                    modelNum = 10;
                    CreateTextLabel("a - амплитуда сигнала", 4);
                    texts[texts.Count - 1].Text = "1";
                    CreateTextLabel("\u0192 \u2080-частота в начальный момент", 4);
                    CreateTextLabel("\u0192 \u2099-частота в конечный момент", 4);
                    CreateTextLabel("\u03C6 - начальная фаза несущей", 3);
                    texts[texts.Count - 1].Text = "0";
                    break;

                case "Белый шум равномерный в [a,b]":
                    modelNum = 11;
                    signal.SetHash("model_k", 10);
                    CreateTextLabel("a", 8);
                    texts[texts.Count - 1].Text = "100";
                    CreateTextLabel("b", 8);
                    texts[texts.Count - 1].Text = "110";
                    break;
                case "Белый шум распределенный по нормальному закону с заданным средним a, и дисперсией σ ²":
                    modelNum = 12;
                    signal.SetHash("model_k", 11);
                    CreateTextLabel("a - среднее", 8);
                    CreateTextLabel("σ ² - дисперсия", 4);
                    break;
                case "Случайный сигнал АРСС (p,q)":
                    modelNum = 13;
                    if (signal.CheckHash("modell_t"))
                    {
                        List<TextBox> texts = (List<TextBox>) signal.GetHash("modell_t");
                        for (int i = 1; i <= Convert.ToInt32(texts[0].Text); i++)
                        {
                            string a = "a" + i.ToString();
                            CreateTextLabel(a, 8);
                        }


                        for (int i = 1; i <= Convert.ToInt32(texts[1].Text); i++)
                        {
                            string b = "b" + i.ToString();
                            CreateTextLabel(b, 8);
                        }
                    }
                    else
                    {
                        signal.SetHash("model_k", 12);
                        CreateTextLabel("p", 6);
                        CreateTextLabel("q", 6);
                    }

                    break;
                case "Арифметическая суперпозиция":
                    signal.SetHash("model_k", 13);
                    for (int i = 0; i <= (int) signal.GetHash("super_c"); i++)
                    {
                        string a = "a" + i.ToString();
                        CreateTextLabel(a, 8);
                    }

                    break;
                case "Мультипликативная суперпозиция":
                    signal.SetHash("model_k", 14);
                    CreateTextLabel("a", 8);
                    break;
            }

            buttonOK.Text = "Ok";
            buttonOK.Location = new Point(20, 20 + margin_y * textsCount);
            buttonOK.Click += CreateModel;
            Controls.Add(buttonOK);
            Height = 11 + margin_y * (textsCount + 3);
            Width = 270;

            buttonFormula.Text = "Формула";
            buttonFormula.Location = new Point(150, 20 + margin_y * textsCount);
            buttonFormula.Click += getFormula_Click;
            Controls.Add(buttonFormula);
            Height = 11 + margin_y * (textsCount + 3);
            Width = 270;
            buttonFormula.Enabled = false;
        }

        private void getFormula_Click(object sender, EventArgs e)
        {
            var modelFormulaWindow = new ModelImage(modelNum);
            modelFormulaWindow.Show();
        }

        private void Start_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !(Array.IndexOf(symb, e.KeyChar) != -1) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private static void check1(object sender, EventArgs e) //0 //значение а от 0 до 1
        {
            Double number;
            if (((Control) sender).Text != "")
            {
                if (!Double.TryParse(((Control) sender).Text, out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (Convert.ToDouble(((Control) sender).Text) >= 1 ||
                        Convert.ToDouble(((Control) sender).Text) <= 0)
                    {
                        MessageBox.Show("Значение должно быть в интервале от 0 до 1!");
                        ((Control) sender).Text = "0,1";
                    }
                }
            }
        }

        private void check2(object sender, EventArgs e) //1 //дробное больше нуля
        {
            Double number;
            if (((Control) sender).Text != "")
            {
                if (!Double.TryParse(((Control) sender).Text, out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (Convert.ToDouble(((Control) sender).Text) <= 0)
                    {
                        MessageBox.Show("Значениие должно быть больше нуля !");
                        ((Control) sender).Text = "1";
                    }
                }

                if (textmod.Trim() == "Cигнал с экспоненциальной огибающей - амплитудная модуляция")
                    if (texts[4].Text != "")
                    {
                        if (Convert.ToDouble(texts[4].Text) > (Convert.ToDouble(((Control) sender).Text) * 0.5))
                        {
                            MessageBox.Show(
                                "Замените значение в поле f(частота несущей). Она не должна превышать половины значения частоты дискретизации!");
                            texts[4].Text = "";
                        }
                    }
            }
        }

        private static void check3(object sender, EventArgs e) //2 //значение круговой частоты от 0 до 180
        {
            Double number;
            if (((Control) sender).Text != "")
            {
                if (!Double.TryParse((((Control) sender).Text), out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (Convert.ToDouble(((Control) sender).Text) > 180 ||
                        Convert.ToDouble(((Control) sender).Text) < 0)
                    {
                        MessageBox.Show("Значениие должно быть в конечном диапазоне от 0 до 180 (град.)");
                        ((Control) sender).Text = "0";
                    }
                }
            }
        }

        private static void check4(object sender, EventArgs e) //3 //значение начальной фазы от 0 до 360
        {
            Double number;
            if (((Control) sender).Text != "")
            {
                if (!Double.TryParse(((Control) sender).Text, out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (Convert.ToDouble(((Control) sender).Text) > 360 ||
                        Convert.ToDouble(((Control) sender).Text) < 0)
                    {
                        MessageBox.Show("Значениие должно быть в конечном диапазоне от 0 до 360 (град.)");
                        ((Control) sender).Text = "0";
                    }
                }
            }
        }

        private static void check5(object sender, EventArgs e) //4 //дробное, больше либо равно нулю
        {
            Double number;
            if (((Control) sender).Text != "")
            {
                if (!Double.TryParse(((Control) sender).Text, out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (Convert.ToDouble(((Control) sender).Text) < 0)
                    {
                        MessageBox.Show("Значениие амплитуды должно быть неотрицательным");
                        ((Control) sender).Text = "1";
                    }
                }
            }
        }

        private static void check6(object sender, EventArgs e) //5 //нет значения в нуле в нуле // для деления по модулю
        {
            Double number;
            if (((Control) sender).Text != "")
            {
                if (!Double.TryParse(((Control) sender).Text, out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (Convert.ToDouble(((Control) sender).Text) == 0)
                    {
                        MessageBox.Show("Значениие не может быть равно нулю");
                        ((Control) sender).Text = "1";
                    }
                }
            }
        }

        private static void check7(object sender, EventArgs e) //6 //целое, больше либо равно нулю
        {
            int number;
            if (((Control) sender).Text != "")
            {
                if (!Int32.TryParse(((Control) sender).Text, out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (Convert.ToInt32(((Control) sender).Text) < 0)
                    {
                        MessageBox.Show("Значениие должно быть неотрицательным");
                        ((Control) sender).Text = "1";
                    }
                }
            }
        }

        private static void check8(object sender, EventArgs e) //7 //целое, больше нуля       
        {
            int number;
            if (((Control) sender).Text != "")
            {
                if (!Int32.TryParse(((Control) sender).Text, out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (Convert.ToInt32(((Control) sender).Text) <= 0)
                    {
                        MessageBox.Show("Значениие должно быть больше нуля");
                        ((Control) sender).Text = "1";
                    }
                }
            }
        }

        private static void check9(object sender, EventArgs e) //8 //есть цифры       
        {
            double number;
            if (((Control) sender).Text != "")
            {
                if (!Double.TryParse(((Control) sender).Text, out number))
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
            }
        }

        private void
            check10(object sender, EventArgs e) //9 //частота несущей (задается в интервале 0; 0.5*fd  ),       
        {
            double number, f_05 = 0;
            if (((Control) sender).Text != "") //если что-то в поле записано
            {
                if (!Double.TryParse(((Control) sender).Text, out number)) //преобразовываем текст в дробное число
                {
                    MessageBox.Show("Введено неверное значение");
                    ((Control) sender).Text = "";
                }
                else
                {
                    if (signal.Frequency == 0)
                    {
                        if (texts[1].Text == "")
                        {
                            MessageBox.Show(
                                "Значение в поле f(частота несущей) зависит от значения частоты дискретизации. Заполните указанные поля!");
                            ((Control) sender).Text = "";
                        }
                        else
                        {
                            f_05 = Convert.ToDouble(texts[1].Text) * 0.5;
                        }
                    }
                    else
                    {
                        f_05 = signal.Frequency * 0.5;
                    }

                    if (f_05 != 0)
                        if (Convert.ToDouble(((Control) sender).Text) > f_05 ||
                            Convert.ToDouble(((Control) sender).Text) < 0)
                        {
                            MessageBox.Show("Значениие должно быть в интервале от 0 до " + f_05 +
                                            " (1/2 значения частоты дискретизации)");
                            ((Control) sender).Text = Convert.ToString(f_05);
                        }
                }
            }
        }

        private void Modelling_Load(object sender, EventArgs e)
        {
        }
    }
}