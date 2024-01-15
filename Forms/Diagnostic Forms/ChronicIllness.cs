using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChronicIllness
{
    [Key]
    public int ChronicIllnessId { get; set; }

    public string ChronicIllnessName { get; set; }

    [ForeignKey("Diagnostic")]
    public string PatientFile { get; set; }

    
    public Diagnostic Diagnostic { get; set; }
}
