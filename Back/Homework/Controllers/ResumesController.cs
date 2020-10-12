using Aibidia.API.Common.Controllers.BaseControllers;
using Aibidia.API.Common.Models;
using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using Homework.Constants;
using Homework.DTOs;

namespace Homework.Controllers
{
    [RoutePrefix(RouteConfigs.RoutePrefix)]
    public class ResumesController : BaseApiController<Resume, ResumeDto, int>
    {
        protected override DbSet<Resume> DbSet => Context.Resumes;

        protected override Expression<Func<Resume, ResumeDto>> AsDto => ResumeDto.AsDto;

        [HttpGet]
        [Route("resumes")]
        public async Task<IHttpActionResult> Get()
        {
            return await GetAllAsync();
        }

        [HttpGet]
        [Route("resumes/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await GetByIdAsync(id);
        }

        [HttpPost]
        [Route("resumes")]
        public async Task<IHttpActionResult> Post(ResumeDto dto)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("resumes/{id}")]
        public async Task<IHttpActionResult> Put(int id, ResumeDto model)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("resumes/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        protected override int GetKey(Resume model)
        {
            return model.ResumeId;
        }

        protected override Expression<Func<Resume, bool>> KeyIs(int key)
        {
            return line => line.ResumeId == key;
        }
    }
}
