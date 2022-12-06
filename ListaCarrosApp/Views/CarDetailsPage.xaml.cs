using ListaCarrosApp.ViewModels;

namespace ListaCarrosApp.Views;

public partial class CarDetailsPage : ContentPage
{
	public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = carDetailsViewModel;

		//Preferences.Set("saveDetails", true);
		//var detailsSaved = Preferences.Get("saveDetails", false);
		
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}