using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public class Author(string? fullName = null, string? initials = null, string? firstName = null, string? middleName = null, string? lastName = null, string? address = null)
    {
        public string? FullName = fullName;
        public string? Initials = initials;
        public string? FirstName = firstName;
        public string? MiddleName = middleName;
        public string? LastName = lastName;
        public string? Address = address;
    }
}
