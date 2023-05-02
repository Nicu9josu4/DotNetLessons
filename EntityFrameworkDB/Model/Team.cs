using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB.Model;

[Table("TEAMS")]
public partial class Team
{
    [Key]
    [Column("ID", TypeName = "NUMBER")]
    public decimal Id { get; set; }

    [Column("TEAM_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TeamName { get; set; }

    [InverseProperty("FirstTeam")]
    public virtual ICollection<Match> MatchFirstTeams { get; set; } = new List<Match>();

    [InverseProperty("SecondTeam")]
    public virtual ICollection<Match> MatchSecondTeams { get; set; } = new List<Match>();

    [InverseProperty("Team")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    [InverseProperty("PrognosedTeamNavigation")]
    public virtual ICollection<PrognoseHistory> PrognoseHistories { get; set; } = new List<PrognoseHistory>();

    [InverseProperty("PrognosedTeamNavigation")]
    public virtual ICollection<Prognose> Prognoses { get; set; } = new List<Prognose>();

    [InverseProperty("VotedTeamNavigation")]
    public virtual ICollection<Voter> Voters { get; set; } = new List<Voter>();
}
