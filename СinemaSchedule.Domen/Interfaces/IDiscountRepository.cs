using СinemaSchedule.Domen.Entities;

namespace СinemaSchedule.Domen.Interfaces;

public interface IDiscountRepository
{
    Task<DiscountEntity?> GetCurrentDiscountAsync();
    Task<DiscountEntity?> GetByDateAsync(DateTime date);
    Task AddAsync(DiscountEntity discount);
    Task RemoveAsync(DiscountEntity discount);
}