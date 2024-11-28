using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Models
{
    [Table ("Comments")]
    public class Comments
    { 
        
        public int Id { get; set; }

        public string Titre { get; set; } = string.Empty;

        public string Contenu { get; set; } = string.Empty;
        
        public DateTime DateCreation { get; set; } = DateTime.Now;
        
        
        
          /* ici le ? permet de dire que mes elements sont de leur type propre et 
    peuvent prendre une valeur sans soucis 
    get : lui permet de recuperer  de la veleur de la propriete d'un element
    set: lui  permet de definir  la veleur  de la priprpiéte d'un element*/
        public int ? StockageId {get;set;}

        public Stockage? Stockage {get;set;}
        public string AppUserId {get;set;}
         
        public AppUser AppUser {get;set;}
    }
}