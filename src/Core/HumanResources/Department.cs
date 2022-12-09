#pragma warning disable CS8618

using System.Text.Json.Serialization;
using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.Core.HumanResources
{
    public class Department : Entity<int>
    {
        public Department() { }

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

        public static Department Create
        (int id, string name, string groupName)
            => new Department
            (
                id,
                OrganizationName.Create(name),
                OrganizationName.Create(groupName)
            );

        public string Name { get; private set; }
        public void UpdateName(string value)
        {
            Name = OrganizationName.Create(value).Value!;
            UpdateModifiedDate();
        }

        public string GroupName { get; private set; }
        public void UpdateGroupName(string value)
        {
            GroupName = OrganizationName.Create(value).Value!;
            UpdateModifiedDate();
        }
    }
}