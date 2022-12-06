using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ListaCarrosApp.Models;
using ListaCarrosApp.Services;
using ListaCarrosApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ListaCarrosApp.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        const string editButtonText = "Salvar Atualização";
        const string createButtonText = "Adicionar";
        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel()
        {
            Title = "Lista de carros";
            AddEditButtonText = createButtonText;
            GetCarsList().Wait();
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        int carId;

        [ICommand]
        async Task GetCarsList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();

                var cars = App.CarService.GetCars();

                foreach (var car in cars) Cars.Add(car);

                //string fileName = "carlist.json";
                //var serializedList = JsonSerializer.Serialize(cars);
                //File.WriteAllText(fileName, serializedList);

                //var rawText = File.ReadAllText(fileName);
                //var carsFromText = JsonSerializer.Deserialize<List<Car>>(rawText);

                //string path = FileSystem.AppDataDirectory;
                //Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            }
            catch (Exception exception)
            {
                Debug.Write($"Impossível listar carros: {exception.Message}");
                await Shell.Current.DisplayAlert("Erro", "Falaha ao retorna a lista de carros.", "Ok");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [ICommand]
        async Task GetCarDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

        [ICommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await Shell.Current.DisplayAlert("Alerta", "Por favor, insira os dados corretamente.", "Ok");
                return;
            }

            var car = new Car
            {
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            if (CarId != 0)
            {
                car.Id = CarId;
                App.CarService.UpdateCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
            }
            else
            {
                App.CarService.AddCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
            }

            await GetCarsList();
            await ClearForm();
        }

        [ICommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Alerta", "Tente novamente.", "Ok");
                return;
            }

            var result = App.CarService.DeleteCar(id);

            if (result == 0) await Shell.Current.DisplayAlert("Alerta", "Não foi possível deletar o carro.", "Ok");
            else
            {
                await Shell.Current.DisplayAlert("Alerta", "Carro removido com sucesso.", "Ok");
                await GetCarsList();
            }
        }

        [ICommand]
        async Task UpdateCar(int id)
        {
            AddEditButtonText = editButtonText;
            return;
        }

        [ICommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            CarId = id;
            var car = App.CarService.GetCar(id);
            Make = car.Make;
            Model = car.Model;
            Vin = car.Vin;
        }

        [ICommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = 0;
            Make = string.Empty;
            Model = string.Empty;
            Vin = string.Empty;
        }
    }
}
