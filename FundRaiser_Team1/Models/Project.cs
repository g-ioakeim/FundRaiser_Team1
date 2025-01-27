﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiser_Team1.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StatusPost { get; set; }
        public string Photo { get; set; }
        //public List<string> Videos { get; set; }
        public List<Package> AwardPackages { get; set; }
        public List<User> Users { get; set; }
    }
}
