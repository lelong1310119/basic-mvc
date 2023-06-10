using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class Predictive
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UDI { get; set; }
        public string ProductId { get; set; } = null!;
        public string? Type { get; set; }
        public float? AirTemperature { get; set; }
        public float? ProcessTemperature { get; set; }
        public float? RotationalSpeed { get; set; }
        public float? Torque { get; set; }
        public float? ToolWear { get; set; }
        public float? Target { get; set; }
        public string? FailureType { get; set; }

    }
}
