using Domain.Models;
using Infrastracture.ApiResponse;

namespace Infrastracture.ICustomerService;

public interface ICustomerService
{
    Response<List<Customer>> GetAllCustomers();
    Response<Customer> GetCustomerById(int customerId);
    Response<bool> AddCustomer(Customer customer);
    Response<bool> UpdateCustomer(Customer customer);
    Response<bool> DeleteCustomer(int customerId);
}