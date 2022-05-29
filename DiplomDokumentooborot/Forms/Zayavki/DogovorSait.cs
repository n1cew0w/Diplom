using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomDokumentooborot.Forms.Zayavki
{
    public partial class DogovorSait : Form
    {
        public DogovorSait()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var helper = new Staff.WordHelper("dogovor-sayt.docx");

            var items = new Dictionary<string, string>
            {
                { "<KLIENT>", textBox1.Text },
                { "<PRICE>" , textBox2.Text },
                { "<PRICE2>" , textBox3.Text },
                

            };

            helper.Process(items);
            MessageBox.Show("Договор на создание сайта сохранен в корневую папку!");
            this.Close();
        }

        private void DogovorSait_Load(object sender, EventArgs e)
        {
            textBox1.Text = SomeClass.variable_class1;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
