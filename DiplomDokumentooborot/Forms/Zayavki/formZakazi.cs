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

namespace DiplomDokumentooborot.Forms
{
    public partial class FormZakazi : Form
    {

        AddZakaz Add = new AddZakaz();
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
                string commandStr = "SELECT id AS 'ID',date AS Дата,  topic AS 'Тема', type_problem AS 'Тип проблемы', status AS 'Статус', ispoln AS 'Исполнитель'  FROM applications";
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
            public void UpdateApplications(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4)
            {
                //Получаем ID пользователя
                string id = class_edit_user.id;
                string SQL_izm = "UPDATE staff SET fio=N'" + textBox1.Text + "', age=N'" + textBox2.Text + "', position=N'" + textBox3.Text + "'," +
                    " registration=N'" + textBox4.Text + "' where id=" + id;
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
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            public void setStatusNew(BindingSource bindingSource1,DataGridView dataGridView1)
            {
                //Получаем ID пользователя
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //string id = class_edit_user.id;
                string SQL_izm = "UPDATE applications SET status= 'Новая' where id=" + id;
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
                string SQL_izm = "UPDATE applications SET status= 'Отменена' where id=" + id;
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
        }


        public FormZakazi()
        {
           

            InitializeComponent();
        }

        private void FormZakazi_Load(object sender, EventArgs e)
        {
            
            Orders orders = new Orders();
            orders.GetListOrders(bindingSource1, dataGridView1);
            orders.GetComboBoxList(comboBox1);

            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;

            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 10;
            dataGridView1.Columns[2].FillWeight = 10;
            dataGridView1.Columns[3].FillWeight = 10;
            dataGridView1.Columns[4].FillWeight = 10;

            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
            
        }
        
        public void button1_Click(object sender, EventArgs e)
        {
            Add.Show();
            Orders orders = new Orders();
            orders.reload_list(bindingSource1, dataGridView1);
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
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Новая")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Отменена")
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
            wsh.Cells[1, "D"] = "Тип проблемы";
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

        }
    }
}
