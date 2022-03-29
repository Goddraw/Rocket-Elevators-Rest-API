namespace RocketElevatorsApi.Models
{
    public class Battery
    {
        public long Id { get; set; }
        public string? status { get; set; }
        public DateTime? date_of_commissioning {get; set;}
        public DateTime? date_of_last_inspection {get; set;}
        public string? certificate_of_operations { get; set; }
        public string? information { get; set; }
        public string? notes { get; set; }
        
    }
}