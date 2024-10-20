using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Dtos.Comments;

namespace WebAPIProject.Dtos.Stockage
{
    public class StockageDto
    {
        
        public int Id {get;set;}

        public string Symbole {get;set;} = string.Empty;

        public string NomCompagnie {get;set;} = string.Empty;

        public decimal Acheter {get;set;}
    
        public decimal DerniereDividente {get;set;}

        public string Industrie {get;set;} = string.Empty;

        public long CapitalisationBoursiere {get;set;}
        public List<CommentsDto> ? Comments  {get;set;}
    }
}