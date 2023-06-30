using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace crud.Models
{
	public class Contact
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener como máximo {1} caracteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Apellido debe tener como máximo {1} caracteres.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El campo Teléfono solo puede contener dígitos.")]
        public string Phone { get; set; }

        [StringLength(500, ErrorMessage = "El campo Comentarios debe tener como máximo {1} caracteres.")]
        public string Comments { get; set; }
    }
}

