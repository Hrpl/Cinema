using СinemaSchedule.Domen;

namespace СinemaSchedule.Infrastructure.Data.Interfaces;

public interface IDiscountRepository
{
    Task<DiscountEntity?> GetCurrentDiscountAsync();
    Task<DiscountEntity?> GetByDateAsync(DateTime date);
    Task AddAsync(DiscountEntity discount);
    Task RemoveAsync(DiscountEntity discount);
}