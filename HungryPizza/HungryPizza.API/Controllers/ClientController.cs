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
        [HttpGet("email/{email}")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                return Ok(await _serviceClient.Get(email));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        //GET api/<ClientController>/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _serviceClient.Get(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // POST api/<ClientController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestClient client)
        {
            try
            {
                client.Id = Guid.NewGuid();

                if (await _serviceClient.Add(_mapper.Map<Client>(client)))
                {
                    return Ok(new { ClientId = client.Id });
                }

                return BadRequest(new FailResponse($"Erro ao cadastrar cliente. Nome: {client.Name}."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // PUT api/<ClientController>/5
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

                return BadRequest(new FailResponse($"Erro alterar o cadastro do cliente."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (await _serviceClient.Delete(id))
                {
                    return NoContent();
                }

                return BadRequest(new FailResponse($"Erro ao deletar cadastro do cliente: {id}."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }
    }
}
