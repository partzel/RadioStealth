using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadioStealth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand OpenMarket { get; }
        public Interaction<MarketViewModel, AlbumViewModel?> ShowDialog { get; }

        public MainWindowViewModel()
        {
            ShowDialog = new();
            OpenMarket = ReactiveCommand.CreateFromTask(async () =>
            {
                var market= new MarketViewModel();

                var result = await ShowDialog.Handle(market);
            });
        }
    }
}
