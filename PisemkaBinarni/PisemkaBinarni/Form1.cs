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

namespace PisemkaBinarni
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool neco = false;

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream data = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            try
            {
                neco = false;
                BinaryWriter zapis = new BinaryWriter(data, Encoding.GetEncoding("windows-1250"));

                int celkovypocetznamek = (int)numericUpDown1.Value;


                int[] vaha = Array.ConvertAll(textBox4.Text.Split(','), int.Parse);
                int[] znamka = Array.ConvertAll(textBox5.Text.Split(','), int.Parse);


                if (vaha.Length == celkovypocetznamek && znamka.Length == celkovypocetznamek)
                {
                    bool isValid = true;
                    for (int i = 0; i < vaha.Length; i++)
                    {
                        if (vaha[i] < 1 || vaha[i] > 5 || znamka[i] < 1 || znamka[i] > 5)
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        zapis.Write(celkovypocetznamek);
                        for (int i = 0; i < vaha.Length; i++)
                        {
                            zapis.Write(vaha[i]);
                            zapis.Write(znamka[i]);
                        }
                        zapis.Write(textBox1.Text);
                        zapis.Write(textBox2.Text);
                        neco = true;
                    }
                    else
                    {
                        MessageBox.Show("Váha a známka musí být v rozmezí 1 až 5");
                    }
                }
                else
                {
                    MessageBox.Show("Chyba");
                    
                }



                numericUpDown1.Value = 1;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                data.Close();

                if (neco == true)
                {
                    button2.Enabled = true;
                    button1.Enabled = false;
                }
            }
            catch {
                data.Close();
                MessageBox.Show("Chyba!");
                if (File.Exists("seznam.dat"))
                {
                    File.Delete("seznam.dat");
                }
                this.Close();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream data = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader cti = new BinaryReader(data, Encoding.GetEncoding("windows-1250"));
            BinaryWriter zapis = new BinaryWriter(data, Encoding.GetEncoding("windows-1250"));
            double soucet = 0;
            int pocetznamek = cti.ReadInt32();
            int nahradnipocet = 0;

            for (int i = 0;i < pocetznamek;i++)
            {
                int vaha = cti.ReadInt32();
                int znamka = cti.ReadInt32();
                soucet += znamka*vaha;
                nahradnipocet += vaha;
                listBox1.Items.Add(znamka);
            }
            listBox1.Items.Add(" ");


            string celyjmeno = cti.ReadString() + " " + cti.ReadString();
            double prumer = soucet / nahradnipocet;

            switch(Math.Round(prumer))
            {
                case 1:
                    {
                        MessageBox.Show("Gratuluji k výbornému výkonu " + celyjmeno);
                        break;
                    }
                case 4:
                    {
                        MessageBox.Show("Chtělo by to přístě zlepšit!! " + celyjmeno);
                        break;
                    }
                case 5:
                    {
                        cti.BaseStream.Position = (pocetznamek * 2 * 4) + 4;
                        zapis.Write("John");
                        zapis.Write("Doe");
                        MessageBox.Show("Vycházi ti za 5 tvoje jmeno z " + celyjmeno + " je zmneneno na John Doe!!!");
                        celyjmeno = "John Doe";
                        break;
                    }

            }
            textBox3.Text += celyjmeno;
            textBox3.Text += Environment.NewLine;

            label12.Text += celyjmeno + " Tvuj Prumer je : " + Math.Round(prumer, 2) + "\n";

            data.Close();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
