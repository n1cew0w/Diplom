using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomDokumentooborot.Forms.Staff
{
    public partial class FormDogovor : Form
    {
        public FormDogovor()
        {
            InitializeComponent();
        }

        private void FormDogovor_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var helper = new WordHelper("blank-trudovogo-dogovora.docx");

            var items = new Dictionary<string, string>
            {
                { "<ORG>", textBox1.Text },
                { "<DIREKTOR>" , textBox2.Text },
                { "<RABOTNIK>" , textBox3.Text },
                { "<PLEXRISE> ", textBox4.Text },
                { "<DOLG> ", textBox5.Text },
                { "<OKLAD> ", textBox6.Text },
                { "<TEST>", numericUpDown1.Value.ToString() },
                { "<DATESTART>", dateTimePicker1.Value.ToString("dd.MM.yyyy") },

            };

            helper.Process(items);
            MessageBox.Show("Трудовой договор сохранен в корневую папку!");
        }
    }
}
