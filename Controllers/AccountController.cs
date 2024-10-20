using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Dtos.Account;
using WebAPIProject.Interfaces;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    [Route("ap/controller")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, 
        ITokenService tokenService,SignInManager<AppUser> signInManager ) 
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        } 

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
              if(!ModelState.IsValid)
                return BadRequest(ModelState);

              var user = await _userManager.Users.FirstOrDefaultAsync(x=> x.UserName == loginDto.Username.ToLower());

              if(user == null) return Unauthorized("Invalid username");

              var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

              if(!result.Succeeded) return Unauthorized("Username not found /or password incorrect");

              return Ok (
                new NewUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
              );
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if(createUser.Succeeded)
                {
                    //ici toute personne se connectanat via la terminaison il lui sera attibué le role de utilisateur
                   var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        // return Ok("User created");
                        return Ok(
                            new NewUserDto 
                            {
                                Username = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                    
                }
                else
                {
                    return StatusCode(500,createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500 , e);
            }


        }
    }
}