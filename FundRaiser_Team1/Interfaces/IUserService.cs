﻿using FundRaiser_Team1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FundRaiser_Team1.Services
{
    public interface IUserService
    {
        public void CreateUser(User user);
        public User ReadUser(int id);
        public List<User> ReadUser();
    }
}
