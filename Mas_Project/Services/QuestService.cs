using Mas_Project.Models;
using Mas_Project.Enums;
using Mas_Project.Data.Repositories.Interfaces;

namespace Mas_Project.Services;

public class QuestService
{
    private readonly IQuestRepository _questRepo;
    private readonly ICustomerRepository _customerRepo;

    public QuestService(IQuestRepository questRepo, ICustomerRepository customerRepo)
    {
        _questRepo = questRepo;
        _customerRepo = customerRepo;
    }

    public async Task<Quest?> GetByIdAsync(Guid id)
    {
        return await _questRepo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Quest>> GetAllAsync()
    {
        return await _questRepo.GetAllAsync();
    }

    public async Task AddQuestAsync(Quest quest, Guid customerId)
    {
        var customer = await _customerRepo.GetByIdAsync(customerId);
        if (customer == null)
            throw new ArgumentException("Customer not found");
        
        await _questRepo.AddAsync(quest);
        await _questRepo.SaveChangesAsync();
    }

    public async Task UpdateQuestStatusAsync(Guid questId, QuestStatus newStatus)
    {
        var quest = await _questRepo.GetByIdAsync(questId);
        if (quest == null) throw new ArgumentException("Quest not found.");

        quest.UpdateStatus(newStatus);
        await _questRepo.UpdateAsync(quest);
        await _questRepo.SaveChangesAsync();
    }
}