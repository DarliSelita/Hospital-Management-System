using System.Collections.Generic;
using System;
using System.Linq;

public class Billing
{
    public int BillingID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; }
    public string PatientFileNumber { get; set; }
    public DateTime BillingDate { get; set; }

    // Navigation property for associated products
    public List<Product> Products { get; set; } = new List<Product>();

    // Calculate the total amount based on product prices.
    public double TotalAmount => Products.Sum(product => product.Price);
}