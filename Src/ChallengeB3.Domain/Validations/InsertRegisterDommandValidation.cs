using ChallengeB3.Domain.Commands;
namespace ChallengeB3.Domain.Validations;

public class InsertRegisterDommandValidation : RegisterValidation<InsertRegisterCommand>
{
    public InsertRegisterDommandValidation()
    {
        ValidateDescription();
        ValidateStatus();
        ValidateDate();
    }
}
