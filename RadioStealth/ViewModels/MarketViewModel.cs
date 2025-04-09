using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;
using System.Threading;
using System.Threading.Tasks;
using RadioStealth.Models;
using System.Linq;

namespace RadioStealth.ViewModels
{
	public class MarketViewModel : ViewModelBase
	{
		private string? _search;
		private bool _isBusy;
		private AlbumViewModel? _selectedAlbum;
		private CancellationTokenSource? _cancellationTokenSource;

        public string? Search { 
			get => _search;
			set
			{
				this.RaiseAndSetIfChanged(ref _search, value);
			} 
		}

		public bool IsBusy
		{
			get => _isBusy;
			set
			{
				this.RaiseAndSetIfChanged(ref _isBusy, value);
			}
		}

		public ObservableCollection<AlbumViewModel> AlbumList { get; } = new();

        public AlbumViewModel? SelectedAlbum {
			get => _selectedAlbum;
			set
			{
				this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
			}
		}

        public MarketViewModel()
        {
			this.WhenAnyValue(m => m.Search)
				.Throttle(TimeSpan.FromMilliseconds(400))
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(DoSearch!);
        }

		public async void DoSearch(string search)
		{
			IsBusy = true;
			AlbumList.Clear();

			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource = new();
			var _cancellationToken = _cancellationTokenSource.Token;

			if (!string.IsNullOrEmpty(search))
			{
				var albums = await Album.SearchAlbums(search);

				foreach (var album in albums)
					AlbumList.Add(new AlbumViewModel(album));

                if (!_cancellationToken.IsCancellationRequested)
                {
                    LoadCovers(_cancellationToken);
                }
			}


			IsBusy = false;
		}

		private async void LoadCovers(CancellationToken cancellationToken)
		{
			foreach (var album in AlbumList.ToList())
			{
				await album.LoadCover();
                if (cancellationToken.IsCancellationRequested)
                {
					return;
                }
			}
		}
    }
}