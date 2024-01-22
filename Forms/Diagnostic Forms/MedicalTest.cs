using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

public class MedicalTest
{
 [Key]
    public int TestId { get; set; }


    public string TestName { get; set; }
    public DateTime TestDate { get; set; }
    public string FilePath { get; set; }

    // Foreign key referencing Diagnostic
    [ForeignKey("Diagnostic")]
    public int DiagnosticId { get; set; }

    // Navigation property
    public Diagnostic Diagnostic { get; set; }

    public byte[] TestFile { get; set; }
}