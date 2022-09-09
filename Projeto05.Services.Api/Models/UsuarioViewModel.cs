using System.ComponentModel.DataAnnotations;

namespace Projeto05.Services.Api.Models
{
    public class UsuarioViewModel
    {
        [MinLength(6, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage ="Informe o nome do usuário.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Informe endereço de email válido.")]
        [Required(ErrorMessage = "Informe o email do usuário.")]
        public string? Email { get; set; }

        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Informe 11 digitos núméricos.")]
        [Required(ErrorMessage = "Informe o cpf do usuário.")]
        public string? Cpf { get; set; }
    }
}
