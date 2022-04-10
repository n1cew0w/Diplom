using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomDokumentooborot
{
    public partial class TestFTP : Form
    {
        public TestFTP()
        {
            InitializeComponent();
        }

        FtpWebRequest request = null;
        FtpWebResponse response = null;
        Stream ftpStream = null;
        int length = 1024;

        private List<string> ListFiles()
        {
            try
            {
                request = (FtpWebRequest)WebRequest.Create(txtServer.Text);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                request.Credentials = new NetworkCredential(txtUser.Text, txtPassword.Text);
                response = (FtpWebResponse)request.GetResponse();
                ftpStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(ftpStream);
                string names = reader.ReadToEnd();

                reader.Close();
                response.Close();
                request = null;

                return names.Split(new string[] { "\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            catch (Exception)
            {
                throw;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            List<string> listFiles = ListFiles();
            for(int i = 0; i < listFiles.Count; i++)
            {
                listView1.Items.Add(listFiles[i]);
            }    
            
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            Download();
        }

        private void PickFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtFile.Text = openFileDialog1.FileName;
            }
        }

        private void Download_Click(object sender, EventArgs e)
        {
            Download();
        }
        private void Download()
        {
            try
            {
                string str = listView1.SelectedItems[0].Text;
                request = (FtpWebRequest)WebRequest.Create(txtServer.Text + "/" + str);
                request.Credentials = new NetworkCredential(txtUser.Text, txtPassword.Text);
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                response = (FtpWebResponse)request.GetResponse();
                ftpStream = response.GetResponseStream();
                saveFileDialog1.FileName= str;
                DialogResult result = saveFileDialog1.ShowDialog();
                byte[] bytebuffer = new byte[length];
                int bytesRead = ftpStream.Read(bytebuffer, 0, length);
                if(result == DialogResult.OK)
                {
                    FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                    try
                    {
                        while(bytesRead > 0)
                        {
                            fs.Write(bytebuffer, 0, bytesRead);
                            bytesRead = ftpStream.Read(bytebuffer,0, length);
                        }
                        MessageBox.Show("Vse chetko", "Information", MessageBoxButtons.OK);
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                ftpStream.Close();
                response.Close();
                request = null;
            }
            catch
            {
                MessageBox.Show("Выбери файл перед загрузкой", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if(txtFile.Text != string.Empty)
            {
                FileInfo fi = new FileInfo(txtFile.Text);

                request = (FtpWebRequest)WebRequest.Create(txtServer.Text + 
                    "/" + fi.Name);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(txtUser.Text, txtPassword.Text);

                ftpStream = request.GetRequestStream();
                FileStream file = File.OpenRead(txtFile.Text);

                byte[] buffer = new byte[length];
                int bytesRead = 0;

                do
                {
                    bytesRead = file.Read(buffer, 0, length);
                    ftpStream.Write(buffer, 0, bytesRead);

                }
                while (bytesRead != 0);
                file.Close();
                ftpStream.Close();
                request = null;

                MessageBox.Show("Vse chetko", "Information", MessageBoxButtons.OK);
            }
            else
            {

            }
        }

    }
}
