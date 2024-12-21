using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.API.Models
{
    public class Class
    {
        public int Id { get; set; } // Chave primária
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty; // Código único da turma

        // Navegação
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}