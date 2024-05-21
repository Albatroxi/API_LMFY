using System.ComponentModel.DataAnnotations;

namespace API_LMFY.Models.questoes
{
    public class questoes
    {
        [Key]
        public int idQuestoe_Provas { get; set; }
        public string enunciado { get; set; }
        public string op1 { get; set; }
        public string op2 { get; set; }
        public string op3 { get; set; }
        public string op4 { get; set; }
        public int op_correta { get; set; }
        public string dificuldade { get; set; }
    }
}
