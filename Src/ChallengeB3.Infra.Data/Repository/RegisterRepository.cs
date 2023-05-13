using ChallengeB3.Domain.Models;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ChallengeB3.Infra.Data.Repository;

public class RegisterRepository: IRegisterRepository
{
    protected readonly DbContextClass _dbContext;
    //protected readonly DbSet<Register> DbSet;

    public RegisterRepository(DbContextClass dbContext)
    {
        _dbContext = dbContext;
        //DbSet = _dbContext.Set<Register>();
        _dbContext.Registers = _dbContext.Set<Register>();
    }

    public async Task AddRegisterAsync(Register register)
    {
        try
        {
            _dbContext.Registers.Add(register);
        }catch (Exception ex)
        {

        }
    }

    public void DeleteRegisterAsync(int registerId)
    {
        try
        {
            var filtered = _dbContext.Registers.Single(x => x.RegisterId == registerId);
            var result = _dbContext.Remove(filtered);
            _dbContext.SaveChanges();


        }
        catch (Exception ex)
        {

        }
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
        Register? registerResult = new Register();
        try
        {
            registerResult =  await _dbContext.Registers.Where(x => x.RegisterId == registerId).FirstAsync();
        }catch(Exception ex)
        {

        }
        return registerResult;
    }

    public async Task UpdateRegisterAsync(Register register)
    {
        try
        {
            var local = _dbContext.Registers.
                FirstOrDefault(entry => entry.RegisterId.Equals(register.RegisterId));

            if (local is not null)
            {
                _dbContext.Entry(local).State = EntityState.Detached;
            }
            
            var result = _dbContext.Registers.Update(register);

            _dbContext.Entry(register).State = EntityState.Modified;
        }
        catch (Exception ex)
        {

        }
        //bContext.SaveChanges();
        
        //return await Task.FromResult(result.Entity);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
