﻿using API.DTOs.AccountRoles;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/v1/account-roles")]
public class AccountRoleController : ControllerBase
{
    private readonly AccountRoleService _service;

    public AccountRoleController(AccountRoleService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accountRoles = _service.GetAccountRole();

        if (!accountRoles.Any())
        {
            return NotFound(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<AccountRoleDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = accountRoles
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountRole = _service.GetAccountRole(guid);
        if (accountRole is null)
        {
            return NotFound(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<AccountRoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = accountRole
        });
    }

    [HttpPost]
    public IActionResult Create(NewAccountRoleDto newAccountRoleDto)
    {
        var createdAccountRole = _service.CreateAccountRole(newAccountRoleDto);
        if (createdAccountRole is null)
        {
            return BadRequest(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<AccountRoleDto>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Successfully created",
            Data = createdAccountRole
        });
    }

    [HttpPut]
    public IActionResult Update(AccountRoleDto updateAccountRoleDto)
    {
        var update = _service.UpdateAccountRole(updateAccountRoleDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (update is 0)
        {
            return BadRequest(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check your data"
            });
        }
        return Ok(new ResponseHandler<AccountRoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteAccountRole(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<AccountRoleDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }
}

