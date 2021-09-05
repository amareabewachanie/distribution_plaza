using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace distribution_plaza.Dtos
{
    public class UserForRegisterDto
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8,MinimumLength = 4,ErrorMessage = "Password should be between 4 and 8 characters long")]
        public string Password { get; set; }
        public string UserRole { get; set; }
        public string SelectedBay { get; set; }
    }
}
