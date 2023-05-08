using ChallengeB3.Domain.Commands;

namespace ChallengeB3.Domain.Validations
{
    public class UpdateRegisterCommandValidation : RegisterValidation<UpdateRegisterCommand>
    {
        public UpdateRegisterCommandValidation() 
        { 
            ValidateRegisterId();
            ValidateDescription();
            ValidateStatus();
            ValidateDate();
        }    
    }
}