using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB.Model;

[Table("PLAYERS")]
public partial class Player
{
    [Key]
    [Column("ID", TypeName = "NUMBER")]
    public decimal Id { get; set; }

    [Column("TEAM_ID", TypeName = "NUMBER")]
    public decimal? TeamId { get; set; }

    [Column("PLAYER_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PlayerName { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<PlayerGoal> PlayerGoals { get; set; } = new List<PlayerGoal>();

    [InverseProperty("PrognosedPlayerNavigation")]
    public virtual ICollection<PrognoseHistory> PrognoseHistories { get; set; } = new List<PrognoseHistory>();

    [InverseProperty("PrognosedPlayerNavigation")]
    public virtual ICollection<Prognose> Prognoses { get; set; } = new List<Prognose>();

    [ForeignKey("TeamId")]
    [InverseProperty("Players")]
    public virtual Team? Team { get; set; }
}
