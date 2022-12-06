using ListaCarrosApp.Views;

namespace ListaCarrosApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CarDetailsPage), typeof(CarDetailsPage));
	}
}
