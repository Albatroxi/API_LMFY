using System.ComponentModel.DataAnnotations;

namespace API_LMFY.Models.provas
{
    public class provas
    {
        [Key]
        public int ID { get; set; }
        public int ID_Turma { get; set; }
    }
}
