using Domain.Models;
using Infrastracture.ApiResponse;

namespace Infrastracture.IRentService;

public interface ICarService
{
    Response<List<Cars>> GetAllCars();
    Response<Cars> GetCarById(int id);
    Response<bool> AddCar(Cars car);
    Response<bool> UpdateCar(Cars car);
    Response<bool> DeleteCar(int id);
}