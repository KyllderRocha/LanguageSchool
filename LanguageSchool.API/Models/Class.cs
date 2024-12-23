﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.API.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty; 

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}