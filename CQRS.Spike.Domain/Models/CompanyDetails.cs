namespace CQRS.Spike.Domain.Models
{
  public class CompanyDetails
  {
    public string Name { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }

    public string TaxNumber { get; set; }
    public string ChamberOfCommerceNumber { get; set; }

    public string BankAccountIban { get; set; }
    public string BankAccountBic { get; set; }
  }
}
