namespace СinemaSchedule.Domen.Entities;

public class DiscountEntity
{
    public int Id { get; set; }
    
    public decimal DiscountPercentage { get; set; }
    
    public DateTime StartDate { get; set; }
}