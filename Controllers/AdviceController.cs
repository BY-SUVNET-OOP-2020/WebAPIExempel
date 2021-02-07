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
        private static List<Advice> advices;

        private ILogger<AdviceController> _logger;

        public AdviceController(ILogger<AdviceController> logger)
        {
            _logger = logger;
            if (advices == null) 
            {
                advices = new List<Advice>();
                advices.Add(new Advice{Id = 0, Text = "Borsta tänderna.", Author = "Tandis"});
                advices.Add(new Advice{Id = 1, Text = "Sov 8h.", Author = "Mamma"});
                advices.Add(new Advice{Id = 2, Text = "Bädda sängen.", Author = "Pappa"});
            }
        }

        [HttpGet("random")] //api/advice/random
        public ActionResult<Advice> GetRandomAdvice()
        {
            var rng = new Random();
            int randomId = rng.Next(0, advices.Count);

            _logger.Log(LogLevel.Debug, "Randomgenerator says: " + randomId);

            return GetAdviceByID(randomId);
        }

        [HttpGet("all")] //api/advice/all
        public ActionResult<List<Advice>> GetAllAdvice()
        {
            if (advices == null) return NotFound();
            else if (advices.Count == 0) return NoContent();
            return Ok(advices);
        }

        [HttpGet("{id}")] //api/advice/id
        public ActionResult<Advice> GetAdviceByID(int id)
        {
            Advice advice = advices.Where(x => x.Id == id).FirstOrDefault<Advice>();
            if (advice == null)
            {
                return NotFound();
            }
            return Ok(advice);
        }

        [HttpPost] //api/advice
        public ActionResult<Advice> AddAdvice(Advice newAdvice)
        {
            newAdvice.Id = advices.Count;
            advices.Add(newAdvice);
            return CreatedAtAction(nameof(GetAdviceByID), new {id = newAdvice.Id}, newAdvice);
        }
    }
}