namespace ChallengeB3.Application.EventSourceNormalizes;

public class RegisterHistoryData
{
    public string Action { get; set; } = string.Empty;
    public string RegisterId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string When { get; set; } = string.Empty;
    public string Who { get; set; } = string.Empty;
}
