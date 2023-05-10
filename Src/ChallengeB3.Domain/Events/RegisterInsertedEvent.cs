namespace ChallengeB3.Domain.Events;
public  class RegisterInsertedEvent : Event
{
    public int RegisterId { get; set; } 
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Action { get; set; } = string.Empty;

    public RegisterInsertedEvent(int registerId, string description, string status, DateTime date, string action)
    {
        RegisterId = registerId;
        Description = description;
        Status = status;
        Date = date;
        Action = action;
    }
}