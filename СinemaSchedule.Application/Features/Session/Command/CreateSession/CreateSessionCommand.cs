using MediatR;

namespace СinemaSchedule.Application.Features.Session.Command.CreateSession;

public class CreateSessionCommand : IRequest<int>
{
    public int MovieId { get; set; }
    public int HallId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal BasePrice { get; set; }
    public DateTime? ActivationDate { get; set; }
}