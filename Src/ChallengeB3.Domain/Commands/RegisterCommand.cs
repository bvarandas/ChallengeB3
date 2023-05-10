using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Domain.Commands;

public abstract class RegisterCommand : Command
{
    public int RegisterId { get; protected set; }
    public string Description { get; protected set; } = string.Empty;
    public string Status { get; protected set; } = string.Empty;
    public DateTime Date { get; protected set; }
    public string Action { get;protected set; } = string.Empty;
}
