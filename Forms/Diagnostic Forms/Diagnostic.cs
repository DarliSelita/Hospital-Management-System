using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Diagnostic
{
    // Patient Information
    public string PatientName { get; set; }

    [Key]
    public string PatientFile { get; set; }

    public string Gender { get; set; }
    public string PhoneNumber { get; set; }

    // Appointment Information
    public string ReasonForAppointment { get; set; }

    // Medical History
    public List<ChronicIllness> ChronicIllnesses { get; set; }
    public string Allergies { get; set; }  // Change from List<string> to string
    public string MedicationsAndVaccines { get; set; }

    // Lifestyle Information
    public bool TobaccoUse { get; set; }
    public double AlcoholConsumptionPerWeek { get; set; }
    public bool DrugUse { get; set; }

    // Vital Signs
    [ForeignKey("PatientFile")]
    public VitalSigns PatientVitalSigns { get; set; }

    // Buttons for Medical Tests
    public List<MedicalTest> MedicalTests { get; set; }

    // Constructor
    public Diagnostic()
    {
        ChronicIllnesses = new List<ChronicIllness>();
        Allergies = string.Empty;  // Initialize as empty string
        MedicationsAndVaccines = string.Empty;  // Initialize as empty string
        PatientVitalSigns = new VitalSigns();
        MedicalTests = new List<MedicalTest>();
    }
}

public class VitalSigns
{
    [Key, ForeignKey("Diagnostic")]
    public string PatientFile { get; set; }

    public int BloodPressure { get; set; }
    public int HeartRate { get; set; }
    public int RespiratoryRate { get; set; }
    public float Temperature { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }

    public Diagnostic Diagnostic { get; set; }
}