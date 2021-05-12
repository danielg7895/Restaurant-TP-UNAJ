using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            try
            {
                return new JsonResult(_service.AddMercaderia(mercaderiaDTO)) { StatusCode = 201};
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

    }
}
