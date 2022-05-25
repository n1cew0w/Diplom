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
using Excel = Microsoft.Office.Interop.Excel;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;
using System.IO;

namespace DiplomDokumentooborot.Forms
{
    public partial class FormZakazi : Form
    {
        

        
        
        static string index_selected_rows;
        static string id_selected_rows;
        public class Orders
        {
            //Переменная соединения

            MySqlConnection conn = new MySqlConnection("server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;");


            DataTable dt = new DataTable();
            BindingSource bs = new BindingSource();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            private MySqlDataAdapter MyDA = new MySqlDataAdapter();
            //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
            private BindingSource bSource = new BindingSource();
            private DataSet ds = new DataSet();
            //Представляет одну таблицу данных в памяти.
            private DataTable table = new DataTable();



            public void GetListOrders(BindingSource bs1, DataGridView dg1)
            {

                //Запрос для вывода строк в БД
                string commandStr = "SELECT id AS 'ID',date AS Дата,  topic AS 'Тема', type_problem AS 'Документ', status AS 'Статус', ispoln AS 'Исполнитель', message AS 'Исполнитель'  FROM applications";
                //Открываем соединение
                conn.Open();
                //Объявляем команду, которая выполнить запрос в соединении conn
                MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
                //Заполняем таблицу записями из БД
                MyDA.Fill(table);
                //Указываем, что источником данных в bindingsource является заполненная выше таблица
                bs1.DataSource = table;
                //Указываем, что источником данных ДатаГрида является bindingsource
                dg1.DataSource = bs1;
                //Закрываем соединение
                conn.Close();
            }
            public void InsertApp(DateTimePicker dateTimePicker1, TextBox textBox1, ComboBox comboBox2, ComboBox comboBox3, ComboBox comboBox4, RichTextBox richTextBox2)
            {
                string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;";

                MySqlConnection conn = new MySqlConnection(connStr);
                //Получение новых параметров пользователя
                string new_date = dateTimePicker1.Text;
                string new_topic = textBox1.Text;
                string new_typeProblem = comboBox2.Text;
                string new_status = comboBox3.Text;
                string new_ispoln = comboBox4.Text;
                string message = richTextBox2.Text;

                if (textBox1.Text.Length > 0)
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
            public void reload_list(BindingSource bs1, DataGridView dg1)
            {
                //Чистим виртуальную таблицу
                table.Clear();
                //Вызываем метод получения записей, который вновь заполнит таблицу
                Orders orders = new Orders();
                orders.GetListOrders(bs1, dg1);
            }
            public void GetCurrentID(DataGridView dataGridView1)
            {
                index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
                // MessageBox.Show("Индекс выбранной строки" + index_selected_rows);
                id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
                // MessageBox.Show("Содержимое поля Код, в выбранной строке" + id_selected_rows);
                class_edit_user.id = id_selected_rows;
            }
            public void DeleteStaff(int id)
            {
                string del = "DELETE FROM applications WHERE id = " + id;
                MySqlCommand del_staff = new MySqlCommand(del, conn);

                try
                {
                    conn.Open();
                    del_staff.ExecuteNonQuery();
                    //this.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления пользователя \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            public void UpdateApplications(TextBox textBox1, DateTimePicker dateTimePicker1, ComboBox comboBox2, ComboBox comboBox3, ComboBox comboBox4,RichTextBox richTextBox2)
            {
                //Получаем ID пользователя
                string id = class_edit_user.id;
                string SQL_izm = "UPDATE applications SET date=N'" + dateTimePicker1.Text + "', topic=N'" + textBox1.Text + "', type_problem=N'" + comboBox2.Text + "'," +
                    "status=N'" + comboBox3.Text + "'," +
                    "ispoln=N'" + comboBox4.Text + "'," +
                    "message=N'" + richTextBox2.Text + "' where id=" + id;
                MessageBox.Show(SQL_izm);
                MySqlConnection conn = new MySqlConnection("server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;");
                conn.Open();
                MySqlCommand command1 = new MySqlCommand(SQL_izm, conn);
                MySqlDataReader dr = command1.ExecuteReader();
                dr.Close();
                conn.Close();
                MessageBox.Show("Данные изменены");
                //this.Activate();
                textBox1.Text = "";

            }
            public void setStatusNew(BindingSource bindingSource1,DataGridView dataGridView1)
            {
                //Получаем ID пользователя
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //string id = class_edit_user.id;
                string SQL_izm = "UPDATE applications SET status= 'Не обработанный' where id=" + id;
                MessageBox.Show(SQL_izm);
                MySqlConnection conn = new MySqlConnection("server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;");
                conn.Open();
                MySqlCommand command1 = new MySqlCommand(SQL_izm, conn);
                MySqlDataReader dr = command1.ExecuteReader();
                dr.Close();
                conn.Close();
                MessageBox.Show("Данные изменены");
                //this.Activate();

                Orders orders = new Orders();
                orders.reload_list(bindingSource1, dataGridView1);
            }
            public void setStatusOtmena(BindingSource bindingSource1, DataGridView dataGridView1)
            {
                //Получаем ID пользователя
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //string id = class_edit_user.id;
                string SQL_izm = "UPDATE applications SET status= 'Нужна печать' where id=" + id;
                MessageBox.Show(SQL_izm);
                MySqlConnection conn = new MySqlConnection("server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;");
                conn.Open();
                MySqlCommand command1 = new MySqlCommand(SQL_izm, conn);
                MySqlDataReader dr = command1.ExecuteReader();
                dr.Close();
                conn.Close();
                MessageBox.Show("Данные изменены");
                //this.Activate();

                Orders orders = new Orders();
                orders.reload_list(bindingSource1, dataGridView1);
            }
            public void setStatusWork(BindingSource bindingSource1, DataGridView dataGridView1)
            {
                //Получаем ID пользователя
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //string id = class_edit_user.id;
                string SQL_izm = "UPDATE applications SET status= 'В работе' where id=" + id;
                MessageBox.Show(SQL_izm);
                MySqlConnection conn = new MySqlConnection("server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;");
                conn.Open();
                MySqlCommand command1 = new MySqlCommand(SQL_izm, conn);
                MySqlDataReader dr = command1.ExecuteReader();
                dr.Close();
                conn.Close();
                MessageBox.Show("Данные изменены");
                //this.Activate();

                Orders orders = new Orders();
                orders.reload_list(bindingSource1, dataGridView1);
            }
            public void setStatusСompleted(BindingSource bindingSource1, DataGridView dataGridView1)
            {
                //Получаем ID пользователя
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //string id = class_edit_user.id;
                string SQL_izm = "UPDATE applications SET status= 'Выполнена' where id=" + id;
                MessageBox.Show(SQL_izm);
                MySqlConnection conn = new MySqlConnection("server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;");
                conn.Open();
                MySqlCommand command1 = new MySqlCommand(SQL_izm, conn);
                MySqlDataReader dr = command1.ExecuteReader();
                dr.Close();
                conn.Close();
                MessageBox.Show("Данные изменены");
                //this.Activate();

                Orders orders = new Orders();
                orders.reload_list(bindingSource1, dataGridView1);
            }
            public void setStatusNotСompleted(BindingSource bindingSource1, DataGridView dataGridView1)
            {
                //Получаем ID пользователя
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //string id = class_edit_user.id;
                string SQL_izm = "UPDATE applications SET status= 'Не выполнена' where id=" + id;
                MessageBox.Show(SQL_izm);
                MySqlConnection conn = new MySqlConnection("server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;");
                conn.Open();
                MySqlCommand command1 = new MySqlCommand(SQL_izm, conn);
                MySqlDataReader dr = command1.ExecuteReader();
                dr.Close();
                conn.Close();
                MessageBox.Show("Данные изменены");
                //this.Activate();

                Orders orders = new Orders();
                orders.reload_list(bindingSource1, dataGridView1);
            }
            public void GetComboBoxList(ComboBox comboBox1)
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
                comboBox1.DataSource = list_stud_table;
                comboBox1.DisplayMember = "fio";
                comboBox1.ValueMember = "id";
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
            public void GetComboBoxListChange(ComboBox comboBox4)
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
                comboBox4.DataSource = list_stud_table;
                comboBox4.DisplayMember = "fio";
                comboBox4.ValueMember = "id";
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
        }
        //public void UpdateData(BindingSource bindingSource1, DataGridView dataGridView1)
        //{
        //    DataTable table = new DataTable();
        //table.Clear();
        //    //Вызываем метод получения записей, который вновь заполнит таблицу
        //    Orders orders = new Orders();
        //    orders.GetListOrders(bindingSource1, dataGridView1);
        //}

        public FormZakazi()
        {
            InitializeComponent();
        }
        class YandexDisk
        {
            
            public async void GetSomeFilesComboBox(ComboBox comboBox2)
            {
                //https://localhost:1337/callback#access_token=AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4&token_type=bearer&expires_in=31536000
                var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
                var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/DownloadFolder" });
                foreach (var item in roodFolderData.Embedded.Items)
                {
                    comboBox2.Items.Add($"{item.Name}");
                }
            }
            public async void SetStatus(ListView listView)
            {
                var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
                var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/DownloadFolder" });
                
                
                

            }

            
           
        }

        private void FormZakazi_Load(object sender, EventArgs e)
        {
            YandexDisk yandexDisk = new YandexDisk();
            yandexDisk.GetSomeFilesComboBox(comboBox2);
             
            HideControls();
            button8.Hide();
            Orders orders = new Orders();
            orders.GetListOrders(bindingSource1, dataGridView1);
            orders.GetComboBoxList(comboBox1);
            orders.GetComboBoxListChange(comboBox4);

            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[6].Visible = false;


            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 10;
            dataGridView1.Columns[2].FillWeight = 10;
            dataGridView1.Columns[3].FillWeight = 10;
            dataGridView1.Columns[4].FillWeight = 10;
            dataGridView1.Columns[5].FillWeight = 10;

            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
            
        }
        public void restart()
        {
            Orders orders = new Orders();
            orders.GetListOrders(bindingSource1, dataGridView1);

        }
        public void button1_Click(object sender, EventArgs e)
        {
            //AddZakaz Add = new AddZakaz();
            //Add.Show();
            //this.Close();
            //Add.Focus();
           // Orders orders = new Orders();
            //orders.reload_list(bindingSource1, dataGridView1);
            ShowControls();
            button7.Hide();
            button8.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.reload_list(bindingSource1, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.GetCurrentID(dataGridView1);
            orders.DeleteStaff(Convert.ToInt32(id_selected_rows));
            orders.reload_list(bindingSource1, dataGridView1);
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /*Новая
            Отменена
            В работе
            Выполнена
            Не выполнена*/
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Статус" && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Не обработанный")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Нужна печать")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "В работе")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Выполнена")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Не выполнена")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                }
            }
                
        }

