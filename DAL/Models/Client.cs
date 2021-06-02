﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string INN { get; set; }
        public string CompanyName { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
