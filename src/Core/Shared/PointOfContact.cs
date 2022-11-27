using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.Core.Shared
{
    public class PointOfContact : Entity<int>
    {
        protected PointOfContact() { }

        private PointOfContact(int contactID) : this() => Id = contactID;

        // public 
    }
}