        private void новаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.setStatusNew(bindingSource1, dataGridView1);
        }

        private void вРаботеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.setStatusWork(bindingSource1, dataGridView1);
        }

        private void отмененаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.setStatusOtmena(bindingSource1, dataGridView1);
        }

        private void выполненаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.setStatusСompleted(bindingSource1, dataGridView1);
        }

        private void неВыполненаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.setStatusNotСompleted(bindingSource1, dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Установить заголовки столбцов в ячейках

            Excel.Application exApp = new Excel.Application();

            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;

            wsh.Cells[1, "A"] = "Номер ";
            wsh.Cells[1, "B"] = "Дата";
            wsh.Cells[1, "C"] = "Тема";
            wsh.Cells[1, "D"] = "Документ";
            wsh.Cells[1, "E"] = "Статус";
            wsh.Cells[1, "F"] = "Исполнитель";

            int i, j;
            for (i = 0; i <= dataGridView1.RowCount - 2; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    wsh.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString();
                }
            }



            exApp.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = "Исполнитель LIKE'" + comboBox1.Text + "%'";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            Orders orders = new Orders();
            orders.GetListOrders(bindingSource1, dataGridView1);
            bindingSource1.Filter = "Исполнитель LIKE'" + comboBox1.Text + "%'";

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.Text =
                dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();
            richTextBox2.Text =
                dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();
            string idDataGrid = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            dateTimePicker1.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox1.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            comboBox2.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            comboBox3.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            comboBox4.Text = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
        }

        private void FormZakazi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)

            {

                e.Cancel = true;

                Hide();

            }
        }
        public void ShowControls()
        {
            textBox1.Show();
            comboBox2.Show();
            comboBox3.Show();
            comboBox4.Show();
            richTextBox2.Show();
            dateTimePicker1.Show();
            button7.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            label8.Show();
        }
        public void HideControls()
        {
            textBox1.Hide();
            comboBox2.Hide();
            comboBox3.Hide();
            comboBox4.Hide();
            dateTimePicker1.Hide();
            richTextBox2.Hide();
            button7.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ShowControls();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.GetCurrentID(dataGridView1);
            orders.UpdateApplications(textBox1,dateTimePicker1,comboBox2,comboBox3,comboBox4,richTextBox2);
            orders.reload_list(bindingSource1, dataGridView1);
            HideControls();
            button8.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.InsertApp(dateTimePicker1, textBox1, comboBox2, comboBox3, comboBox4, richTextBox1);
            orders.reload_list(bindingSource1, dataGridView1);
            HideControls();
            button8.Hide();
        }
    }
}
