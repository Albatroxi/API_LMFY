using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_LMFY.Models.cursos
{
    public class cursos
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int id { get; set; }

        [Key]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string nome { get; set; }
    }
}
