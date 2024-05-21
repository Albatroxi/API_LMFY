using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace API_LMFY.Models.users
{
    public class usuarios
    {
        public int idusuario { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string nome_completo { get; set; }

        [Key]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(Int32.MaxValue, MinimumLength = 6, ErrorMessage = "A senha deve conter ao menos 6 dígitos.")]
        public string senha { get; set; }

        public string perfil { get; set; }
        /*
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string nome { get; set; }

        [Key]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(Int32.MaxValue, MinimumLength = 6, ErrorMessage = "A senha deve conter ao menos 6 dígitos.")]
        public string pssW { get; set; }

        public string token { get; set; }

        public int usuarioatributo { get; set; }
        */

        public string gnewpass()
        {
            string newPass = Guid.NewGuid().ToString().Substring(0,8);
            senha = newPass;
            return newPass;
        }

    }
}
