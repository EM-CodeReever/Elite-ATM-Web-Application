using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EliteATMWebApp.Models
{
    [Table("Account")]
    public partial class Account
    {
        [Key]
        [Column("AccountID")]
        public int AccountId { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Type { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string Currency { get; set; } = null!;
        [Column("UserID")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Accounts")]
        public virtual User? User { get; set; }
    }
}
