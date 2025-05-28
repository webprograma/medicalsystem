namespace PatientService.DTOs
{
    public class PatientDto
    {
        public string FullName { get; set; }
        public string Diagnosis { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool Discharged { get; set; }
    }
}
