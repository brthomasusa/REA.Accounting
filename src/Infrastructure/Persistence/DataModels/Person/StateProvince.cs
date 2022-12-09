namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class StateProvince
    {
        public int StateProvinceID { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? CountryRegionCode { get; set; }
        public bool IsOnlyStateProvinceFlag { get; set; }
        public string? Name { get; set; }
        public int TerritoryID { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}