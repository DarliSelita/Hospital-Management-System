using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalTest
{
    [Key]
    public int TestId { get; set; }

    public string TestName { get; set; }
    public DateTime TestDate { get; set; }
    public string FilePath { get; set; }

    // Foreign key referencing Diagnostic
    [ForeignKey("PatientRecord")]
    public string PatientRecordId { get; set; }

    // Navigation property
    public Diagnostic PatientRecord { get; set; }

    public byte[] TestFile { get; set; }
}
