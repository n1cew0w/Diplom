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
using DiplomDokumentooborot.Forms;
using DiplomDokumentooborot.Forms.Staff;

namespace DiplomDokumentooborot.Forms
{
    public partial class FormStaff : Form
    {
        static string index_selected_rows;
        static string id_selected_rows;
        
        public class DBOperation
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


            
            public void GetListStaff(BindingSource bs1,DataGridView dg1)
            {

                //Запрос для вывода строк в БД
                string commandStr = "SELECT id AS 'ID', fio AS 'ФИО', age AS 'Возраст', position AS 'Должность', registration AS 'Место прописки' FROM staff";
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
            public void InsertStaff(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4 )
            {
                string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;";

                MySqlConnection conn = new MySqlConnection(connStr);
                //Получение новых параметров пользователя
                string new_fio = textBox1.Text;
                string new_age = textBox2.Text;
                string new_position = textBox3.Text;
                string new_registration = textBox4.Text;

                if (textBox1.Text.Length > 0)
                {
                    //Формируем строку запроса на добавление строк
                    string sql_insert_clothes = " INSERT INTO `staff` (fio, age,position,registration) " +
                        "VALUES ('" + new_fio + "', '" + new_age + "', '" + new_position + "', '" + new_registration + "')";


                    //Посылаем запрос на добавление данных
                    MySqlCommand insert_clothes = new MySqlCommand(sql_insert_clothes, conn);
                    try
                    {
                        conn.Open();
                        insert_clothes.ExecuteNonQuery();
                        MessageBox.Show("Добавление сотрудника прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка добавления сотрудника \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DBOperation DBO = new DBOperation();
                DBO.GetListStaff(bs1,dg1);
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
                string del = "DELETE FROM staff WHERE id = " + id;
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
            public void UpdateStaff(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4)
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
        }
        
       
        public FormStaff()
        {
            InitializeComponent();
        }

        private void FormSotrudniki_Load(object sender, EventArgs e)
        {
            DBOperation DBO = new DBOperation();
            DBO.GetListStaff(bindingSource1,dataGridView1);
           
 
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


       
        private void button1_Click(object sender, EventArgs e)
        {
            DBOperation DBO = new DBOperation();
            DBO.InsertStaff(textBox1,textBox2,textBox3,textBox4);
            DBO.reload_list(bindingSource1,dataGridView1);


        }
        //обновление грида
        
        
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DBOperation DBO = new DBOperation();
            DBO.GetCurrentID(dataGridView1);
            DBO.UpdateStaff(textBox1,textBox2,textBox3,textBox4);
            DBO.reload_list(bindingSource1,dataGridView1);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            string fromDGtoTB = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox1.Text =
                dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox2.Text =
                dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox3.Text =
               dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox4.Text =
               dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
        }

        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DBOperation DBO = new DBOperation();
            DBO.GetCurrentID(dataGridView1);
            DBO.DeleteStaff(Convert.ToInt32(id_selected_rows));
            DBO.reload_list(bindingSource1, dataGridView1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Установить заголовки столбцов в ячейках

            Excel.Application exApp = new Excel.Application();

            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;

            wsh.Cells[1, "A"] = "Номер сотрудника";
            wsh.Cells[1, "B"] = "ФИО";
            wsh.Cells[1, "C"] = "Возраст";
            wsh.Cells[1, "D"] = "Должность";
            wsh.Cells[1, "E"] = "Место прописки";

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
        FormDogovor Dogovor = new FormDogovor();
        private void button2_Click(object sender, EventArgs e)
        {
            Dogovor.ShowDialog();
        }

        private void FormStaff_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)

            {

                e.Cancel = true;

                Hide();

            }
        }
    }
}
