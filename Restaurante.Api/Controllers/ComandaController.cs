using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Domain.DTOs;
using Application;

namespace Restaurante.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaService _service;
        
        public ComandaController(IComandaService serv)
        {
            _service = serv;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Comanda), StatusCodes.Status201Created)]
        public IActionResult AddComanda([FromBody]AddComandaDTO comanda)
        {
            try
            {
                return new JsonResult(_service.AddComanda(comanda)) { StatusCode = 201 };
            }
            catch (InvalidIdentifier e)
            {
                string jsonString = $"{{ \"Error\": \"{e.Message}\" }}";
                Response.StatusCode = 404;
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Comanda), StatusCodes.Status200OK)]
        public IActionResult GetComanda(string id)
        {
            try
            {
                return new JsonResult(_service.GetComanda(id)) { StatusCode = 200 };
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
