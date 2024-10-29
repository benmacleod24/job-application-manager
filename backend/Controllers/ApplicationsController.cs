using JPTBackend.DTOs.Request;
using JPTBackend.DTOs.Response;
using JPTBackend.Models;
using JPTBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace JPTBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<ApplicationResponseDto>> GetApplications()
        {
            List<ApplicationResponseDto> applications = await _applicationService.GetApplications();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationResponseDto>> GetApplicationById(int id)
        {
            ApplicationResponseDto application = await _applicationService.GetApplicationById(id);
            return Ok(application);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationResponseDto>> UpdateApplication(int id, CreateUpdateApplicationDto request)
        {
            ApplicationResponseDto application = await _applicationService.UpdateApplication(id, request);
            return Ok(application);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationResponseDto>> CreateApplication(CreateUpdateApplicationDto request)
        {
            ApplicationResponseDto application = await _applicationService.CreateApplication(request);
            return Created(String.Empty, application);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplication(int id)
        {
            bool isDeleted = await _applicationService.DeleteApplication(id);

            // Application not found.
            if (!isDeleted)
            {
                return NotFound("Application not found.");
            }

            return NoContent();
        }
    }
}
