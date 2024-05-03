using System.ComponentModel.DataAnnotations;

namespace API_LMFY.Models.users
{
    public class usersModel
    {
        [Key]
        public int id { get; set; }

        public string login { get; set; }

        public string pssW { get; set; }

        // AGUARDANDO MAIS INFORMACOES

    }
}
