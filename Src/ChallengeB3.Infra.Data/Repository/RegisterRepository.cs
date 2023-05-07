﻿using ChallengeB3.Domain.Models;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace ChallengeB3.Infra.Data.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly DbContextClass _dbContext;

        public RegisterRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Register> AddRegisterAsync(Register register)
        {
            var result = _dbContext.Registers.Add(register);
            _dbContext.SaveChanges();

            return await Task.FromResult( result.Entity);
        }

        public async Task<bool> DeleteRegisterAsync(int registerId)
        {
            var filtered = _dbContext.Registers.Where(x => x.RegisterId == registerId);
            var result = _dbContext.Remove(filtered);
            _dbContext.SaveChanges();

            return await Task<bool>.FromResult( result is not null ? true : false);
        }

        public async Task<IEnumerable<Register>> GetAllRegisterAsync()
        {
            return await _dbContext.Registers.ToListAsync();
        }

        public async Task<Register> GetRegisterByIDAsync(int registerId)
        {
            return await _dbContext.Registers.Where(x => x.RegisterId == registerId).FirstAsync();
        }

        public async Task<Register> UpdateRegisterAsync(Register register)
        {
            var result = _dbContext.Registers.Update(register);
            _dbContext.SaveChanges();
            
            return await Task.FromResult(result.Entity);
        }
    }
}