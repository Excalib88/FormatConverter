using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FormatConverter.Utils
{
    public static class FileHelper
    {
        public static async Task<byte[]> Download(string link)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(link);

                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsByteArrayAsync();
                    }

                    throw new InvalidDataException("Cannot download the file");
                }
            }
        }

        public static string ChangeFileExtension(string fileName)
        {
            return fileName.Replace(".docx", ".pdf");
        }
    }
}