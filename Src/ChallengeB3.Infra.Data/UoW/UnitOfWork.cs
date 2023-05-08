using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Infra.Data.Context;

namespace ChallengeB3.Infra.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContextClass _dbContext;

    public UnitOfWork(DbContextClass dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Commit()
    {
        return _dbContext.SaveChanges() > 0;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
