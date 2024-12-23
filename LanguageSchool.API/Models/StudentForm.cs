using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageSchool.API.Models
{
    public class StudentForm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty; 
        public int ClassId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}