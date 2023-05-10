using ChallengeB3.Domain.Commands;
using ChallengeB3.Domain.Models;
namespace ChallengeB3.Domain.Interfaces;
public interface IRegisterService
{
    Task<IEnumerable<Register>> GetListAllAsync();
    Task<Register> GetRegisterByIDAsync(int registerId);
    Task<RegisterCommand> AddRegisterAsync(Register register);
    Task<RegisterCommand> UpdateRegisterAsync(Register register);
    IList<RegisterHistoryData> GetAllHistory(int registerId);
    void RemoveRegisterAsync(int registerId);
}