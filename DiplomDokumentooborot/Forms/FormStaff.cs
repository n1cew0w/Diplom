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

namespace DiplomDokumentooborot.Forms
{
    public partial class FormStaff : Form
    {
        
        string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_18_13;database=is_1_18_st13_VKR;password=72511715;";

        //Переменная соединения
        MySqlConnection conn;

        DataTable dt = new DataTable();
        BindingSource bs = new BindingSource();
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        string index_selected_rows;
        string id_selected_rows;
        string fromDGtoTB;
        public FormStaff()
        {
            InitializeComponent();
        }

        private void FormSotrudniki_Load(object sender, EventArgs e)
        {
            //Инициализируем соединение с БД
            conn = new MySqlConnection(connStr);
            //Вызываем метод для заполнение дата Грида
            GetListStaff();
            //Видимость полей в гриде
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

        private void GetListStaff()
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
            bindingSource1.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource
            dataGridView1.DataSource = bindingSource1;
            //Закрываем соединение
            conn.Close();
        }
        private void InsertStaff()
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
                    "VALUES ('" + new_fio+ "', '" + new_age + "', '" + new_position + "', '" + new_registration + "')";


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
        private void button1_Click(object sender, EventArgs e)
        {
            InsertStaff();
            reload_list();
            

        }
        //обновление грида
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListStaff();
        }
        private void GetCurrentID()
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
                this.Show();
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
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            GetCurrentID();
            //Получаем ID пользователя
            string id = class_edit_user.id;
            string SQL_izm = "UPDATE staff SET fio=N'" + textBox1.Text + "', age=N'" + textBox2.Text + "', position=N'" + textBox3.Text + "'," +
                " registration=N'" + textBox4.Text + "' where id=" + id;
            MessageBox.Show(SQL_izm);
            MySqlConnection connection1 = new MySqlConnection(connStr);
            connection1.Open();
            MySqlCommand command1 = new MySqlCommand(SQL_izm, connection1);
            MySqlDataReader dr = command1.ExecuteReader();
            dr.Close();
            connection1.Close();
            MessageBox.Show("Данные изменены");
            this.Activate();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            reload_list();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            fromDGtoTB = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
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
            GetCurrentID();
            DeleteStaff(Convert.ToInt32(id_selected_rows));
            reload_list();
        }

        
    }
}
