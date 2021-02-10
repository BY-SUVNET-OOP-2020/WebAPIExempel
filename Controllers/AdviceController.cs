using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("api/advices/")]
    public class AdviceController : ControllerBase
    {
        private AdviceDbContext _dbContext;
        private ILogger<AdviceController> _logger;

        public AdviceController(ILogger<AdviceController> logger, AdviceDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // [HttpGet("random")] //api/advice/random
        // public ActionResult<Advice> GetRandomAdvice()
        // {
        //     var rng = new Random();
        //     int randomId = rng.Next(0, advices.Count);

        //     _logger.Log(LogLevel.Debug, "Randomgenerator says: " + randomId);

        //     return GetAdviceByID(randomId);
        // }

        // [HttpGet("all")] //api/advice/all
        // public ActionResult<List<Advice>> GetAllAdvice()
        // {
        //     if (advices == null) return NotFound();
        //     else if (advices.Count == 0) return NoContent();
        //     return Ok(advices);
        // }

        [HttpGet("{id}")] //api/advice/id
        public async Task<ActionResult<Advice>> GetAdviceByID(int id)
        {
            Advice advice = await _dbContext.Advices.FindAsync(id);
            if (advice == null)
            {
                return NotFound();
            }
            return Ok(advice);
        }

        [HttpPost] //api/advice
        public async Task<ActionResult<Advice>> AddAdvice(Advice newAdvice)
        {
            _dbContext.Advices.Add(newAdvice);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAdviceByID), new {id = newAdvice.Id}, newAdvice);
        }
    }
}