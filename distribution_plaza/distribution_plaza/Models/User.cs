using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace distribution_plaza.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt  { get; set; }
        public string UserRole { get; set; }
        public string SelectedBay { get; set; }
    }
}
