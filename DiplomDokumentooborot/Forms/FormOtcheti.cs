using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace DiplomDokumentooborot.Forms
{
    public partial class FormOtcheti : Form
    {
        DiskHttpApi api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
        
        public async void GetSomeFiles()
        {
            //https://localhost:1337/callback#access_token=AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4&token_type=bearer&expires_in=31536000
            var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
            var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/DownloadFolder" });
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
        DeleteFileRequest DeleteFileRequest;
        
        //public async void Delete()
        //{
            
        //    var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
        //    const string folderName = "DownloadFolder";
        //    var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/" });
        //    if (!roodFolderData.Embedded.Items.Any(i => i.Type == ResourceType.Dir && i.Name.Equals(folderName)))
        //    {
        //        await api.Commands.CreateDictionaryAsync("/" + folderName);
        //    }

        //    //var link = await api.Commands.DeleteAsync("/" + folderName + "/" + Path.GetFileName(txtFile.Text) );
        //    using (var fs = File.OpenRead(txtFile.Text))
        //    {
        //        await api.Commands.DeleteAsync(link, fs);
        //    }
        //    listView2.Clear();
        //    GetSomeFiles();

        //}
        public FormOtcheti()
        {
            InitializeComponent();
            
        }
     
        
        private void FormOtcheti_Load(object sender, EventArgs e)
        {
            
            GetSomeFiles();
           
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtFile.Text = openFileDialog1.FileName;
            }

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var api = new DiskHttpApi("AQAAAAAMGeW_AAfSUTnWf4rWjUYavTHgNvrryg4");
            const string folderName = "DownloadFolder";
            var roodFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = "/" });
            if (!roodFolderData.Embedded.Items.Any(i=>i.Type==ResourceType.Dir && i.Name.Equals(folderName)))
            {
                await api.Commands.CreateDictionaryAsync("/" + folderName);
            }

            var link = await api.Files.GetUploadLinkAsync("/" + folderName + "/" + Path.GetFileName(txtFile.Text), overwrite: false);
            using (var fs = File.OpenRead(txtFile.Text))
            {
                await api.Files.UploadAsync(link, fs);
            }
            listView2.Clear();
            GetSomeFiles();
            
        }
        public async void DownloadFile()
        {
            const string folderName = "DownloadFolder";
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
        private void button3_Click(object sender, EventArgs e)
        {
            DownloadFile();
            var destDir = Path.Combine(Environment.CurrentDirectory, "Download");
            System.Diagnostics.Process.Start(Environment.CurrentDirectory,destDir);
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor | System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
    }

