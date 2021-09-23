﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Pocos
{
    public class UserPoco
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Claim Role { get; set; }

    }
}
