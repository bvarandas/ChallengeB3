using ChallengeB3.Domain.Models;

namespace ChallengeB3.Domain.Interfaces;

public interface IRegisterRepository : IDisposable
{
    public Task<Register> GetRegisterByIDAsync(int registerId);
    public Task AddRegisterAsync(Register register);
    public Task UpdateRegisterAsync(Register register);
    public Task<IEnumerable<Register>> GetAllRegisterAsync();
    public Task DeleteRegisterAsync(int registerId);
}
