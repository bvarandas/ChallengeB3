using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Domain.Commands;

public class InsertRegisterCommand : RegisterCommand
{
    public InsertRegisterCommand(string description, string status, DateTime date )
    {
        Description = description;
        Status = status;
        Date = date;
    }

    public override bool IsValid()
    {
        return true;
    }
}
