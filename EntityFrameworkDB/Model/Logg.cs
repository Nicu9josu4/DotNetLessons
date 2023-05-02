using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB.Model;

[Table("LOGGS")]
public partial class Logg
{
    [Key]
    [Column("ID", TypeName = "NUMBER")]
    public decimal Id { get; set; }

    [Column("USERID", TypeName = "NUMBER")]
    public decimal? Userid { get; set; }

    [Column("USERMESSAGE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Usermessage { get; set; }

    [Column("USERCALLBACKDATA")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Usercallbackdata { get; set; }

    [Column("THROWEDEXCEPTIONS")]
    [Unicode(false)]
    public string? Throwedexceptions { get; set; }

    [Column("LOGGTYPE", TypeName = "NUMBER")]
    public decimal? Loggtype { get; set; }

    [Column("LOGGDATE", TypeName = "DATE")]
    public DateTime? Loggdate { get; set; }
}
