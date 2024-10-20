using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Dtos.Comments
{
    public class CreateCommentsDto
    {
         [Required]
         [MinLength(5, ErrorMessage = "Title must be characters")]
         [MaxLength(300, ErrorMessage = "Title connot be over 300 charcters")]
        public string Titre { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Contenu must be characters")]
        [MaxLength(300, ErrorMessage = "Contenu connot be over 300 charcters")]
        public string Contenu { get; set; } = string.Empty;
    }
}