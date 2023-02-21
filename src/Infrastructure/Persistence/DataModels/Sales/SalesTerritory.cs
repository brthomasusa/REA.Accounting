namespace REA.Accounting.Infrastructure.Persistence.DataModels.Sales
{
    public class SalesTerritory
    {
        public int TerritoryID { get; set; }
        public string? CountryRegionCode { get; set; }

        public string? Name { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? Group { get; set; }
        public decimal SalesYTD { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal CostYTD { get; set; }
        public decimal CostLastYear { get; set; }
    }
}