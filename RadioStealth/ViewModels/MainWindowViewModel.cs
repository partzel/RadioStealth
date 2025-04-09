using DynamicData.Binding;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadioStealth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand OpenMarket { get; }
        public Interaction<MarketViewModel, AlbumViewModel?> ShowDialog { get; }
        public ObservableCollection<AlbumViewModel> Albums { get;} = new();

        public MainWindowViewModel()
        {
            ShowDialog = new();
            OpenMarket = ReactiveCommand.CreateFromTask(async () =>
            {
                var market= new MarketViewModel();
                var result = await ShowDialog.Handle(market);

                if (result is not null)
                {
                    Albums.Add(result);
                }
            });
        }
    }
}
