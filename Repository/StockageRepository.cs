using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Data;
using WebAPIProject.Dtos.Stockage;
using WebAPIProject.Helpers;
using WebAPIProject.Interfaces;
using WebAPIProject.Models;

namespace WebAPIProject.Repository
{
    public class StockageRepository : IStockageRepository
    {
        public readonly ApplicationDBContext _context;

            /*ici on s'apprette a faire l'injecion de depnses*/
        public StockageRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async  Task<Stockage> CreateAsync(Stockage stockageModel)
        {
            await _context.Stockages.AddAsync(stockageModel);
            await _context.SaveChangesAsync();
            return stockageModel;

        }

        public async Task<Stockage ?> DeleteAsync(int id)
        {
            var stockageModel = await  _context.Stockages.FirstOrDefaultAsync(x => x.Id == id); 
            if(stockageModel == null)
            {
                return null;
            }
            _context.Stockages.Remove(stockageModel);
            await _context.SaveChangesAsync();
            return stockageModel;
        }

        public async Task<List<Stockage>> GetAllAsync(QueryObject query)
        {
            /*en ajoutant un objet de requete la fonction de doit plus retourner un async*/
            /*AsQueryable() convertit l'ensemble en une requête qui peut être modifiée dynamiquement.*/
            var stockages = _context .Stockages.Include(c=> c.Comments).AsQueryable();
             /*Si query.NomCompagnie n'est pas null, vide ou constitué uniquement d'espaces blancs, 
             alors la requête stockages est filtrée pour ne contenir que 
             les enregistrements où NomCompagnie contient la chaîne spécifiée dans query.NomCompagnie.*/
            if(!string.IsNullOrWhiteSpace(query.NomCompagnie))
            {
                stockages =stockages.Where(s=> s.NomCompagnie.Contains(query.NomCompagnie));
            }

            /*De la même manière, si query.Symbole n'est pas null, vide ou constitué uniquement 
            d'espaces blancs,alors la requête stockages est filtrée pour ne contenir que 
            les enregistrements où Symbole contient la chaîne spécifiée dans query.Symbole.*/
              if(!string.IsNullOrWhiteSpace(query.Symbole))
            {
                stockages =stockages.Where(s=> s.Symbole.Contains(query.Symbole));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
               if(query.SortBy.Equals("Symbole",StringComparison.OrdinalIgnoreCase))
               {
                 stockages = query.IsDecsending ? stockages.OrderByDescending(s=>s.Symbole) : stockages.OrderBy(s=> s.Symbole);
               }
            }

            var skipNumber =(query.PageNumber - 1)*query.PageSize;
            /*ToListAsync() exécute la requête de manière asynchrone et retourne les résultats sous forme de liste.
            await attend la fin de cette opération asynchrone avant de retourner les résultats.*/
            // return await stockages.ToListAsync();
             return await stockages.Skip(skipNumber).Take(query.PageSize).ToListAsync();
             
          
        }

        public  async Task<Stockage ?> GetByIdAsync(int id)
        {   /*INCLUDE fe marche par avec Find ou FindAsync*/
             return await _context.Stockages.Include(c => c.Comments).FirstOrDefaultAsync(i=> i.Id == id);
            /*return await _context.Stockages.FindAsync(id);*/
        }

        public async Task<Stockage?> GetBySymboleAsync(string symbole)
        {
            return await _context.Stockages.FirstOrDefaultAsync(s => s.Symbole == symbole);
        }

        public Task<bool> StockExist(int id)
        {
            return _context.Stockages.AnyAsync(s=> s.Id == id);
        }

        

        public async Task<Stockage ? > UpdateAsync(int id, UpdateStockageRequestDto stockageDto)
        {
            var existingStockage = await _context.Stockages.FirstOrDefaultAsync(x => x.Id == id);

            if(existingStockage == null)
            {
                return null;
            }

            existingStockage.Symbole = stockageDto.Symbole;
            existingStockage.NomCompagnie = stockageDto.NomCompagnie;
            existingStockage.Acheter = stockageDto.Acheter;
            existingStockage.DerniereDividente = stockageDto.DerniereDividente;
            existingStockage.Industrie = stockageDto.Industrie;
            existingStockage.CapitalisationBoursiere = stockageDto.CapitalisationBoursiere;

            await _context.SaveChangesAsync();

            return existingStockage;

        }






        /* public  async   Task<List<Stockage>> GetAllAsync()
          {
              return await _context.Stockages.ToListAsync();
      }*/
    }
}