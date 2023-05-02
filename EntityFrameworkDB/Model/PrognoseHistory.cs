﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB.Model;

[Table("PROGNOSE_HISTORY")]
public partial class PrognoseHistory
{
    [Key]
    [Column("ID", TypeName = "NUMBER")]
    public decimal Id { get; set; }

    [Column("VOTER_ID", TypeName = "NUMBER")]
    public decimal VoterId { get; set; }

    [Column("MATCH_ID", TypeName = "NUMBER")]
    public decimal? MatchId { get; set; }

    [Column("SCORE_TEAM1", TypeName = "NUMBER")]
    public decimal? ScoreTeam1 { get; set; }

    [Column("SCORE_TEAM2", TypeName = "NUMBER")]
    public decimal? ScoreTeam2 { get; set; }

    [Column("PROGNOSED_TEAM", TypeName = "NUMBER")]
    public decimal? PrognosedTeam { get; set; }

    [Column("PROGNOSED_PLAYER", TypeName = "NUMBER")]
    public decimal? PrognosedPlayer { get; set; }

    [Column("PROGNOSED_DATE", TypeName = "DATE")]
    public DateTime? PrognosedDate { get; set; }

    [Column("PROGNOSED_TYPE", TypeName = "NUMBER")]
    public decimal? PrognosedType { get; set; }

    [ForeignKey("MatchId")]
    [InverseProperty("PrognoseHistories")]
    public virtual Match? Match { get; set; }

    [ForeignKey("PrognosedPlayer")]
    [InverseProperty("PrognoseHistories")]
    public virtual Player? PrognosedPlayerNavigation { get; set; }

    [ForeignKey("PrognosedTeam")]
    [InverseProperty("PrognoseHistories")]
    public virtual Team? PrognosedTeamNavigation { get; set; }

    [ForeignKey("VoterId")]
    [InverseProperty("PrognoseHistories")]
    public virtual Voter Voter { get; set; } = null!;
}
