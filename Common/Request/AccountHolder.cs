using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Request
{
    public record CreateAccountHolder(string FirstName, string LastName, DateTime DateOfBirth, string Email, string ContactNumber);

    public record UpdateAccountHolder(int Id, string FirstName, string LastName, string Email, string ContactNumber);

}
