using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Data;
using WebAPIProject.Mappers;
using WebAPIProject.Interfaces;
using WebAPIProject.Models;

namespace WebAPIProject.Repository
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDBContext _context;
       
        public CommentsRepository (ApplicationDBContext context)
        {
            _context= context;
           
        }

        public async Task<Comments> CreateAsync(Comments commentsModel)
        {
            await _context.Comments.AddAsync(commentsModel);
            await _context.SaveChangesAsync();
            return commentsModel;
        }

        public async Task<Comments?> DeleteAsync(int id)
        {
            var commentsModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id ==id);
            if(commentsModel == null)
            {
                return null;
            }
            _context.Comments.Remove(commentsModel);
            await _context.SaveChangesAsync();
            return  commentsModel;
        }

        public async Task<List<Comments>> GetAllAsync()
        {
            return await _context.Comments.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comments?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comments?> UpdateAsync(int id, Comments commentsModel)
        {
            var existingComments = await _context.Comments.FindAsync( id);

            if(existingComments == null)
            {
                return null;
            }
            existingComments.Titre  = commentsModel.Titre;
            existingComments.Contenu = commentsModel.Contenu;

            await _context.SaveChangesAsync();

            return existingComments;
        }
    }

}