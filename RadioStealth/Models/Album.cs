using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTunesSearch;
using iTunesSearch.Library;

namespace RadioStealth.Models
{
    public class Album
    {
        private static iTunesSearchManager _searchManager = new();

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
            var query = await _searchManager.GetAlbumsAsync(searchTerm);

            return query.Albums.Select(a => 
                new Album(a.ArtistName, a.CollectionName, a.CollectionViewUrl.Replace("100x100bb", "600x600bb"))
            );
        }
    }
}
