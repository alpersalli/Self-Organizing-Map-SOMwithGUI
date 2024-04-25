using SOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSS2019510154
{
    public partial class Form1 : Form
    {
        public int dimension;
        public static string x;
        private string data;
        private string[] clusters;
        private Form2 form2;

        public Form1(string[] clusters)
        {
            InitializeComponent();
            this.clusters = clusters;

        }



  

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Metin Dosyaları (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // path of selected file
                textBox1.Text = openFileDialog.FileName;

            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            string file = textBox1.Text;

            if (file == "")
            {
                return;
            }
            System.IO.StreamReader sr = new System.IO.StreamReader(file);
            int lineCount = File.ReadLines(file).Count();
            Console.WriteLine(lineCount);
            string[] fileContent = new string[lineCount];
            int index = 0;
            string line;


            sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                // Satırı diziye ekleyin
                fileContent[index] = line;
                index++;
            }


            sr.BaseStream.Position = 0;


            int lng = sr.ReadLine().Split(',').Length;
            new Map(lng - 1, dimension, file);
            // print clusters

            sr.Close();

            int widthgrid = 10;
            int heightgrid = 150;
            for (int i = 0; i < dimension*dimension; i++)
            {

                Form1.ActiveForm.Width += 5;
                Form1.ActiveForm.Height += 30;
                Button button = new Button();


                if (i % dimension == 0)
                {
                    widthgrid = 10;
                    heightgrid = heightgrid + 50;
                }
                else
                {
                    widthgrid = widthgrid + 100;
                }

                button.Location = new Point(widthgrid, heightgrid);
                button.Text = "(" + i / dimension + "," + i % dimension + ")";
                button.Width = 100;
                button.Height = 50;
                button.BackColor = Color.Orange;

                button.Click += (s, a) =>
                {
                    string clusters_data = "";

                    button.Text = button.Text.Replace("(", "");
                    button.Text = button.Text.Replace(")", "");
                    List<string> instances = new List<string>();
                    for (int j = 0; j < clusters.Length; j++)
                    {
                        if (clusters[j] == button.Text)
                        {

                            Console.WriteLine(fileContent[j]);
                            if (form2 != null)
                                form2.Close();


                            instances.Add(fileContent[j]);
                            form2 = new Form2(instances);
                            form2.Text = "Instances in " + button.Text;

                            form2.Show();
                        }
                    }
                };
                this.Controls.Add(button);
            }




        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dimension = Convert.ToInt32(textBox2.Text);
        }


       

    }
    public partial class Form2 : Form
    {
        public Form2(List<string> clusterData)
        {
            this.Height = 250;
            this.Width = 250;
            
            TextBox textBox = new TextBox();

            textBox.ReadOnly = true;
            textBox.Multiline = true;
            
            textBox.Width = 150;
            textBox.Height = 150;
            foreach (string data in clusterData)
            {
                textBox.Text += data + "\r\n";
            }



            textBox.Location = new Point(30, 30);
            this.Controls.Add(textBox);
        }
    }

}
