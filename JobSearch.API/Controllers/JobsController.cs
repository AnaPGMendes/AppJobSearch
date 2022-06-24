using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobSearch.API.DataBase;
using JobSearch.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private int numberOfRegistryByPage = 5;
        private JobSearchContext _data;
        public JobsController(JobSearchContext data)
        {
            _data = data;
        }

        [HttpGet]
        public IEnumerable<Job> GetJobs(string word, string cityState, int numberOfPage = 1)
        {
            // TODO - Tratar situação onde os dois parametros são nulos
            if (word == null) word = "";
            if (cityState == null) cityState = "";

            // TODO - Add Paginação (InfinityScroll)

            return _data.Jobs
                .Where(a =>
                a.PublicationDate >= DateTime.Now.AddDays(-15) &&  // TODO - Limitar pesquisa por periodo
                a.CityState.ToLower().Contains(cityState.ToLower()) &&
                (
                    a.JobTitle.ToLower().Contains(word.ToLower()) ||
                    a.TecnologyTools.ToLower().Contains(word.ToLower()) ||
                    a.Company.ToLower().Contains(word.ToLower())
                )
            )
            .Skip(numberOfRegistryByPage * (numberOfPage - 1)) // pula o num de registros (no caso 5)
            .Take(numberOfRegistryByPage) // mostra os próximos (5) registros
            .ToList<Job>();
        }

        [HttpGet("{id}")] // Ex: 'api/Jobs/1'
        public IActionResult GetJob(int id)
        {
            Job jobDb = _data.Jobs.Find(id);

            if (jobDb == null) 
            {
                return NotFound();
            }
            return new JsonResult(jobDb);
        }

        [HttpPost] 
        public IActionResult AddJob(Job job)
        {
            // TODO - Add Validação.
            job.PublicationDate = DateTime.Now;
            _data.Jobs.Add(job);
            _data.SaveChanges();

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }
    }
}
