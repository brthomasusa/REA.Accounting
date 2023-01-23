using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class Password
    {
        public int BusinessEntityID { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}