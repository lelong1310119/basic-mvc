
namespace Project.Models

{
    public class Predictive
    {
        public int UDI { get; set; }
        public string ProductId { get; set; } = null!;
        public string Type { get; set; } = null!;   
        public float AirTemperature { get; set; }   
        public float ProcessTemperature { get; set; }   
        public float RotationalSpeed { get; set; }  
        public float Torque { get; set; }   
        public float ToolWear { get; set; } 
        public float Target { get; set; }   
        public string FailureType { get; set; } = null!;    
            
    }
}
