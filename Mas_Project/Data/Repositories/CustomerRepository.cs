using Microsoft.EntityFrameworkCore;
using Mas_Project.Models;
using Mas_Project.Data.Repositories.Interfaces;

namespace Mas_Project.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DBContext _context;

    public CustomerRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await GetByIdAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}