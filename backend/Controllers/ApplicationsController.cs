using JPTBackend.DTOs;
using JPTBackend.Models;
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
        private readonly DataContext _context;
        public ApplicationsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApplicationResponseDto>> GetApplications()
        {
            List<JobApplication> applications = await _context.JobApplications.Include(a => a.Resume).ToListAsync();
            return Ok(applications.Select(x => new ApplicationResponseDto
            {
                Description = x.Description,
                JobUrl = x.JobUrl,
                Id = x.Id,
                Name = x.Name,
                Resume = new ResumeResponseDto
                {
                    Name = x.Resume.Name,
                    Id = x.Resume.Id,
                }
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationResponseDto>> GetApplicationById(int id)
        {
            JobApplication application = await _context.JobApplications.Include(a => a.Resume).FirstAsync(a => a.Id == id);
            return Ok(new ApplicationResponseDto
            {
                Name = application.Name,
                Id = application.Id,
                JobUrl = application.JobUrl,
                Description = application.Description,
                Resume = new ResumeResponseDto
                {
                    Id = application.Resume.Id,
                    Name = application.Resume.Name,
                }
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationResponseDto>> UpdateApplication(int id, CreateUpdateApplicationDto request)
        {
            JobApplication application = await _context.JobApplications.FindAsync(id);

            if (application == null)
            {
                return NotFound("Application not found.");
            }

            application.Name = request.Name;
            application.ResumeId = request.ResumeId;
            application.JobUrl = request.JobLink;
            application.Description = request.Description;

            // Attempt to save the changes.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(new ApplicationResponseDto
            {
                Name = application.Name,
                JobUrl = application.JobUrl,
                Description = application.Description,
                Id = application.Id,
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationResponseDto>> CreateApplication(CreateUpdateApplicationDto request)
        {
            Resume resume = await _context.Resumes.FindAsync(request.ResumeId);

            // The resume was not found.
            if ( resume == null)
            {
                return NotFound("Resume not found.");
            }

            JobApplication application = new JobApplication
            {
                Name = request.Name,
                Description = request.Description,
                JobUrl = request.JobLink,
                Resume = resume
            };

            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();

            return Ok(new ApplicationResponseDto
            {
                Description = request.Description,
                JobUrl = request.JobLink,
                Id = application.Id,
                Name = application.Name,
                Resume = new ResumeResponseDto
                {
                    Id = resume.Id,
                    Name = resume.Name,
                }
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplication(int id)
        {
            JobApplication application = await _context.JobApplications.FindAsync(id);

            // Application not found.
            if (application == null)
            {
                return NotFound("Application not found.");
            }

            _context.JobApplications.Remove(application);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
