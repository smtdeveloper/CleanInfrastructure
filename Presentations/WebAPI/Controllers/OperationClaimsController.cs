using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(OperationClaimAddDto operationClaimAddDto)
        {
            var addResult = await _operationClaimService.AddAsync(operationClaimAddDto);
            if (!addResult.Success)
                return BadRequest(addResult);

            return Ok(addResult);
        }
    }
}
