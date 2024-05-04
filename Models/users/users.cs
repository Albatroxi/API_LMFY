using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace API_LMFY.Models.users
{
    public class users
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string nome { get; set; }

        [Key]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(Int32.MaxValue, MinimumLength = 6, ErrorMessage = "A senha deve conter ao menos 6 dígitos.")]
        public string pssW { get; set; }

        public int attribute { get; set; }

    }
}
