using Application;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        [ProducesResponseType(typeof(GetComandaDTO), StatusCodes.Status201Created)]
        public IActionResult AddComanda([FromBody]AddComandaDTO comanda)
        {
            try
            {
                return new JsonResult(_service.AddComanda(comanda)) { StatusCode = 201 };
            }
            catch (InvalidIdentifier e)
            {
                string jsonString = $"{{ \"Error\": \"{e.Message}\" }}";
                Response.StatusCode = 400;
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetComandaDTO), StatusCodes.Status200OK)]
        public IActionResult GetComanda(string id)
        {
            try
            {
                return new JsonResult(_service.GetComanda(id)) { StatusCode = 200 };
            }
            catch (InvalidGUID e)
            {
                string jsonString = $"{{ \"Error\": \"{e.Message}\" }}";
                Response.StatusCode = 400;
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetComandaDTO), StatusCodes.Status200OK)]
        public IActionResult GetComandaByDate([FromQuery]string fecha = "")
        {
            try
            {
                return new JsonResult(_service.GetComandaByDateList(fecha)) { StatusCode = 200 };
            }
            catch (InvalidDate e)
            {
                string jsonString = $"{{ \"Error\": \"{e.Message}\" }}";
                Response.StatusCode = 400;
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("formaEntrega")]
        [ProducesResponseType(typeof(List<GetFormaEntregaDTO>), StatusCodes.Status200OK)]
        public IActionResult GetFormasEntrega()
        {
            try
            {
                return new JsonResult(_service.GetFormasEntrega()) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
