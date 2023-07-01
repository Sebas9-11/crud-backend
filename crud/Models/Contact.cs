using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace crud.Models
{
	public class Contact
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, ErrorMessage = "The Name field must be no longer than {1} characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Lastname field is required.")]
        [StringLength(50, ErrorMessage = "The Last Name field must be no longer than {1} characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Phone field is required. ")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "The Phone field can only contain digits.")]
        public string Phone { get; set; }

        [StringLength(500, ErrorMessage = "The Comments field must be no longer than {1} characters.")]
        public string Comments { get; set; }
    }
}

