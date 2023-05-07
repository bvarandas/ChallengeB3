using ProtoBuf;



namespace ChallengeB3.Domain.Models
{
    [ProtoContract]
    public sealed class Register
    {
        [ProtoMember(1)]
        public int RegisterId { get; set; }
        [ProtoMember(2)]
        public string Description { get; set; } = string.Empty;
        
        [ProtoMember(3)]
        public string Status { get; set; } = string.Empty;
        
        [ProtoMember(4)]
        public DateTime Date { get; set; } 
    }
}
