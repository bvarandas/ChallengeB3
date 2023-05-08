using ChallengeB3.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Domain.Validations
{
    public class RemoveRegisterCommandValidation : RegisterValidation<RemoveRegisterCommand>
    {
        public RemoveRegisterCommandValidation()
        {
            ValidateRegisterId();
        }
    }
}
