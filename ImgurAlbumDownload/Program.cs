using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ImgurAlbumDownload
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = Options.Parse(args);
            var imageLinks = ListImageLinks(options.AlbumId, options.ClientId).ToList();
            DirectoryExtensions.EnsureExists(options.DownloadLocation);

            for (var i = 0; i < imageLinks.Count; i++)
            {
                Console.Write($"\r{i / (double) imageLinks.Count * 100}%");
                DownloadImage(imageLinks[i], options.DownloadLocation);
            }
        }

        private static IEnumerable<string> ListImageLinks(string albumId, string clientId)
        {
            var webClient = new WebClient {Headers = {[HttpRequestHeader.Authorization] = $"Client-ID {clientId}"}};

            var body = webClient.DownloadString($"https://api.imgur.com/3/album/{albumId}");

            return ((JArray) JObject.Parse(body)["data"]["images"]).Select(token => token["link"].ToString());
        }

        private static void DownloadImage(string imageLink, string location)
        {
            var filename = imageLink.Split(new[] {'/'}, StringSplitOptions.None).Last();
            var webClient = new WebClient();

            webClient.DownloadFile(imageLink, Path.Combine(location, filename));
        }
    }
}