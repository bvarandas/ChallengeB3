using ChallengeB3.Domain.Models;

namespace ChallengeB3.Domain.Interfaces;

public interface IRegisterService
{
    
    Task<IEnumerable<Register>> GetListAllAsync();
    
}
