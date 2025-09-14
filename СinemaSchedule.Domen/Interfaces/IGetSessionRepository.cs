namespace СinemaSchedule.Domen.Interfaces;

public interface IGetSessionRepository<TRequest, TResponse>
{
    
    Task<TResponse> GetAll(TRequest request);
}