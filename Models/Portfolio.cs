using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Models
{
    [Table ("Portfolios")]
    public class Portfolio
    {
        public string AppUserId {get;set;}
        public int StockageId {get;set;}
        public AppUser  AppUser {get;set;}
        public Stockage  Stockage {get;set;}
    }
}