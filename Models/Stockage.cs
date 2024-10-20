using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Models
{
    [Table ("Stockages")]
    public class Stockage
    {
        public int Id {get;set;}

        public string Symbole {get;set;} = string.Empty;

        public string NomCompagnie {get;set;} = string.Empty;
        /* ici je precise que dans la base de dàonné le nombre total des chiffre doit etre
        egalement a 18 avec deux chiffres apres la virgule*/ 
        [Column(TypeName = "decimal(18,2)")]

        public decimal Acheter {get;set;}
        [Column(TypeName = "decimal(18,2)")]

        public decimal DerniereDividente {get;set;}

        public string Industrie {get;set;} = string.Empty;

        public long CapitalisationBoursiere {get;set;}
        /* ici c'est u semblant de tableau qui regroupe  les commentaires  qui seront en
        stock et nous devons mettre en reletion a travers les cles etrengere et cles primaires*/
        public  List<Comments> Comments  {get;set;} = new List<Comments>(); 
        public  List<Portfolio> Portfolios {get;set;} = new List<Portfolio> ();   

    }
}