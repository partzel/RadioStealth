using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using iTunesSearch.Library;
using iTunesSearch.Library.Models;
using Avalonia.Media.Imaging;
using System.IO;

namespace RadioStealth.Models
{
    public class Album
    {
        private static iTunesSearchManager _searchManager = new();
        private static HttpClient _httpClient = new();
        private string CachePath => $"./Cache/{Artist}-{Name}";

        public string Artist { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }

        public Album(string artist, string name, string coverUrl)
        {
            Artist = artist;
            Name = name;
            CoverUrl = coverUrl;
        }

        public static async Task<IEnumerable<Album>> SearchAlbums(string searchTerm)
        {
            AlbumResult query;
            IEnumerable<Album> results;

            try
            {
                query = await _searchManager.GetAlbumsAsync(searchTerm);
                results = query.Albums.Select(a =>
                new Album(a.ArtistName, a.CollectionName, a.ArtworkUrl100.Replace("100x100bb", "600x600bb")));

            }
            catch (HttpRequestException)
            {
                results = new List<Album>();
            }

            return results;
        }


        public async Task<Stream> LoadCoverBitmapAsync()
        {
            if (File.Exists(CachePath + ".bmp"))
            {
                return File.OpenRead(CachePath + ".bmp");
            }
            else
            {
                var data = await _httpClient.GetByteArrayAsync(CoverUrl);
                return new MemoryStream(data);
            }
        }
    }
}
