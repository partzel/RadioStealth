using RadioStealth.Models;
using ReactiveUI;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using System.Threading;

namespace RadioStealth.ViewModels
{
	public class AlbumViewModel : ViewModelBase
	{
		private Album _album;
        private Bitmap? _cover;

        public Bitmap? Cover
        {
            get => _cover;
            set
            {
                this.RaiseAndSetIfChanged(ref _cover, value);
            }
        }

        public AlbumViewModel(Album album)
        {
            _album = album;
        }

        public string Artist => _album.Artist;
        public string Name => _album.Name;

        public async Task LoadCover()
        {
            await using (var imageStream = await _album.LoadCoverBitmapAsync())
            {
                Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
            }
        }
    }
}