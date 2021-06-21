using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Salon.DAL.Models;
using Salon.DAL.UnitOfWork;
using Salon.Common.Authorization;
using System.Collections.Generic;

namespace Salon.Controllers
{
    [Authorize]
    [Route("usergroup")]
    public class UserGroupController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public UserGroupController(IConfiguration configuration, IUnitOfWork IUnitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = IUnitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGroupModel>>> SearchAsync()
        {
            return Ok(await _unitOfWork.UserGroup.Search());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGroupModel>> GetByIdAsync(long id)
        {
            return Ok(await _unitOfWork.UserGroup.GetById(id));
        }
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] UserGroupModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _unitOfWork.UserGroup.Add(model);
            _unitOfWork.Commit();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserGroupModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _unitOfWork.UserGroup.Update(model);
            _unitOfWork.Commit();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ActiveByIdAsync(long id)
        {
            await _unitOfWork.UserGroup.Active(id);
            _unitOfWork.Commit();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(long id)
        {
            await _unitOfWork.UserGroup.Delete(id);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}