using System.ComponentModel.DataAnnotations;

namespace lab3.Models
{
    public class LoginViewModel
    {
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

