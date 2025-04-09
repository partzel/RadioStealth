using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RadioStealth.ViewModels;
using System;

namespace RadioStealth;

public partial class MarketWindow : ReactiveWindow<MarketViewModel> {
    public MarketWindow()
    {
        InitializeComponent();
        this.WhenActivated(action => action(ViewModel!.AddAlbumCommand.Subscribe(Close)));
    }
}