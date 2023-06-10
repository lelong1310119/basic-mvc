using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class Machine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Location { get; set; }
        public string? MinTemp { get; set; }
        public string? MaxTemp { get; set; }
        public string? Leakage { get; set; }
        public string? Evaporation { get; set; }
        public string? Electricity { get; set; }
        public string? Parameter1Dir { get; set; }
        public string? Parameter1Speed { get; set; }
        public string? Parameter2_9am { get; set; }
        public string? Parameter2_3pm { get; set; }
        public string? Parameter3_9am { get; set; }
        public string? Parameter3_3pm { get; set; }
        public string? Parameter4_9am { get; set; }
        public string? Parameter4_3pm { get; set; }
        public string? Parameter5_9am { get; set; }
        public string? Parameter5_3pm { get; set; }
        public string? Parameter6_9am { get; set; }
        public string? Parameter6_3pm { get; set; }
        public string? Parameter7_9am { get; set; }
        public string? Parameter7_3pm { get; set; }
        public string? Failure_today { get; set; }
        public double RISK_MM { get; set; }
        public string? Fail_tomorrow { get; set; }
    }
}
