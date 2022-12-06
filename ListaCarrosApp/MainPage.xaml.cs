using ListaCarrosApp.ViewModels;

namespace ListaCarrosApp;

public partial class MainPage : ContentPage
{
	public MainPage(CarListViewModel carListViewModel)
	{
		InitializeComponent();
        BindingContext = carListViewModel;
    }
}

