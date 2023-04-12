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
            neco = false;
            FileStream data = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter zapis = new BinaryWriter(data, Encoding.GetEncoding("windows-1250"));

            int celkovypocetznamek = (int)numericUpDown1.Value;

            string text1 = textBox4.Text;
            string[] vaha = text1.Split(',');
            text1 = textBox5.Text;
            string[] znamka = text1.Split(',');
            /*MessageBox.Show("" + vaha.Length);
            MessageBox.Show("" + celkovypocetznamek);*/

            if (vaha.Length == celkovypocetznamek && znamka.Length == celkovypocetznamek)
            {
                zapis.Write(celkovypocetznamek);
                for(int i = 0; i<vaha.Length ;i++)
                {
                    zapis.Write(int.Parse(vaha[i]));
                    zapis.Write(int.Parse(znamka[i]));
                }
                zapis.Write(textBox1.Text);
                zapis.Write(textBox2.Text);
                neco = true;
            }
            else
            {
                MessageBox.Show("Nekde si zadal spatny pocet!!!");
            }



            numericUpDown1.Value = 0;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            data.Close();

            if(neco == true)
            {
                button2.Enabled = true;
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream data = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader cti = new BinaryReader(data, Encoding.GetEncoding("windows-1250"));
            double soucet = 0;
            int pocetznamek = cti.ReadInt32();
            int nahradnipocet = pocetznamek;

            MessageBox.Show("" + pocetznamek);
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
            textBox3.Text += celyjmeno;
            textBox3.Text += Environment.NewLine;

            double prumer = soucet / nahradnipocet;
            label12.Text += celyjmeno + " Jeho prumer je : " + Math.Round(prumer,2) + "\n";

            data.Close();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
