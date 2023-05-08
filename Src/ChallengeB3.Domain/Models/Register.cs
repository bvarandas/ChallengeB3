using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeB3.Domain.Models;

[Table("tb_Register")]
[ProtoContract]
public sealed class Register 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ProtoMember(1)]
    public int RegisterId { get; set; }
    [ProtoMember(2)]
    public string Description { get; set; } = string.Empty;
    
    [ProtoMember(3)]
    public string Status { get; set; } = string.Empty;
    
    [ProtoMember(4)]
    public DateTime Date { get; set; }

    public Register(int registerId, string description, string status, DateTime date)
    {
        RegisterId = registerId;
        Description = description;
        Status = status;
        Date = date;
    }

    public Register(string description, string status, DateTime date)
    {
        Description = description;
        Status = status;
        Date = date;
    }

    public Register() { }
}