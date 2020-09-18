using AutoMapper;
using HungryPizza.API.Common;
using HungryPizza.API.VO;
using HungryPizza.Domain.Core.Interfaces.Services;
using HungryPizza.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HungryPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServicePizza _servicePizza;

        public PizzaController(IServicePizza servicePizza, IMapper mapper)
        {
            _mapper = mapper;
            _servicePizza = servicePizza;
        }

        // GET: api/<PizzaController>
        [ProducesResponseType(typeof(IEnumerable<Pizza>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _servicePizza.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // GET api/<PizzaController>/5
        [ProducesResponseType(typeof(Pizza), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _servicePizza.Get(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // POST api/<PizzaController>
        [ProducesResponseType(typeof(ResponseAdded), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestPizza pizza)
        {
            try
            {
                pizza.Id = Guid.NewGuid();

                if (await _servicePizza.Add(_mapper.Map<Pizza>(pizza)))
                {
                    return Ok(new ResponseAdded() { Id = pizza.Id });
                }

                return BadRequest(new FailResponse($"Erro ao cadastrar."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // PUT api/<PizzaController>/5
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] RequestPizza pizza)
        {
            try
            {
                pizza.Id = id;

                if (await _servicePizza.Update(_mapper.Map<Pizza>(pizza)))
                {
                    return NoContent();
                }

                return BadRequest(new FailResponse($"Erro alterar o cadastro."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // DELETE api/<PizzaController>/5
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (await _servicePizza.Delete(id))
                {
                    return NoContent();
                }

                return BadRequest(new FailResponse($"Erro ao excluir."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }
    }
}
