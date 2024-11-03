using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;
using System.Threading;
using static System.Windows.Forms.LinkLabel;
using System.IO;
using ThreadingDownloadVideos.Global;
using ThreadingDownloadVideos.forms;
using System.Collections.ObjectModel;

namespace ThreadingDownloadVideos
{
    public partial class Form1 : Form
    {
        private ObservableCollection<string> url; 
        public Form1()
        {
            InitializeComponent();
        }
        private Action onDownloadComplete;

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(txtUrl.Text))
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(txtUrl, "Please enter video url!");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(txtUrl, "");

            //}
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (url.Count <= 0)
                return;
            Downloads d = new Downloads(new DownloadYoutubeVideo());
            Notification n = new Notification(new MessageBoxNotification());

            d.Subscibe(n.Show);
            foreach (string Url in url)
            {
                Thread t = new Thread(()=> d.Download(Url));
                t.Start();
            }
            
        }
        private void ShowMessageBox()
        {
            Notification n = new Notification(new MessageBoxNotification());
            n.Show("Download Completed","success");
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            frmAddVideos frmAddVideos = new frmAddVideos();
            frmAddVideos.onSave += Urls;
            frmAddVideos.ShowDialog();
        }
        private void Urls(ObservableCollection<string> urls)
        {
            url = urls; 
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            btnDownload.Focus();
        }
    }
}
