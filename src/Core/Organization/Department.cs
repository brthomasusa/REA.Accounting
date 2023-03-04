#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Organization
{
    public sealed class Department : Entity<int>
    {
        private Department
        (
            int id,
            OrganizationName name,
            OrganizationName groupName
        )
        {
            Id = id;
            Name = name.Value!;
            GroupName = groupName.Value!;
        }

        internal static Result<Department> Create(int id, string name, string groupName)
        {
            try
            {
                Department dept = new
                (
                    id,
                    OrganizationName.Create(name),
                    OrganizationName.Create(groupName)
                );

                return dept;
            }
            catch (Exception ex)
            {
                return Result<Department>.Failure<Department>(new Error("Department.Create", Helpers.GetExceptionMessage(ex)));
            }
        }

        internal Result<Department> Update(string name, string groupName)
        {
            try
            {
                Name = OrganizationName.Create(name);
                GroupName = OrganizationName.Create(groupName);
                UpdateModifiedDate();

                return this;
            }
            catch (Exception ex)
            {
                return Result<Department>.Failure<Department>(new Error("Department.Update", Helpers.GetExceptionMessage(ex)));
            }
        }

        public string Name { get; private set; }

        public string GroupName { get; private set; }
    }
}