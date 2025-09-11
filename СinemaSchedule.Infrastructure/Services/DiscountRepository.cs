using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Infrastructure.Data.Services;

public class DiscountRepository : IDiscountRepository
{
    private readonly CinemaSheduleAppContext _context;

    public DiscountRepository(CinemaSheduleAppContext context)
    {
        _context = context;
    }

    public async Task<DiscountEntity?> GetCurrentDiscountAsync()
    {
        return await _context.Discount
            .Where(d => d.StartDate <= DateTime.Today)
            .OrderByDescending(d => d.StartDate)
            .FirstOrDefaultAsync();
    }

    public async Task<DiscountEntity?> GetByDateAsync(DateTime date)
    {
        return await _context.Discount
            .FirstOrDefaultAsync(d => d.StartDate == date.Date);
    }

    public async Task AddAsync(DiscountEntity discount)
    {
        await _context.Discount.AddAsync(discount);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(DiscountEntity discount)
    {
        _context.Discount.Remove(discount);
        await _context.SaveChangesAsync();
    }
}