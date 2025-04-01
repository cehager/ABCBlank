using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    // public record CreateAccountHolder(string FirstName, string LastName, DateTime DateOfBirth, string Email, string ContactNumber);
    public class CreateAccountHolder
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }

    //public record UpdateAccountHolder(int Id, string FirstName, string LastName, string Email, string ContactNumber);

    public class UpdateAccountHolder
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }
}
