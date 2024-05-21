using System.ComponentModel.DataAnnotations;

namespace API_LMFY.Models.turmas
{
    public class turmas
    {
        [Key]
        public int ID { get; set; }
        public int ID_Curso { get; set; }
        public string Nome_Turma { get; set; }
    }
}
