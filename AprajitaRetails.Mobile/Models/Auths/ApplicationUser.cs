//using Microsoft.AspNetCore.Identity;

namespace AprajitaRetails.Server.Models
{
    public class MobileUser //: IdentityUser
    {
        public string? FullName { get; set; }
        public string? StoreId { get; set; }
        public string? EmployeeId { get; set; }

        public int? UserId { get; set; } = 0;
        public string UserName { get; set; }
        public string UserEmail { get; set; }


        public string? StoreGroupId { get; set; }
        public Guid? AppClinetId { get; set; }
        public UserType? UserType { get; set; } = global::UserType.Guest;
        public RolePermission? Permission { get; set; } = RolePermission.Guest;
        public bool Approved { get; set; } = false;
    }
}