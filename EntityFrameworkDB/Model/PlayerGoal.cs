using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB.Model;

[Table("PLAYER_GOALS")]
public partial class PlayerGoal
{
    [Key]
    [Column("ID", TypeName = "NUMBER")]
    public decimal Id { get; set; }

    [Column("PLAYER_ID", TypeName = "NUMBER")]
    public decimal? PlayerId { get; set; }

    [Column("MATCH_ID", TypeName = "NUMBER")]
    public decimal? MatchId { get; set; }

    [Column("GOALS", TypeName = "NUMBER")]
    public decimal? Goals { get; set; }

    [ForeignKey("MatchId")]
    [InverseProperty("PlayerGoals")]
    public virtual Match? Match { get; set; }

    [ForeignKey("PlayerId")]
    [InverseProperty("PlayerGoals")]
    public virtual Player? Player { get; set; }
}
