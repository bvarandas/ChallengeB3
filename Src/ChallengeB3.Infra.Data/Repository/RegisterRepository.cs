using ChallengeB3.Domain.Models;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ChallengeB3.Infra.Data.Repository;

public class RegisterRepository: IRegisterRepository
{
    protected readonly DbContextClass _dbContext;
    

    public RegisterRepository(DbContextClass dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRegisterAsync(Register register)
    {
        _dbContext.Registers.Add(register);
    }

    public async Task DeleteRegisterAsync(int registerId)
    {
        var filtered = _dbContext.Registers.Where(x => x.RegisterId == registerId);
        var result = _dbContext.Remove(filtered);
        //_dbContext.SaveChanges();

        //return await Task<bool>.FromResult( result is not null ? true : false);
    }

    public async Task<IEnumerable<Register>> GetAllRegisterAsync()
    {
        var  registerList = new List<Register>();
        try
        {
            registerList =  await _dbContext.Registers.ToListAsync();

        }catch (Exception ex)
        {

        }

        return registerList;
    }

    public async Task<Register> GetRegisterByIDAsync(int registerId)
    {
        return await _dbContext.Registers.Where(x => x.RegisterId == registerId).FirstAsync();
    }

    public async Task UpdateRegisterAsync(Register register)
    {
        var result = _dbContext.Registers.Update(register);
        //bContext.SaveChanges();
        
        //return await Task.FromResult(result.Entity);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
