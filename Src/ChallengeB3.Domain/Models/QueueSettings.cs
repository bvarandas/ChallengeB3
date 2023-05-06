namespace ChallengeB3.Domain.Models
{
    public class QueueSettings
    {
        public string HostName { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
        public ushort Interval { get; set; }
    }
}
