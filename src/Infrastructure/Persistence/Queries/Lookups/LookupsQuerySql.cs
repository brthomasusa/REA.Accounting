namespace REA.Accounting.Infrastructure.Persistence.Queries.Lookups
{
    public static class LookupsQuerySql
    {
        public const string GetStateCodeIdForUSA =
        @"SELECT 
            StateProvinceID, StateProvinceCode 
        FROM Person.StateProvince 
        WHERE CountryRegionCode = 'US' 
        ORDER BY StateProvinceCode";

        public const string GetStateCodeIdForAll =
        @"SELECT 
            StateProvinceID, StateProvinceCode 
        FROM Person.StateProvince 
        ORDER BY StateProvinceCode";
    }
}