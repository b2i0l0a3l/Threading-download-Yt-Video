using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadingDownloadVideos.Global
{
    public class CustomNotificaionEventArgs : EventArgs
    {
        public readonly bool IsOk;
        public CustomNotificaionEventArgs(bool isOk)
        {
            this.IsOk = isOk;
        }
    }
    public class Notification
    {
        INotification _notification;
        public Notification(INotification notification)
        {
            _notification = notification;
        }
        public void Show(string Content, string title)
        {
            _notification.Show(Content,title);
        }
    }
    public interface INotification
    {
        void Show(string Content,string title);
    }
    public class MessageBoxNotification : INotification
    {
        private event EventHandler<CustomNotificaionEventArgs> OnOkClicked;
        public void Show(string Content, string title)
        {
            DialogResult result= MessageBox.Show(Content,title, MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK)
            {
                OnOkClicked?.Invoke(this,new CustomNotificaionEventArgs(true));
            }

        }
    }

}
