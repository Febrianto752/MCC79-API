using API.Contracts;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class GeneralController<TRepository, TEntity> : ControllerBase
        where TRepository : IGeneralRepository<TEntity>
        where TEntity : class
    {
        protected readonly TRepository _repository;

        public GeneralController(TRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _repository.GetAll();

            if (!entities.Any())
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<TEntity>>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "data found",
                Data = entities
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {

            var entity = _repository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not found"
                });
            }

            return Ok(new ResponseHandler<TEntity>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "data found",
                Data = entity
            });
        }

        [HttpPost]
        public IActionResult Create(TEntity entity)
        {
            var createdEntity = _repository.Create(entity);

            if (createdEntity is null)
            {
                return BadRequest(new ResponseHandler<TEntity>()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Check your data",
                });
            }

            return Ok(new ResponseHandler<TEntity>()
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.Created.ToString(),
                Message = "Successfully created data",
                Data = createdEntity
            });
        }

        [HttpPut]
        public IActionResult Update(TEntity entity)
        {
            var getGuid = (Guid)typeof(TEntity).GetProperty("GUID")!.GetValue(entity)!;
            var isFound = _repository.IsExist(getGuid);

            if (isFound is false)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }

            var isUpdated = _repository.Update(entity);
            if (!isUpdated)
            {
                return BadRequest(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check your data"
                });
            }

            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isFound = _repository.IsExist(guid);

            if (isFound is false)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }

            var isDeleted = _repository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check connection to database"
                });
            }

            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully deleted"
            });
        }
    }
}
