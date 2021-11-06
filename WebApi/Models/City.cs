using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    [Table(name: "City")]
    public class City
    {
        [Key]
        public int Id { get; set; }

        public int CountryId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        public Country Country { get; set; }
    }
}
