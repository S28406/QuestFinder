using Mas_Project.Models;
using Mas_Project.Data.Repositories.Interfaces;

namespace Mas_Project.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepo;

    public CustomerService(ICustomerRepository customerRepo)
    {
        _customerRepo = customerRepo;
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _customerRepo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _customerRepo.GetAllAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        await _customerRepo.AddAsync(customer);
        await _customerRepo.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        await _customerRepo.UpdateAsync(customer);
        await _customerRepo.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _customerRepo.DeleteAsync(id);
        await _customerRepo.SaveChangesAsync();
    }
}