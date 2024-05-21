using System.ComponentModel.DataAnnotations;

namespace API_LMFY.Models.disciplinas
{
    public class disciplinas
    {
        [Key]
       public int ID { get; set; }
        public string Nome_Disciplina { get; set; }
    }
}
