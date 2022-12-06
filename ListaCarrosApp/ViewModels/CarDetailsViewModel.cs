using CommunityToolkit.Mvvm.ComponentModel;
using ListaCarrosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ListaCarrosApp.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        Car car;

        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
            Car = App.CarService.GetCar(Id);
        }
    }
}
