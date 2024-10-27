using JPTBackend.DTOs;
using JPTBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JPTBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly DataContext _context;

        public ResumeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Resume>>> GetAllResumes()
        {
            List<Resume> resumes = await _context.Resumes.ToListAsync();
            return Ok(resumes.Select(x => new ResumeResponseDto
            {
                Id = x.Id,
                Applications = x.Applications,
                Name = x.Name,
            }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResumeResponseDto>> GetResumeById(int id)
        {
            Resume resume = await _context.Resumes.Include(r => r.Applications).FirstAsync(r => r.Id == id);
            return Ok(new ResumeResponseDto
            {
                Name = resume.Name,
                Id = resume.Id,
                Applications = resume.Applications,
            });
        }

        [HttpPost]
        public async Task<ActionResult<CreateResumeResponseDto>> CreateResume(CreateResumeDto request)
        {
            var newResume = new Resume
            {
                Name = request.Name
            };

            _context.Resumes.Add(newResume);
            await _context.SaveChangesAsync();

            return Ok(new CreateResumeResponseDto
            {
                Id = newResume.Id,
                Name = newResume.Name
            });

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CreateResumeResponseDto>> UpdateResume(int id, CreateResumeDto resume)
        {
            Resume updatedResume = await _context.Resumes.FindAsync(id);

            // Resume was not found.
            if (updatedResume == null)
            {
                return NotFound("Resume not found.");
            }

            // Updated fields in the resume.
            updatedResume.Name = resume.Name;

            // Attempt to save the changes.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

       

            return Ok(new CreateResumeResponseDto 
            { 
                Name = updatedResume.Name, 
                Id = updatedResume.Id
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResume(int id)
        {
            Resume resume = await _context.Resumes.FindAsync(id);

            if (resume == null)
            {
                return NotFound("Resume not found.");
            }

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
