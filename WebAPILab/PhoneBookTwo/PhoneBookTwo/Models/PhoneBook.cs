﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookTwo.Models
{
    public class PhoneBook
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}