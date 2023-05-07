using ChallengeB3.Domain.Models;

namespace ChallengeB3.Domain.Interfaces
{
    public interface IRegisterRepository
    {
        public Task<Register> GetRegisterByIDAsync(int registerId);
        public Task<Register> AddRegisterAsync(Register register);
        public Task<Register> UpdateRegisterAsync(Register register);
        public Task<IEnumerable<Register>> GetAllRegisterAsync();
        public Task<bool> DeleteRegisterAsync(int registerId);
    }
}
