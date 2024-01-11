// Product class
public class Product
{
    public int ProductID { get; set; }
    public int BillingID { get; set; }
    public string ServiceName { get; set; }
    public double Price { get; set; }

    // Navigation property for associated billing
    public Billing Billing { get; set; }
}
