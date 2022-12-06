using ListaCarrosApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace ListaCarrosApp.Services
{
    public class CarService
    {
        SQLiteConnection connection;
        string _dbPath;
        public string StatusMessage;
        int result = 0;

        public CarService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (connection != null) return;

            connection = new SQLiteConnection(_dbPath);
            connection.CreateTable<Car>();
        }

        public List<Car> GetCars()
        {
            try
            {
                Init();
                return connection.Table<Car>().ToList();    
            }
            catch (Exception)
            {
                StatusMessage = "Falha ao carregar dados.";
            }

            return new List<Car>();
        }

        public Car GetCar(int id)
        {
            try
            {
                Init();
                return connection.Table<Car>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Falha ao buscar dados.";
            }

            return null;
        }

        public int DeleteCar(int id)
        {
            try
            {
                Init();
                return connection.Table<Car>().Delete(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Falha ao deletar dados.";
            }

            return 0;
        }

        public void AddCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Falha ao salvar carro.");

                result = connection.Insert(car);
                StatusMessage = result == 0 ? "Falha ao adicionar." : "Carro adicionado com sucesso.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Falha ao adicionar carro.";
            }
        }

        public void UpdateCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Falha ao salvar carro.");

                result = connection.Update(car);
                StatusMessage = result == 0 ? "Falha ao atualizar." : "Carro atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Falha ao atualizar dados.";
            }
        }
    }
}
