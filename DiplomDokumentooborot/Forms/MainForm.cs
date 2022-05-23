using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiplomDokumentooborot.Forms;
using FontAwesome.Sharp;
using static DiplomDokumentooborot.Forms.FormZakazi;

namespace DiplomDokumentooborot
{//TESTTWATATWATATWATWATWATWATA
    //proverka
    public partial class MainForm : Form
    {

        //поля дизайна
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public MainForm()
        {
            InitializeComponent();
            //конструктор дизайна
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(10, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            //form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        //методы дизайна
        private void ActivateButton(object senderBtn, Color color)
        {
            if(senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //левые кнопки
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //icon current childform
                iconPictureBox1.IconChar = currentBtn.IconChar;
                iconPictureBox1.IconColor = color;
            }
        }
        private void DisableButton()
        {
            if(currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if(currentChildForm != null)
            {
                //open onlyform
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;

            
        }
        void Reload(string param)
        {
            //Здесь чего нибудь делаем.
            //Это непосредственно то что выполнится по событию.
        }
        public void ManagerRole(int role)
        {
            

            FormZakazi form = new FormZakazi();
           
            switch (role)
            {
                
                //И в зависимости от того, какая роль (цифра) хранится в поле класса и передана в метод, показываются те или иные кнопки.
               
                case 1:
                    iconButton5.Hide();

                    break;
                case 2:
                    iconButton1.Enabled = false;
                    iconButton1.BackColor = Color.Transparent;
                    iconButton1.Text = "Недоступно!";
                    iconButton3.Hide();
                    
                   


                    break;
                //Еси по какой то причине в классе ничего не содержится, то всё отключается вообще
                default:
                    label1.Text = "Неопределённый";
                    label1.ForeColor = Color.Red;
                    break;
            }
        }

        private void TestAuth_Load(object sender, EventArgs e)
        {

            timer1.Interval = 10;
            timer1.Enabled = true;
           timer1.Start();
            //Сокрытие текущей формы
            this.Hide();
            //Инициализируем и вызываем форму диалога авторизации
            Authorization authh = new Authorization();
            //Вызов формы в режиме диалога
            authh.ShowDialog();
            //Если авторизации была успешна и в поле класса хранится истина, то делаем движуху:
            if (Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                //Вытаскиваем из класса поля в label'ы
               
                label2.Text = Auth.auth_fio;
                label5.Text = "Авторизация успешна!";
                //Красим текст в label в зелёный цвет
                label5.ForeColor = Color.Green;
                //Вызываем метод управления ролями
                ManagerRole(Auth.auth_role);
            }
            //иначе
            else
            {
                //Закрываем форму
                this.Close();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            
            OpenChildForm(new FormStaff());
            lblTitleChildForm.Text = "Сотрудники";
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            
            OpenChildForm(new FormZakazi());
            lblTitleChildForm.Text = "Заявки";
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            
            OpenChildForm(new FormOtcheti());
            lblTitleChildForm.Text = "Документы";
        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {
            DisableButton();
        }

        private void lblTitleChildForm_Click(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconPictureBox1.IconChar = IconChar.Home;
            iconPictureBox1.IconColor = Color.MediumPurple;
            lblTitleChildForm.Text = "Главная";
        }
        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        //dragForm
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,int wMsg, int wParam,int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            Reset();
            try
            {
                if(currentChildForm != null)
                {
                    currentChildForm.Hide();
                }
            }
            catch (Exception ex)
            {
               
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToLongTimeString();
            label7.Text = DateTime.Now.ToLongDateString();
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconPictureBox4_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        
        private void iconButton4_Click(object sender, EventArgs e)
        {


            System.Diagnostics.Process.Start(Application.ExecutablePath); 
            this.Close(); 
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);

            OpenChildForm(new OtchetiSotr());
            lblTitleChildForm.Text = "Документы";
        }
    }
}
