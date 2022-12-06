using ListaCarrosApp.Services;
using ListaCarrosApp.ViewModels;
using ListaCarrosApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ListaCarrosApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");

		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CarService>(s, dbPath));

		builder.Services.AddSingleton<CarListViewModel>();
        builder.Services.AddTransient<CarDetailsViewModel>();

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<CarDetailsPage>();

        return builder.Build();
	}
}
