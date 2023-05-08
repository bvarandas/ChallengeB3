using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Domain.Interfaces;

public interface IUnitOfWork
{
    bool Commit();
}
