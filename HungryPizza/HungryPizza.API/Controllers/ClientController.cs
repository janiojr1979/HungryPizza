using AutoMapper;
using HungryPizza.API.Common;
using HungryPizza.API.VO;
using HungryPizza.Domain.Core.Interfaces.Services;
using HungryPizza.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HungryPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceClient _serviceClient;

        public ClientController(IServiceClient serviceClient, IMapper mapper)
        {
            _mapper = mapper;
            _serviceClient = serviceClient;
        }

        // GET: api/<ClientController>
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpGet("email/{email}")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                var client = await _serviceClient.Get(email);

                if (client == null || client.Id == Guid.Empty)
                {
                    return BadRequest(new FailResponse($"Cliente não encontrado."));
                }

                return Ok(client);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        //GET api/<ClientController>/5
        [ProducesResponseType(typeof(ResponseAdded), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var client = await _serviceClient.Get(id);

                if (client == null || client.Id == Guid.Empty)
                {
                    return BadRequest(new FailResponse($"Cliente não encontrado."));
                }

                return Ok(client);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // POST api/<ClientController>
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestClient client)
        {
            try
            {
                client.Id = Guid.NewGuid();

                if (await _serviceClient.Add(_mapper.Map<Client>(client)))
                {
                    return Ok(new ResponseAdded() { Id = client.Id });
                }

                return BadRequest(new FailResponse($"Erro ao cadastrar."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // PUT api/<ClientController>/5        
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] RequestClient client)
        {
            try
            {
                client.Id = id;

                if (await _serviceClient.Update(_mapper.Map<Client>(client)))
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

        // DELETE api/<ClientController>/5        
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (await _serviceClient.Delete(id))
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
