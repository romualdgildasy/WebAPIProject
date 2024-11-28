using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Data;
using WebAPIProject.Dtos.Stockage;
using WebAPIProject.Helpers;
using WebAPIProject.Interfaces;
using WebAPIProject.Mappers;

namespace WebAPIProject.Controllers
{
       [Route("api/stockages")]
       [ApiController]
    public class StockageController : ControllerBase
    {   
        /*readonly est un modificateur qui peut être utilisé avec des champs de données
         pour indiquer qu'ils ne peuvent être modifiés une fois initialisés, 
         c'est-à-dire qu'ils sont en lecture seule après leur initialisation 
        dans le constructeur ou lors de leur déclaration.*/
             private readonly ApplicationDBContext _context;
             private readonly IStockageRepository _stockageRepo;
        public StockageController (ApplicationDBContext context ,IStockageRepository stockageRepo)
        {
            _stockageRepo = stockageRepo;
            _context = context;
        }

            /*ici on creer un methode qui va afficher ou lire touts les elemts qu'on
            a dans la base de donnée*/
            [HttpGet]
            [Authorize]
            public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
            {   
                 if(!ModelState.IsValid)
                return BadRequest(ModelState);

                //var  stockages = _context.Stockages.ToList()
                //rn ajoutant g=query dans le getall c'est pouvoir envoyer notre object de requete
                var stockages = await _stockageRepo.GetAllAsync(query);
                /*ici nous allons ajouter la selection pour pouvoir faire un mappage */
                var stockageDto = stockages.Select(s=> s.ToStockageDto()).ToList();
                return Ok (stockages);

            }

            /* ici en testant nous allons proceder via les Id pour trouver les element dasn la BD*/
            [HttpGet]    
            [Route ("{id:int}")]
            public async Task<IActionResult> GetById([FromRoute] int id)
            {   
                 if(!ModelState.IsValid)
                return BadRequest(ModelState);

                /*ici on rechercher les element stckoer dans la base de donné via l'id*/
                var stockage = await  _stockageRepo.GetByIdAsync(id); 
                /*_context.Stockages.FindAsync(id);*/
                /*si l'id ne correspond pas ça renvoit null*/
                if (stockage == null)
                {
                    return NotFound();
                }
               /*return Ok(stockage);*/
                return Ok(stockage.ToStockageDto());
            }

            /* le HtppPost lui permet d'ajouter les elementts dans la base de donnée*/
            [HttpPost]
             public async Task<IActionResult> Create([FromBody] CreateStockageRequestDto StockageDto)
            {
                var stockageModel = StockageDto.ToStockageFromCreateDto();
                await _stockageRepo.CreateAsync(stockageModel);
              /*  await _context.Stockages.AddAsync(stockageModel);
                await _context.SaveChangesAsync();*/
                 return CreatedAtAction(nameof(GetById), new { id = stockageModel.Id }, stockageModel.ToStockageDto());
            }

            /* permet de mettre a jour un elmenet existant ta travers un id 
            ça a met le conteneu de notre base de donné et mettant egalement a jour 
            l'id d=je croid*/
            [HttpPut]
            [Route ("{id:int}")]
            public async Task<IActionResult> Udpdate([FromRoute] int id, [FromBody] UpdateStockageRequestDto updateDto)
            {   
                 if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var stockageModel = await _stockageRepo.UpdateAsync(id,updateDto);
                /*  ;*/
                if(stockageModel == null)
                {
                    return NotFound();
                }
                /*ici nous definiton de ce qui va etre mis a jour cars ses elles sont 
                stocker dabs Dto nous renvoyons les elements dans le Dto qui 
                verifie puis envoie dans la base de donnée*/
              /*  stockageModel.Symbole = updateDto.Symbole;
                stockageModel.NomCompagnie = updateDto.NomCompagnie;
                stockageModel.Acheter = updateDto.Acheter;
                stockageModel.DerniereDividente = updateDto.DerniereDividente;
                stockageModel.Industrie = updateDto.Industrie;
                stockageModel.CapitalisationBoursiere = updateDto.CapitalisationBoursiere;*/

                 await _context.SaveChangesAsync();
                return Ok(stockageModel.ToStockageDto());
            }

             [HttpDelete]
             [Route ("{id:int}")]
             public async Task<IActionResult> Delete([FromRoute] int id)
            {   
                 if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
                 /*ici on supprime l'elemen dans la bd a partir de son Id.*/
             var stockageModel = await _stockageRepo.DeleteAsync(id);
             /*_context.Stockages.FirstOrDefaultAsync(x=> x.Id == id);*/
             if(stockageModel == null)
             {
                 return NotFound();
             }
             

             /*_context.Stockages.Remove(stockageModel);
             await _context.SaveChangesAsync();*/

             return NoContent();
            }
        
        }
    }

   

