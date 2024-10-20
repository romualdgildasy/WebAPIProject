using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Dtos.Comments;
using WebAPIProject.Models;

namespace WebAPIProject.Mappers
{
    public static class CommentsMappers
    {
        public static CommentsDto ToCommentsDto (this Comments commentsModel)
        {
            return new CommentsDto
            {
                Id = commentsModel.Id,
                Titre = commentsModel.Titre,
                Contenu = commentsModel.Contenu,
                DateCreation = commentsModel.DateCreation,
                StockageId = commentsModel.StockageId,
            };
        }

        public static Comments ToCommentsFromCreate (this CreateCommentsDto commentsDto, int StockageId)
        {
            return new Comments
            {
                Titre = commentsDto.Titre,
                Contenu = commentsDto.Contenu,
                StockageId = StockageId
               
            };
            
        }

         public static Comments ToCommentsFromUpdate (this UpdateCommentsRequestDto commentsDto)
        {
            return new Comments
            {
                Titre = commentsDto.Titre,
                Contenu  = commentsDto.Contenu,
        
               
            };
            
        }
        }
    }
