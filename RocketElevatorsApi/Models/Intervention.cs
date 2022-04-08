namespace RocketElevatorsApi.Models
{
    public class Intervention
    {
        public long Id { get; set; }
        public string? author { get; set; }
        public string? customerID { get; set; }
        public string? buildingID { get; set; }
        public string? batteryID { get; set; }
        public string columnID { get; set; }
        public string elevatorID { get; set; }
        public string employeeID {get; set;}
        public DateTime? startDateAndTimeOfIntervention {get; set;}
        public DateTime? endDateAndTimeOfIntervention {get; set;}
        public string? result  { get; set; }
        public string? report {get; set;}
        public string? status {get; set;}
    }
}