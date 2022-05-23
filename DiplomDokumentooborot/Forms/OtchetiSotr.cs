using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace DiplomDokumentooborot.Forms
{
    public partial class OtchetiSotr : Form
    {
        public OtchetiSotr()
        {
            InitializeComponent();
        }
        DiskHttpApi api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");

        public async void GetSomeFiles()
        {
            //https://localhost:1337/callback#access_token=AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4&token_type=bearer&expires_in=31536000
            var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
            var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/Sotrudnik" });
            foreach (var item in roodFolderData.Embedded.Items)
            {
                listView2.Items.Add($"{item.Name}");
            }
        }
        public async void GetDwnLink()
        {
            var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/DownloadFolder" });
            foreach (var item in roodFolderData.Embedded.Items)
            {

                var lnk = await api.Files.GetDownloadLinkAsync(item.Path);
                //listBox1.Items.Add(item.Name + "\t" + lnk.Href);

            }


        }
        private void OtchetiSotr_Load(object sender, EventArgs e)
        {

            GetSomeFiles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }
        public async void DownloadFile()
        {
            const string folderName = "Sotrudnik";
            var destDir = Path.Combine(Environment.CurrentDirectory, "Download");
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }
            var testFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest
            {
                Path = "/" + folderName
            });
            foreach (var item in testFolderData.Embedded.Items)
            {
                try
                {
                    string current = listView2.SelectedItems[0].Text;
                    api.Files.DownloadFileAsync(path: item.Path, Path.Combine(destDir, current));
                    var lnk = await api.Files.GetDownloadLinkAsync(item.Path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Сотруднику")
            {
                var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
                const string folderName = "Sotrudnik";
                var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/" });
                if (!roodFolderData.Embedded.Items.Any(i => i.Type == ResourceType.Dir && i.Name.Equals(folderName)))
                {
                    await api.Commands.CreateDictionaryAsync("/" + folderName);
                }

                var link = await api.Files.GetUploadLinkAsync("/" + folderName + "/" + Path.GetFileName(textBox1.Text), overwrite: false);
                using (var fs = File.OpenRead(textBox1.Text))
                {
                    await api.Files.UploadAsync(link, fs);
                }
                listView2.Clear();
                GetSomeFiles();
            }
            else
            {
                var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
                const string folderName = "DownloadFolder";
                var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/" });
                if (!roodFolderData.Embedded.Items.Any(i => i.Type == ResourceType.Dir && i.Name.Equals(folderName)))
                {
                    await api.Commands.CreateDictionaryAsync("/" + folderName);
                }

                var link = await api.Files.GetUploadLinkAsync("/" + folderName + "/" + Path.GetFileName(textBox1.Text), overwrite: false);
                using (var fs = File.OpenRead(textBox1.Text))
                {
                    await api.Files.UploadAsync(link, fs);
                }
                listView2.Clear();
                GetSomeFiles();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            DownloadFile();
            var destDir = Path.Combine(Environment.CurrentDirectory, "Download");
            System.Diagnostics.Process.Start(Environment.CurrentDirectory, destDir);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            listView2.Clear();
            GetSomeFiles();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                for (int i = listView2.Items.Count - 1; i >= 0; i--)
                {

                    var item = listView2.Items[i];
                    item.BackColor = Color.SlateBlue;
                    if (item.Text.ToLower().Contains(comboBox1.Text.ToLower()))
                    {
                        item.BackColor = Color.Green;

                    }
                    else
                    {

                    }
                }

            }
            else
            {

            }
        }
    }
}
