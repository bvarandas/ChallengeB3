using ChallengeB3.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Domain.Commands;

public class RemoveRegisterCommand : RegisterCommand
{
    public RemoveRegisterCommand(int registerId)
    {
        RegisterId = registerId;
    }
    public override bool IsValid()
    {
        ValidationResult = new RemoveRegisterCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
