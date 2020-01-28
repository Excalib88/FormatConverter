using System;
using System.Net;
using System.Threading.Tasks;

namespace FormatConverter.Utils
{
    public static class FileHelper
    {
        public static async Task<byte[]> Download(string link)
        {
            var webClient = new WebClient();
            var fileArray = await webClient.DownloadDataTaskAsync(new Uri(link));
            
            return fileArray;
        }
    }
}