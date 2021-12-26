using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Windows.Forms;

namespace Laba8
{
    public partial class Form1 : Form
    {
        private string filename;
        private string fileText;
        private List<string> filetextlist = new List<string>();
        private char[] vowels = "АЕЁИОУЫЭЮЯAEIOUY".ToLower().ToCharArray();
        private char[] consonants = "БВГДЖЗЙКЛМНПРСТФХЦЧШЩЬЪBCDFGHJKLMNPQRSTVWXZ".ToLower().ToCharArray();
        Matrix matrix1;
        Matrix matrix2;
        Temperature temp;
        TemperatureDict tempDict;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            Size = new Size(814, 487);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = openFileDialog1.FileName;
            fileText = System.IO.File.ReadAllText(filename);
            richTextBox1.Text = fileText;
        }
        static int GetCount(char[] array, char[] ar)
        {
            int count = 0;
            foreach (char ch in array)
                foreach (char cha in ar)
                    if (ch == cha)
                        count++;
            return count;
        }
        void podschet()
        {
            char[] array = fileText.ToLower().ToCharArray();
            int vowelsCount = GetCount(array, vowels);
            int consonantsCount = GetCount(array, consonants);
            textBox1.Text = Convert.ToString(vowelsCount);
            textBox2.Text = Convert.ToString(consonantsCount);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            podschet();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                Size = new Size(814, 487);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                Size = new Size(990, 478);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                Size = new Size(990, 546);
            }
            else if(tabControl1.SelectedIndex == 3)
            {
                Size = new Size(814, 487);
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                Size = new Size(990, 478);
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                Size = new Size(990, 546);
            }
        }
        // zadanie 2 
        public class Matrix //Ex2
        {
            public int[,] matrix;
            private int rows;
            private int columns;
            public int maxValue = 2;

            public Matrix(int rows, int columns, int MaxValue)
            {
                matrix = new int[rows, columns];
                this.rows = rows;
                this.columns = columns;
                maxValue = MaxValue + 1;
            }

            public Matrix(int rows, int columns) { matrix = new int[rows, columns]; this.rows = rows; this.columns = columns; }

            public void RandomGenerate()
            {
                Random rnd = new Random();
                for (int row = 0; row < rows; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        matrix[row, column] = rnd.Next(0, maxValue);
                    }
                }
            }

            static public bool TryMultiply(Matrix matrix1, Matrix matrix2, out Matrix matrixRes)
            {
                if (matrix1.columns == matrix2.rows)
                {
                    matrixRes = new Matrix(matrix1.rows, matrix2.columns);
                    for (int rowMatrix1 = 0; rowMatrix1 < matrix1.rows; rowMatrix1++)
                    {
                        for (int columnsMatrix2 = 0; columnsMatrix2 < matrix2.columns; columnsMatrix2++)
                        {
                            matrixRes.matrix[rowMatrix1, columnsMatrix2] = 0;
                            for (int column = 0; column < matrix1.columns; column++)
                                matrixRes.matrix[rowMatrix1, columnsMatrix2] += matrix1.matrix[rowMatrix1, column] * matrix2.matrix[column, columnsMatrix2];
                        }
                    }
                    return true;
                }
                else
                {
                    matrixRes = null;
                    return false;
                }
            }

            public void MatrixGui(DataGridView table)
            {
                table.Rows.Clear();
                table.Columns.Clear();
                for (int col = 0; col < columns; col++)
                    table.Columns.Add("", "");

                for (int row = 0; row < rows; row++)
                {
                    table.Rows.Add();
                    for (int cell = 0; cell < columns; cell++)
                        table[cell, row].Value = this.matrix[row, cell];
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Rows1.Value > 0 & Columns1.Value > 0 & Columns2.Value > 0 & Rows2.Value > 0)
            {
                matrix1 = new Matrix((int)Rows1.Value, (int)Columns1.Value, 30);
                matrix1.RandomGenerate();
                matrix1.MatrixGui(dataGridView1);
                matrix2 = new Matrix((int)Rows2.Value, (int)Columns2.Value, 30);
                matrix2.RandomGenerate();
                matrix2.MatrixGui(dataGridView2);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            bool mult = Matrix.TryMultiply(matrix1, matrix2, out Matrix multiplyResult);
            if (mult)
                multiplyResult.MatrixGui(dataGridView3);
            else
                MessageBox.Show("Ошибка умножения");
        }

        public class Temperature //task 3
        {
            public int[,] temperature;
            public double[] middleTemperature;
            public double[] middleTemperatureSort;
            public Temperature()
            {
                temperature = new int[12, 30];
                middleTemperature = new double[12];
                middleTemperatureSort = new double[12];
            }
            public void RandomGenerate()
            {
                Random rnd = new Random();
                for(int mounth = 0; mounth < 12; mounth++)
                {
                    if (mounth ==0 || mounth == 1 || mounth == 2 || mounth == 10 || mounth == 11)
                    {
                        for (int day = 0; day < 30; day++)
                        {
                            temperature[mounth, day] = rnd.Next(-35, 5);
                        }
                    }
                    else if (mounth ==3 || mounth == 9 )
                    {
                        for (int day = 0; day < 30; day++)
                        {
                            temperature[mounth, day] = rnd.Next(-10, 13);
                        }
                    } else if (mounth ==4 || mounth == 8)
                    {
                        for (int day = 0; day < 30; day++)
                        {
                            temperature[mounth, day] = rnd.Next(0, 20);
                        }
                    } else if (mounth == 5 || mounth == 6 || mounth == 7)
                    {
                        for (int day = 0; day < 30; day++)
                        {
                            temperature[mounth, day] = rnd.Next(14, 32);
                        }
                    }
                }
                MiddleTempCalc();
            }
            public void MiddleTempCalc()
            {
                for (int mounth = 0; mounth < 12; mounth++)
                {
                    int tempSum = 0;
                    for (int day = 0; day < 30; day++)
                    {
                        tempSum += temperature[mounth, day];
                    }
                    middleTemperature[mounth] = Math.Round((double)tempSum/30,1);
                }
            }
            public void SortTemp()
            {
                middleTemperatureSort = middleTemperature;
                Array.Sort(middleTemperatureSort);
            }
        }
        // task 4

        public class TextList
        {        
            private char[] vowels = "АЕЁИОУЫЭЮЯAEIOUY".ToLower().ToCharArray();
            private char[] consonants = "БВГДЖЗЙКЛМНПРСТФХЦЧШЩЬЪBCDFGHJKLMNPQRSTVWXZ".ToLower().ToCharArray();
            private string filenam;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            filetextlist.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = openFileDialog1.FileName;
            fileText = System.IO.File.ReadAllText(filename);
            filetextlist = System.IO.File.ReadAllLines(filename).ToList();
            richTextBox2.Text = fileText;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int vow = 0;
            int con = 0;
            for (int i =0; i<filetextlist.Count; i++)
            {
                char[] array = filetextlist[i].ToLower().ToCharArray();
                vow += GetCount(array, vowels);
                con += GetCount(array, consonants);
            }
            textBox3.Text = Convert.ToString(con);
            textBox4.Text = Convert.ToString(vow);
        }

        private void tempGenerateBtn_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "";
            temp.RandomGenerate();
            for (int mounth = 0; mounth < 12; mounth++)
            {
                richTextBox3.Text += $"\n\tMonth: {mounth + 1}\n\n";
                int day = 1;
                for (int days = 0; days < 30; days++)
                {
                    richTextBox3.Text += $"Day: {day}, Temp: {temp.temperature[mounth, days]}\n";
                    day++;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            temp = new Temperature();
            tempDict = new TemperatureDict();
        }

        private void MiddleTempBtn_Click(object sender, EventArgs e)
        {
            richTextBox4.Text = "";
            for (int i = 0; i < 12; i++)
            {
                richTextBox4.Text += $" Month: {i + 1}, TempAvg: {temp.middleTemperature[i]}\n";
            }
        }

        private void SortTempBtn_Click(object sender, EventArgs e)
        {
            richTextBox5.Text = "";
            temp.SortTemp();
            foreach(double d in temp.middleTemperatureSort)
            {
                richTextBox5.Text += $" TempAvg: {d +" "}\n";
            }            
        }

        // task 5 

        // task 6
        public class TemperatureDict
        {
            public Dictionary<string, int[]> temperature;
            public Dictionary<string, double> middleTemp;
            public Dictionary<string, double> middleTempSort;

            public void RandomGenerate()
            {
                temperature = new Dictionary<string, int[]>(12);
                temperature.Add("Январь", RandomTemp(31, -30, -5));
                temperature.Add("Февраль", RandomTemp(28, -25, 0));
                temperature.Add("Март", RandomTemp(31, -10, 5));
                temperature.Add("Апрель", RandomTemp(30, -5, 5));
                temperature.Add("Май", RandomTemp(31, -5, 10));
                temperature.Add("Июнь", RandomTemp(30, 5, 15));
                temperature.Add("Июль", RandomTemp(31, 8, 30));
                temperature.Add("Август", RandomTemp(31, 10, 30));
                temperature.Add("Сентябрь", RandomTemp(30, 8, 25));
                temperature.Add("Октябрь", RandomTemp(31, 0, 10));
                temperature.Add("Ноябрь", RandomTemp(30, -25, 0));
                temperature.Add("Декабрь", RandomTemp(31, -35, -5));
                MiddleTempCalc();
            }
            private int[] RandomTemp(int days, int min, int max)
            {
                Random random = new Random();
                int[] getTemp = new int[days];
                for (int day =0; day < days; day++)
                {
                    getTemp[day] = random.Next(min, max);
                }
                return getTemp;
            }
            private void MiddleTempCalc()
            {
                middleTemp = new Dictionary<string, double>(12);
                foreach(KeyValuePair<string,int[]> mounth in temperature)
                {
                    middleTemp.Add(mounth.Key, AvgTemp(mounth.Value));
                }
            }
            private double AvgTemp(int[] days)
            {
                double middle = 0;
                foreach (int day in days)
                {
                    middle += day;
                }
                return Math.Round(middle / days.Length);
            }
            public void Sort()
            {
                middleTempSort = new Dictionary<string, double>(12);
                middleTempSort = middleTemp.OrderBy(t => t.Value).ToDictionary(t => t.Key, t => t.Value);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox8.Text = "";
            tempDict.RandomGenerate();
            foreach (KeyValuePair<string, int[]> keyValuePair in tempDict.temperature)
            {
                richTextBox8.Text += $"{keyValuePair.Key}\n";
                int dayCount = 1;
                foreach (int days in keyValuePair.Value)
                {
                    richTextBox8.Text += $"{dayCount}: {days}\n";
                    dayCount++;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox7.Text = "";
            foreach(KeyValuePair<string, double> keyValuePair in tempDict.middleTemp)
            {
                richTextBox7.Text += $"{keyValuePair.Key}: {keyValuePair.Value}\n";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "";
            tempDict.Sort();
            foreach (KeyValuePair<string, double> keyValuePair in tempDict.middleTempSort)
            {
                richTextBox6.Text += $"{keyValuePair.Key}: {keyValuePair.Value}\n";
            }
        }
    }
}
