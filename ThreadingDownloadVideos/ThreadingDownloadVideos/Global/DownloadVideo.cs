using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;
using System.IO;
using System.Windows.Forms;
namespace ThreadingDownloadVideos.Global
{
    public class Downloads
    {
        private IDownload _download;

        public Downloads(IDownload download)
        {
            _download = download;
        }
        public void Download(string url)
        {
            _download.Download(url);
        }
        public void Subscibe(Action<string , string >func)
        {
            _download.Subscribe(func);
        }
    }
    public interface IDownload
    {
        void Download(string url);
        void Subscribe(Action<string, string> func);
    }
    public class DownloadYoutubeVideo : IDownload
    {
        public event Action<string,string> OnDownloadCompleted;
        private  void DownloadCompleted(string text,string video)
        {
            Action<string, string> handle = OnDownloadCompleted;
            if(handle!=null)
                handle(text,video);

        }
        public void Download(string url)
        {
            
            var youTube = YouTube.Default;
            var video = youTube.GetVideo(url);
            File.WriteAllBytes(@"D:\" + video.FullName, video.GetBytes());
            OnDownloadCompleted?.Invoke("Completed", video.FullName);
        }
        public void Subscribe(Action<string,string> func)
        {
            OnDownloadCompleted += func;
        }
       
    
}
}
