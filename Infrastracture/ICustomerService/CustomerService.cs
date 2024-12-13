using System.Net;
using Dapper;
using Domain.Models;
using Infrastracture.ApiResponse;
using Infrastracture.DataContext;

namespace Infrastracture.ICustomerService;

public class CustomerService(DapperContext context):ICustomerService
{
    public Response<List<Customer>> GetAllCustomers()
    {
        var sql = "select * from customers;";
        var customers = context.Connection().Query<Customer>(sql).ToList();
        return new Response<List<Customer>>(customers);
    }
    

    public Response<Customer> GetCustomerById(int id)
    {
        var sql = "select * from customers where customerid=@id;";
        var customer = context.Connection().QueryFirst<Customer>(sql, new { id });
        return customer == null ? new Response<Customer>(HttpStatusCode.NotFound,message: "Customer not found")
            : new Response<Customer>(HttpStatusCode.OK, message:"Customer is here");
    }

    public Response<bool> AddCustomer(Customer customer)
    {
        var sql = "insert into customers(fullname,phone,email) values(fullname=@fullname,phone=@phone,email=@email);";
        var result = context.Connection().Execute(sql, customer);
        return result == 0? new Response<bool>(HttpStatusCode.NotFound, message: "Customer not found")
            : new Response<bool>(HttpStatusCode.Created, message: "Customer successfully added");
    }

    public Response<bool> UpdateCustomer(Customer customer)
    {
        var sql = "update customers set fullname=@fullname,phone=@phone,email=@email where customerid=@customerid;";
        var result = context.Connection().Execute(sql, customer);
        return result > 0? new Response<bool>(HttpStatusCode.InternalServerError, message: "Customer not found")
            : new Response<bool>(HttpStatusCode.OK, message: "Customer successfully updated");
    }

    public Response<bool> DeleteCustomer(int id)
    {
        var sql = "delete from customer where customerid=@id";
        var result = context.Connection().Execute(sql, new { Id=id });
        return result == 0? new Response<bool>(HttpStatusCode.NotFound, message: "Customer not found")
            : new Response<bool>(HttpStatusCode.OK, message: "Customer successfully deleted");
    }
}