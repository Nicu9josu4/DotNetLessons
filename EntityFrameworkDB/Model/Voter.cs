using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB.Model;

[Table("VOTER")]
public partial class Voter
{
    [Key]
    [Column("ID", TypeName = "NUMBER")]
    public decimal Id { get; set; }

    [Column("FIRST_NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("LAST_NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("PHONE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("CHAT_ID", TypeName = "NUMBER")]
    public decimal ChatId { get; set; }

    [Column("LANGUAGE")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Language { get; set; }

    [Column("VOTED_TEAM", TypeName = "NUMBER")]
    public decimal? VotedTeam { get; set; }

    [Column("DATE_VOTED_TEAM", TypeName = "DATE")]
    public DateTime? DateVotedTeam { get; set; }

    [InverseProperty("Voter")]
    public virtual ICollection<PrognoseHistory> PrognoseHistories { get; set; } = new List<PrognoseHistory>();

    [InverseProperty("Voter")]
    public virtual ICollection<Prognose> Prognoses { get; set; } = new List<Prognose>();

    [ForeignKey("VotedTeam")]
    [InverseProperty("Voters")]
    public virtual Team? VotedTeamNavigation { get; set; }
}
