using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Dtos.Comments
{
    public class CommentsDto
    {
        public int Id { get; set; }

        public string Titre { get; set; } = string.Empty;

        public string Contenu { get; set; } = string.Empty;
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public int ? StockageId {get;set;}


    }
}