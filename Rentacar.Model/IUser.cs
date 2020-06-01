using System;

namespace Rentacar.Models
{
    public interface IUser
    {
        string Firstname { get; set; }
        string Surname { get; set; }
        string Address { get; set; }
        string Residence { get; set; }
        string Postalcode { get; set; }
        string Housenumber { get; set; }
        string Role { get; set; }
        string Id { get; set; }
        string UserName { get; set; }
        string NormalizedUserName { get; set; }
        string Email { get; set; }
        string NormalizedEmail { get; set; }
        bool EmailConfirmed { get; set; }
        string PasswordHash { get; set; }
        string SecurityStamp { get; set; }
        string ConcurrencyStamp { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        bool TwoFactorEnabled { get; set; }
        DateTimeOffset? LockoutEnd { get; set; }
        bool LockoutEnabled { get; set; }
        int AccessFailedCount { get; set; }
        string ToString();
    }
}