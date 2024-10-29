using JPTBackend.DTOs;
using JPTBackend.Models;
using JPTBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JPTBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResumeResponseDto>>> GetAllResumes()
        {
            List<ResumeResponseDto> resumes = await _resumeService.GetResumes();
            return Ok(resumes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResumeResponseDto>> GetResumeById(int id)
        {
            ResumeResponseDto resume = await _resumeService.GetResumeById(id);
            return Ok(resume);
        }

        [HttpPost]
        public async Task<ActionResult<CreateResumeResponseDto>> CreateResume(CreateResumeDto request)
        {
            ResumeResponseDto _resume = await _resumeService.CreateResume(request);

            return Created(String.Empty, _resume);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CreateResumeResponseDto>> UpdateResume(int id, CreateResumeDto resume)
        {
            ResumeResponseDto _resume = await _resumeService.UpdateResume(id, resume);
            return Ok(_resume);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResume(int id)
        {
            bool isDeleted = await _resumeService.DeleteResume(id);

            if (!isDeleted)
            {
                return NotFound("Resume not found.");
            }

            return NoContent();
        }
    }
}
