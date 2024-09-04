using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class Author(string? fullName, string? initials, string? firstName, string? middleName, string? lastName, string? address)
    {
        public string? FullName = fullName;
        public string? Initials = initials;
        public string? FirstName = firstName;
        public string? MiddleName = middleName;
        public string? LastName = lastName;
        public string? Address = address;
    }
}
