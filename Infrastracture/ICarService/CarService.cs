using System.Net;
using Dapper;
using Domain.Models;
using Infrastracture.ApiResponse;
using Infrastracture.DataContext;

namespace Infrastracture.IRentService;

public class CarService(DapperContext context) : ICarService
{
    public Response<List<Cars>> GetAllCars()
    {
        var sql = "select * from cars";
        var cars = context.Connection().Query<Cars>(sql).ToList();
        return new Response<List<Cars>>(cars);
    }

    public Response<Cars> GetCarById(int id)
    {

        var sql = "select * from cars where carid = @id";
        var car = context.Connection().QuerySingleOrDefault<Cars>(sql, new { id });
        return car == null
            ? new Response<Cars>(HttpStatusCode.NotFound, "Car not found")
            : new Response<Cars>(HttpStatusCode.OK, "Car already exists");
    }

    public Response<bool> AddCar(Cars car)
    {
        var sql =
            "insert into cars(model, manufacturer, year, priceperday) values(model=@model, manufacturer=@model, year=@year, priceper=@priceperday);";
        var result = context.Connection().Execute(sql, car);
        return result == 0
            ? new Response<bool>(HttpStatusCode.NotFound, "Car not found")
            : new Response<bool>(HttpStatusCode.Created, "Car successufully added");
    }

    public Response<bool> UpdateCar(Cars car)
    {
        var sql =
            "update cars set model=@model, manufacturer=@manufacturer, year=@year, priceperday=@priceperday where carid=@carid;";
        var result = context.Connection().Execute(sql, car);
        return result > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Car not found")
            : new Response<bool>(HttpStatusCode.OK, "Car successfully updated");
    }

    public Response<bool> DeleteCar(int id)
    {
        var car = GetCarById(id).Data;
        if (car == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Car not found");
        }

        var sql = "delete from cars where carid = @id";
        var result = context.Connection().Execute(sql, new { Id = id });
        return result == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Car not found")
            : new Response<bool>(HttpStatusCode.OK, "Car successfully deleted");
    }
}