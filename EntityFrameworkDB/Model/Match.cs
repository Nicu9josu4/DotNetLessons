using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB.Model;

[Table("MATCH")]
public partial class Match
{
    [Key]
    [Column("ID", TypeName = "NUMBER")]
    public decimal Id { get; set; }

    [Column("FIRST_TEAM_ID", TypeName = "NUMBER")]
    public decimal? FirstTeamId { get; set; }

    [Column("SECOND_TEAM_ID", TypeName = "NUMBER")]
    public decimal? SecondTeamId { get; set; }

    [Column("START_TIME", TypeName = "DATE")]
    public DateTime? StartTime { get; set; }

    [Column("FIRST_TEAM_SCORE", TypeName = "NUMBER")]
    public decimal? FirstTeamScore { get; set; }

    [Column("SECOND_TEAM_SCORE", TypeName = "NUMBER")]
    public decimal? SecondTeamScore { get; set; }

    [ForeignKey("FirstTeamId")]
    [InverseProperty("MatchFirstTeams")]
    public virtual Team? FirstTeam { get; set; }

    [InverseProperty("Match")]
    public virtual ICollection<PlayerGoal> PlayerGoals { get; set; } = new List<PlayerGoal>();

    [InverseProperty("Match")]
    public virtual ICollection<PrognoseHistory> PrognoseHistories { get; set; } = new List<PrognoseHistory>();

    [InverseProperty("Match")]
    public virtual ICollection<Prognose> Prognoses { get; set; } = new List<Prognose>();

    [ForeignKey("SecondTeamId")]
    [InverseProperty("MatchSecondTeams")]
    public virtual Team? SecondTeam { get; set; }
}
