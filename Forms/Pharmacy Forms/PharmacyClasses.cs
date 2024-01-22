    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Diagnose
    {
        public int DiagnoseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }

    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int PatientId { get; set; }
        public int DiagnoseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual PatientPrc Patient { get; set; }
        public virtual Diagnose Diagnose { get; set; }
        public virtual ICollection<PrescribedMedication> PrescribedMedications { get; set; }
        public virtual ICollection<DispensingRecord> DispensingRecords { get; set; }

    }

    public class PatientPrc
    {
        [Key]
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual PatientDetails Details { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }

    public class PatientDetails
    {
        public int PatientDetailsId { get; set; }
        public int PatientId { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public virtual PatientPrc Patient { get; set; }
    }

    public class Medication
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<PrescribedMedication> PrescribedMedications { get; set; }
        public virtual MedicalInventory MedicalInventory { get; set; }


    }

    public class PrescribedMedication
    {
        public int PrescribedMedicationId { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicationId { get; set; }
        public int Quantity { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual Medication Medication { get; set; }
    }

    public class DispensingRecord
    {
        public int DispensingRecordId { get; set; }
        public int PrescriptionId { get; set; }
        public int QuantityDispensed { get; set; }
        public DateTime DispensingDate { get; set; }
        public virtual Prescription Prescription { get; set; }
    }

    public class MedicalInventory
    {
        public int MedicalInventoryId { get; set; }
        public int MedicationId { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReorderLevel { get; set; }
        public string SupplierInformation { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string StorageLocation { get; set; }
        public virtual Medication Medication { get; set; }
    }
