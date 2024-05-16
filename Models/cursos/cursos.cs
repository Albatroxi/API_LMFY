using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API_LMFY.Models.cursos
{
    public class cursos
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
    }
}
