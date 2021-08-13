using Core.Entities.Abstract;
using System;

namespace Entities.Dtos
{
    public class UserRegisterDto : IDto
    {
        //public Guid EmployeeId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public bool CanUseSystem { get; set; }
    }
}
