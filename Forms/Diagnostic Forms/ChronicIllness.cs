using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChronicIllness
{
    [Key]
    public int ChronicIllnessId { get; set; }

    public string ChronicIllnessName { get; set; }

    // Foreign key referencing Diagnostic
    [ForeignKey("Diagnostic")]
    public int DiagnosticId { get; set; }

    // Navigation property
    public Diagnostic Diagnostic { get; set; }
}
