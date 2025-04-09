using System;
using System.Collections.Generic;
using RadioStealth.Models;
using ReactiveUI;

namespace RadioStealth.ViewModels
{
	public class AlbumViewModel : ViewModelBase
	{
		private Album _album;

        public AlbumViewModel(Album album)
        {
            _album = album;
        }

        public string Artist => _album.Artist;
        public string Name => _album.Name;
    }
}