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
    public class MercaderiaController : ControllerBase
    {
        private readonly IMercaderiaService _service;

        public MercaderiaController(IMercaderiaService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Mercaderia), StatusCodes.Status201Created)]
        public IActionResult AddMercaderia([FromBody]AddMercaderiaDTO mercaderiaDTO)
        {
            bool isValid = ModelState.IsValid;
            try
            {
                return new JsonResult(_service.AddMercaderia(mercaderiaDTO)) { StatusCode = 201};
            }
            catch (InvalidIdentifier e)
            {
                Response.StatusCode = 404;
                string jsonString = $"{{\"Error\": \"{e.Message}\"}}";
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Mercaderia), StatusCodes.Status200OK)]
        public IActionResult GetMercaderia(int id)
        {
            try
            {
                return new JsonResult(_service.GetMercaderia(id)) { StatusCode = 200 };
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Mercaderia), StatusCodes.Status200OK)]
        public IActionResult GetMercaderiasByTipos([FromQuery]List<int> tiposId)
        {
            try
            {
                return new JsonResult(_service.GetMercaderiasByTipos(tiposId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Mercaderia), StatusCodes.Status200OK)]
        public IActionResult UpdateMercaderia([FromBody]UpdateMercaderiaDTO mercaderiaDTO)
        {
            try
            {
                return new JsonResult(_service.UpdateMercaderia(mercaderiaDTO));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Mercaderia), StatusCodes.Status200OK)]
        public IActionResult DeleteMercaderia(int id)
        {
            try
            {
                _service.RemoveMercaderia(id);
                string jsonString = $"{{ \"Status\": \"Mercaderia eliminada.\" }}";
                return Content(jsonString, "application/json");
            } 
            catch (InvalidIdentifier e)
            {
                Response.StatusCode = 404;
                string jsonString = $"{{\"Error\": \"{e.Message}\"}}";
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
