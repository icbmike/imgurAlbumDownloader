using System;
using System.Collections.Generic;
using CommandLine;

namespace ImgurAlbumDownload
{
    class Options
    {
        [Option]
        public String ClientId { get; set; }

        [Option]
        public String AlbumId { get; set; }

        [Option]
        public String DownloadLocation { get; set; }

        public static Options Parse(string[] args)
        {
            var options = new Options();

            Parser.Default.ParseArguments(args, options);

            return options;
        }
    }
}