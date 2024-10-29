using JPTBackend.DTOs.Request;
using JPTBackend.DTOs.Response;
using JPTBackend.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace JPTBackend.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly DataContext _context;

        public ApplicationService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationResponseDto>> GetApplications()
        {
            List<JobApplication> jobApplications = await _context.JobApplications.Include(a => a.Resume).ToListAsync();
            return jobApplications.Select(a => MapToDTO(a)).ToList();
        }

        public async Task<ApplicationResponseDto> GetApplicationById(int ApplicationId)
        {
            JobApplication jobApplication = await _context.JobApplications.Include(a => a.Resume).FirstOrDefaultAsync(a => a.Id == ApplicationId);
            return MapToDTO(jobApplication);
        }

        public async Task<ApplicationResponseDto?> CreateApplication(CreateUpdateApplicationDto application)
        {
            Resume _resume = await _context.Resumes.FindAsync(application.ResumeId);

            // Resume was not found.
            if (_resume == null)
            {
                return null;
            }

            JobApplication jobApplication = new JobApplication
            {
                Name = application.Name,
                Description = application.Description,
                JobUrl = application.JobLink,
                ResumeId = application.ResumeId,
                Resume = _resume
            };

            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return MapToDTO(jobApplication);
        }

        public async Task<ApplicationResponseDto?> UpdateApplication(int ApplicationId, CreateUpdateApplicationDto application)
        {
            JobApplication _application = await _context.JobApplications.FindAsync(application.ResumeId);

            // Application to update was not found.
            if (_application == null)
            {
                return null;
            }

            _application.Name = application.Name;
            _application.Description = application.Description;
            _application.JobUrl = application.JobLink;
            _application.ResumeId = application.ResumeId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return MapToDTO(_application);
        }

        public async Task<bool> DeleteApplication(int ApplicationId)
        {
            JobApplication application = await _context.JobApplications.FindAsync(ApplicationId);

            // Application to update was not found.
            if (application == null)
            {
                return false;
            }

            _context.JobApplications.Remove(application);
            await _context.SaveChangesAsync();

            return true;
        }


        private ApplicationResponseDto MapToDTO(JobApplication application)
        {
            return new ApplicationResponseDto
            {
                Id = application.Id,
                Description = application.Description,
                JobUrl = application.JobUrl,
                Name = application.Name,
                Resume = new ResumeResponseDto
                {
                    Id = application.Resume.Id,
                    Name = application.Resume.Name
                },
            };
        }
    }
}
