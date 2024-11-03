using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreadingDownloadVideos.Global;

namespace ThreadingDownloadVideos.forms
{
    public partial class frmAddVideos : Form
    {
        public delegate void SaveList(ObservableCollection<string> urls);
        public event SaveList onSave;

        private ObservableCollection<string> Urls = new ObservableCollection<string>();

        public frmAddVideos()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUrl.Text))
                return;

            Urls.CollectionChanged += ShowMessage;
            Urls.Add(txtUrl.Text);
            
        }
        private void ShowMessage(object sender,NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                    Notification n = new Notification(new MessageBoxNotification());
                    n.Show("An Items Add","Success");
                lblLength.Text = Urls.Count.ToString();
                    txtUrl.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            onSave?.Invoke(Urls);
            this.Close();
        }
    }
}
