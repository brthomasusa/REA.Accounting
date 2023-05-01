using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Store
{
    public record GetEmployeeListAction(int PageNumber, int PageSize, string SearchTerm);
}