using CryptoGuard.MAUI.ViewModels;
using Microsoft.Maui.Controls;

namespace CryptoGuard.MAUI.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.RefreshUsername();
            viewModel.LoadDataCommand.Execute(null);
            // Favori coinler değiştiğinde CollectionView'ı yenile
            Microsoft.Maui.Controls.MessagingCenter.Subscribe<object>(this, "FavoritesChanged", (sender) =>
            {
                FavoriteCoinsCollection.ItemsSource = viewModel.FavoriteCoins;
            });
        }
    }
} 