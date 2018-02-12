using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneBook2.Models
{
    public class PhoneBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}