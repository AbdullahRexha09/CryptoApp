using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapplication.Models
{
    public class FavoriteCryptos
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column("userid")]
        public Guid UserId{ get; set; }
        [Column("cryptoId")]
        public string CryptoId { get; set; }
    }
}
