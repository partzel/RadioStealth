using Avalonia.Controls;
using Avalonia.ReactiveUI;
using RadioStealth.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace RadioStealth.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }

        public async Task DoShowDialogAsync(InteractionContext<MarketViewModel, AlbumViewModel?> interaction)
        {
            var dialog = new MarketWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<AlbumViewModel?>(this);
            interaction.SetOutput(result);
        }
    }
}