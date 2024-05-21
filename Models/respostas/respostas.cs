using System.ComponentModel.DataAnnotations;

namespace API_LMFY.Models.respostas
{
    public class respostas
    {
        [Key]
        public int ID { get; set; }
        public int ID_Prova { get; set; }
        public int ID_Aluno { get; set; }
        public int ID_Questao { get; set; }
        public string Resposta { get; set; }

    }
}
