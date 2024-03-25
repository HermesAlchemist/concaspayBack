using AutoMapper;
using ConcasPay.Domain;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;
using jwtRegisterLogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcasPay.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuariosController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public ActionResult<Response<string>> GetUsuario()
    {
        Response<string> response = new Response<string>();

        response.Mensagem = "Acessei";

        return Ok(response);
    }
}
