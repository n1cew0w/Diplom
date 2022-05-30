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
    public partial class ProektnayaZayavka : Form
    {
        public ProektnayaZayavka()
        {
            InitializeComponent();
        }

        private void ProektnayaZayavka_Load(object sender, EventArgs e)
        {
            textBox4.Text = SomeClass.variable_class1;
            textBox3.Text = SomeClass.variable_class2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var helper = new Staff.WordHelper("obrazec_zayavki.docx");

            var items = new Dictionary<string, string>
            {
                { "<NAZVANIE>", textBox1.Text },
                { "<OBLAST>" , textBox2.Text },
                { "<AVTOR>" , textBox3.Text },
                { "<ZAKAZCHIK>" , textBox4.Text },
                { "<IDEA>" , textBox5.Text },
                { "<RESULT>" , textBox6.Text },
                { "<RESOURCE>" , textBox7.Text },
                { "<ZNANIYA>" , richTextBox1.Text },
                { "<ETAP>" , richTextBox2.Text },
                { "<RISK>" , textBox9.Text },


            };

            helper.Process(items);
            MessageBox.Show("Проектная заявка сохранена в корневую папку!");
            this.Close();
        }
    }
}
