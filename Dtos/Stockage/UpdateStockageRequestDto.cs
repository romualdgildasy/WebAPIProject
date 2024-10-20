using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Dtos.Stockage
{
    public class UpdateStockageRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbole connot be over 10 charcters")]       
        public string Symbole {get;set;} = string.Empty;

        [Required]
        [MaxLength(15, ErrorMessage = "NomCompagnie connot be over 15 charcters")]  
        public string NomCompagnie {get;set;} = string.Empty;

        [Required]
        [Range(1,1000000000)]
        public decimal Acheter {get;set;}
        
        [Required]
        [Range(0.001,100)]
        public decimal DerniereDividente {get;set;}

        [Required]
        [MaxLength(15, ErrorMessage = "Industrie connot be over 15 charcters")] 
        public string Industrie {get;set;} = string.Empty;


        [Required]
        [Range(1,5000000000)]
        public long CapitalisationBoursiere {get;set;}
    }
}