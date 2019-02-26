using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitApp.Models
{
    [Table("GymClass")]
    public class GymClass
    {
        [Key]
        public int GymClassId { get; set; }

        public string Name { get; set; }

        public int? MaxClients { get; set; }
    }
}
