using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Interfaces;
using WebAPIProject.Dtos.Comments;
using WebAPIProject.Mappers;
using WebAPIProject.Dtos.Stockage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using WebAPIProject.Models;
using WebAPIProject.Extension;

namespace WebAPIProject.Controllers
{
    [Route("api/comment")]
    [ApiController]   
    public class CommentsController :ControllerBase
    {
        private readonly ICommentsRepository _commentsRepo;
        private readonly IStockageRepository _stockageRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentsController (ICommentsRepository commentsRepo,IStockageRepository stockageRepo,
        UserManager<AppUser> userManager)
        {
            _commentsRepo = commentsRepo;
            _stockageRepo = stockageRepo;
            _userManager = userManager;

        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            /*ici nous allon faire ce qu'on applle état de modele permet de gérer les transitions entre différents états d’un objet
            */
            /*si notre modele de validation est correcte ça passe, s'il n'est pas correcte ça retourne une mauvaise valeurs*/
                if(!ModelState.IsValid)
                return BadRequest(ModelState);

              var comments = await _commentsRepo.GetAllAsync();
              var commentsDto = comments.Select(s=> s.ToCommentsDto());
                return Ok (commentsDto);
        }

    //en ajoutant INT devant les Id cela va faciliter la validation des données en paquets de Json
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id )
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentsRepo.GetByIdAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments.ToCommentsDto());
        }

        [HttpPost("{StockageId:int}")]
        public async Task<IActionResult> Create([FromRoute] int StockageId,
        CreateCommentsDto commentsDto)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _stockageRepo.StockExist(StockageId))
            {
                return BadRequest("Stock does not exist");
            }

            // recupeation des noms des utilisateur dans la bd apres un commmentaire
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentsModel = commentsDto.ToCommentsFromCreate(StockageId);
            // recuperation de l'id dans l'application user
            commentsModel.AppUserId = appUser.Id;
            await _commentsRepo.CreateAsync(commentsModel);
            return CreatedAtAction(nameof(GetById),new {id = commentsModel.Id},commentsModel.ToCommentsDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateCommentsRequestDto updateDto)
        { 
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentsRepo.UpdateAsync(id,updateDto.ToCommentsFromUpdate());
            if(comments == null)
            {
               return  NotFound("commnents not found");
            }
            return Ok(comments.ToCommentsDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {   
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var commentsModel = await _commentsRepo.DeleteAsync(id);
            if(commentsModel ==null)
            {
                return NotFound("Comments does not exist");
            }
               return Ok(commentsModel);
        }
    }
}