namespace PatientService.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public bool Discharged { get; set; }
    }
}
