using System.ComponentModel.DataAnnotations;

namespace API_LMFY.Models.provas_org
{
    public class provas_org
    {
        [Key]
        public int id { get; set; }
        public int q_1 { get; set; }
        public bool q_1_resp { get; set; }
        public int q_2 { get; set; }
        public bool q_2_resp { get; set; }
        public int q_3 { get; set; }
        public bool q_3_resp { get; set; }
        public int q_4 { get; set; }
        public bool q_4_resp { get; set; }
        public int q_5 { get; set; }
        public bool q_5_resp { get; set; }
        public int q_6 { get; set; }
        public bool q_6_resp { get; set; }
        public int q_7 { get; set; }
        public bool q_7_resp { get; set; }
        public int q_8 { get; set; }
        public bool q_8_resp { get; set; }
        public int q_9 { get; set; }
        public bool q_9_resp { get; set; }
        public int q_10 { get; set; }
        public bool q_10_resp { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string usermail { get; set; }

        public bool finish { get; set; }
    }
}
