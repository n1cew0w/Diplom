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
    public partial class DogovorPO : Form
    {
        public DogovorPO()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var helper = new Staff.WordHelper("dogovor-po.docx");

            var items = new Dictionary<string, string>
            {
                { "<KLIENT>", textBox1.Text },
                { "<PRICE>" , textBox2.Text },
                { "<PRICE2>" , textBox3.Text },


            };

            helper.Process(items);
            MessageBox.Show("Договор на создание программного обеспечения сохранен в корневую папку!");
            this.Close();
        }

        private void DogovorPO_Load(object sender, EventArgs e)
        {
            textBox1.Text = SomeClass.variable_class1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }
    }
}
