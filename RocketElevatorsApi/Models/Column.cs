namespace RocketElevatorsApi.Models
{
    public class Column
    {
        public long Id { get; set; }
        public string? set_type { get; set; }
        public long nb_of_floors_served { get; set; }
        public string? status { get; set; }
        public string? information { get; set; }
        public string? notes { get; set; }
        public long? battery_id {get; set;}
        
    }
}