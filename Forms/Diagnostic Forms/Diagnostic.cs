// Diagnostic class
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Diagnostic
{
    [Key]
    public int Id { get; set; }

    // Patient Information
    public string PatientName { get; set; }
    public string PatientFile { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }

    // Appointment Information
    public string ReasonForAppointment { get; set; }

    // Medical History
    public List<ChronicIllness> ChronicIllnesses { get; set; }
    public string Allergies { get; set; }
    public string MedicationsAndVaccines { get; set; }

    // Lifestyle Information
    public bool TobaccoUse { get; set; }
    public double AlcoholConsumptionPerWeek { get; set; }
    public bool DrugUse { get; set; }

    // Vital Signs
    [ForeignKey("PatientVitalSigns")]
    public int DiagnosticId { get; set; }

    // Navigation property
    public VitalSigns PatientVitalSigns { get; set; }

    // Buttons for Medical Tests
    public List<MedicalTest> MedicalTests { get; set; } = new List<MedicalTest>();

    // Constructor
    public Diagnostic()
    {
        ChronicIllnesses = new List<ChronicIllness>();
        Allergies = string.Empty;
        MedicationsAndVaccines = string.Empty;
        PatientVitalSigns = new VitalSigns(); 
    }

}


public class VitalSigns
{
    [Key]
    public int VitalSignsId { get; set; }

    public int BloodPressure { get; set; }
    public int HeartRate { get; set; }
    public int RespiratoryRate { get; set; }
    public float Temperature { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }

    // Foreign key referencing Diagnostic
    [ForeignKey("Diagnostic")]
    public int DiagnosticId { get; set; }

    // Navigation property
    public Diagnostic Diagnostic { get; set; }
}
