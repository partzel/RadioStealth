using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace RadioStealth.ViewModels
{
	public class MarketViewModel : ViewModelBase
	{
		private string? _search;
		private bool _isBusy;
		private AlbumViewModel? _selectedAlbum;

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
			AlbumList.Add(new AlbumViewModel());
			AlbumList.Add(new AlbumViewModel());
			AlbumList.Add(new AlbumViewModel());
			AlbumList.Add(new AlbumViewModel());
        }
    }
}