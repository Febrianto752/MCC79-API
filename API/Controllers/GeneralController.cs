using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GeneralController<TContractRepository, TEntity> : ControllerBase
        where TContractRepository : IGeneralRepository<TEntity>
        where TEntity : class
    {
        protected readonly TContractRepository _repository;

        public GeneralController(TContractRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _repository.GetAll();

            if (!entities.Any())
            {
                return NotFound();
            }

            return Ok(entities);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {

            var entity = _repository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create(TEntity entity)
        {
            var createdEntity = _repository.Create(entity);
            return Ok(createdEntity);
        }

        [HttpPut]
        public IActionResult Update(TEntity entity)
        {
            var isUpdated = _repository.Update(entity);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok(new { message = "Success updated data" });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _repository.Delete(guid);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok(new { message = "Success deleted data" });
        }
    }
}
