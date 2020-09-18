using AutoMapper;
using HungryPizza.API.Common;
using HungryPizza.API.VO;
using HungryPizza.Domain.Core.Interfaces.Services;
using HungryPizza.Domain.Models;
using HungryPizza.Infra.CrossCutting.Tools;
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
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceOrder _serviceOrder;

        public OrderController(IServiceOrder serviceOrder, IMapper mapper)
        {
            _mapper = mapper;
            _serviceOrder = serviceOrder;
        }

        // GET: api/<OrderController>
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _serviceOrder.Get(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        [ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpGet("history/{clientId}")]
        public async Task<IActionResult> GetHistory(Guid clientId)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<ResponseOrder>>(await _serviceOrder.GetAllByClient(clientId)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // POST api/<OrderController>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseAdded), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        public async Task<IActionResult> Post([FromBody] RequestOrder order)
        {
            try
            {
                order.Id = Guid.NewGuid();
                order.Items.ForEach(i => i.OrderId = order.Id);

                if (await _serviceOrder.Add(_mapper.Map<Order>(order)))
                {
                    return Ok(new ResponseAdded() { Id = order.Id });
                }

                return BadRequest(new FailResponse($"Erro ao cadastrar."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // PUT api/<OrderController>/5
        [ProducesResponseType(typeof(ResponseAdded), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] RequestOrder order)
        {
            try
            {
                order.Id = id;

                if (await _serviceOrder.Update(_mapper.Map<Order>(order)))
                {
                    return Ok(new ResponseAdded() { Id = order.Id });
                }

                return BadRequest(new FailResponse($"Erro ao cadastrar."));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new FailResponse(e.Message, e));
            }
        }

        // DELETE api/<OrderController>/5
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(FailResponse), 400)]
        [ProducesResponseType(typeof(FailResponse), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (await _serviceOrder.Delete(id))
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
