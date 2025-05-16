using testKol.Models.DTOs;

namespace testKol.Services;

public interface IVisitService
{
    public Task<ClientVisitDTO> GetClientVisit(int id);
    
    public Task AddClientVisit(VisitAddDTO clientVisit);
}