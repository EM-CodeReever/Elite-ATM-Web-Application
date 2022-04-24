using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EliteATMWebApp.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string Password { get; set; } = null!;

        [InverseProperty("User")]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
