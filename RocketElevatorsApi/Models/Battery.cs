namespace RocketElevatorsApi.Models
{
    public class Battery
    {
        public long Id { get; set; }
        public string? status { get; set; }
        public DateTime? CommissioningDate {get; set;}
        public DateTime? LastInspectionDate {get; set;}
        public string? CertificateOfOperations { get; set; }
        public string? Information { get; set; }
        public string? Notes { get; set; }
        
    }
}