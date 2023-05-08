using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Domain.Commands;

public class UpdateRegisterCommand : RegisterCommand
{
    public UpdateRegisterCommand(int registerId, string description, string status, DateTime date )
    {
        RegisterId = registerId;
        Description = description;
        Status = status;
        Date = date;
    }

    public override bool IsValid()
    {
        //ValidationResult = new 
        return true;
    }
}
