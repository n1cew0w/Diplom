using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace DiplomDokumentooborot.Forms
{
    public partial class AddZakaz : Form
    {
        string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;";
        MySqlConnection conn;
        public class OrdersAdd
        {
            FormZakazi form = new FormZakazi();
            
           
            public void InsertApp(DateTimePicker dateTimePicker1, TextBox textBox2, ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3,RichTextBox richTextBox1)
            {
                string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;";

                MySqlConnection conn = new MySqlConnection(connStr);
                //Получение новых параметров пользователя
                string new_date = dateTimePicker1.Text;
                string new_topic = textBox2.Text;
                string new_typeProblem = comboBox1.Text;
                string new_status = comboBox2.Text;
                string new_ispoln = comboBox3.Text;
                string message = richTextBox1.Text;

                if (textBox2.Text.Length > 0)
                {
                    //Формируем строку запроса на добавление строк
                    string sql_insert_clothes = " INSERT INTO `applications` (date, topic,type_problem,status,ispoln,message) " +
                        "VALUES ('" + new_date + "', '" + new_topic + "', '" + new_typeProblem + "', '" + new_status + "', '" + new_ispoln + "', '" + message + "')";


                    //Посылаем запрос на добавление данных
                    MySqlCommand insert_clothes = new MySqlCommand(sql_insert_clothes, conn);
                    try
                    {
                        conn.Open();
                        insert_clothes.ExecuteNonQuery();
                        MessageBox.Show("Добавление заявки прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка добавления заявки \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка!", "Информация");
                }
            }
            public void GetComboBoxList(ComboBox comboBox3)
            {
                MySqlConnection conn = new MySqlConnection("server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;");
                //Формирование списка статусов
                DataTable list_stud_table = new DataTable();
                MySqlCommand list_stud_command = new MySqlCommand();
                //Открываем соединение
                conn.Open();
                //Формируем столбцы для комбобокса списка ЦП
                list_stud_table.Columns.Add(new DataColumn("id", System.Type.GetType("System.Int32")));
                list_stud_table.Columns.Add(new DataColumn("fio", System.Type.GetType("System.String")));
                //Настройка видимости полей комбобокса
                comboBox3.DataSource = list_stud_table;
                comboBox3.DisplayMember = "fio";
                comboBox3.ValueMember = "id";
                //Формируем строку запроса на отображение списка статусов прав пользователя
                string sql_list_users = "SELECT id, fio FROM staff";
                list_stud_command.CommandText = sql_list_users;
                list_stud_command.Connection = conn;
                //Формирование списка ЦП для combobox'a
                MySqlDataReader list_stud_reader;
                try
                {
                    //Инициализируем ридер
                    list_stud_reader = list_stud_command.ExecuteReader();
                    while (list_stud_reader.Read())
                    {
                        DataRow rowToAdd = list_stud_table.NewRow();
                        rowToAdd["id"] = Convert.ToInt32(list_stud_reader[0]);
                        rowToAdd["fio"] = list_stud_reader[1].ToString();
                        list_stud_table.Rows.Add(rowToAdd);
                    }
                    list_stud_reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    conn.Close();
                }
            }
            public async void GetSomeFiles(ComboBox comboBox1)
            {
                //https://localhost:1337/callback#access_token=AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4&token_type=bearer&expires_in=31536000
                var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
                var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/Sotrudnik" });
                foreach (var item in roodFolderData.Embedded.Items)
                {
                    comboBox1.Items.Add($"{item.Name}");
                }
            }

        }
        
        public AddZakaz()
        {
            InitializeComponent();
        }

        private void AddZakaz_Load(object sender, EventArgs e)
        {
            OrdersAdd orders = new OrdersAdd();
            conn = new MySqlConnection(connStr);
            orders.GetComboBoxList(comboBox3);
            orders.GetSomeFiles(comboBox1);


        }
        FormZakazi f = new FormZakazi();


        private void button1_Click(object sender, EventArgs e)
        {
            
               OrdersAdd orders = new OrdersAdd();
            orders.InsertApp(dateTimePicker1, textBox2, comboBox1, comboBox2, comboBox3,richTextBox1);
            this.Close();
            
            
            
            
            
        }
    }
}
