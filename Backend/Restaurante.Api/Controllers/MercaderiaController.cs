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
        [ProducesResponseType(typeof(GetMercaderiaDTO), StatusCodes.Status201Created)]
        public IActionResult AddMercaderia([FromBody]AddMercaderiaDTO mercaderiaDTO)
        {
            try
            {
                return new JsonResult(_service.AddMercaderia(mercaderiaDTO)) { StatusCode = 201};
            }
            catch (InvalidIdentifier e)
            {
                Response.StatusCode = 400;
                string jsonString = $"{{\"Error\": \"{e.Message}\"}}";
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetMercaderiaDTO), StatusCodes.Status200OK)]
        public IActionResult GetMercaderia(int id)
        {
            try
            {
                return new JsonResult(_service.GetMercaderia(id)) { StatusCode = 200 };
            } 
            catch (InvalidIdentifier e)
            {
                Response.StatusCode = 400;
                string jsonString = $"{{\"Error\": \"{e.Message}\"}}";
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetMercaderiaDTO), StatusCodes.Status200OK)]
        public IActionResult GetMercaderiasByTipos([FromQuery]List<int> tipo)
        {
            try
            {
                if (tipo.Count == 0) return new JsonResult(_service.GetMercaderias());

                return new JsonResult(_service.GetMercaderiasByTipos(tipo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GetMercaderiaDTO), StatusCodes.Status201Created)]
        public IActionResult UpdateMercaderia(int id, [FromBody]AddMercaderiaDTO mercaderiaDTO)
        {
            try
            {
                return new JsonResult(_service.UpdateMercaderia(id, mercaderiaDTO));
            }
            catch (InvalidIdentifier e)
            {
                Response.StatusCode = 400;
                string jsonString = $"{{\"Error\": \"{e.Message}\"}}";
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GetMercaderiaDTO), StatusCodes.Status200OK)]
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
                Response.StatusCode = 400;
                string jsonString = $"{{\"Error\": \"{e.Message}\"}}";
                return Content(jsonString, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("tipoMercaderia")]
        [ProducesResponseType(typeof(List<GetTipoMercaderiaDTO>), StatusCodes.Status200OK)]
        public IActionResult GetTiposMercaderia()
        {
            try
            {
                return new JsonResult(_service.GetTiposMercaderia()) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
