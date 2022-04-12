using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SbShared
{
    public class PersonModel
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
