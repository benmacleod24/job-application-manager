using JPTBackend.DTOs;
using JPTBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace JPTBackend.Services
{
    public class ResumeService : IResumeService
    {
        private readonly DataContext _context;

        public ResumeService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ResumeResponseDto>> GetResumes()
        {
            List<Resume> _resumes = await _context.Resumes.ToListAsync();
            return _resumes.Select(r => MapToDTO(r)).ToList();
        }

        public async Task<ResumeResponseDto> GetResumeById(int ResumeId)
        {
            Resume _resume = await _context.Resumes.FindAsync(ResumeId);
            return MapToDTO(_resume);
        }

        public async Task<ResumeResponseDto> CreateResume(CreateResumeDto Resume)
        {
            Resume _resume = new Resume
            {
                Name = Resume.Name,
            };

            _context.Resumes.Add(_resume);
            await _context.SaveChangesAsync();

            return MapToDTO(_resume);
        }

        public async Task<ResumeResponseDto> UpdateResume(int ResumeId, CreateResumeDto Resume)
        {
            Resume _resume = await _context.Resumes.FindAsync(ResumeId);

            // The resume to update was not found.
            if (_resume == null)
            {
                return null;
            }

            // Update values in the resume.
            _resume.Name= Resume.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return MapToDTO(_resume);
        }

        public async Task<bool> DeleteResume(int ResumeId)
        {
            Resume _resume = await _context.Resumes.FindAsync(ResumeId);

            // Resume to delete was not found.
            if (_resume == null)
            {
                return false;
            }

            _context.Resumes.Remove(_resume);
            await _context.SaveChangesAsync();

            return true;
        }

        private ResumeResponseDto MapToDTO(Resume resume)
        {
            return new ResumeResponseDto
            {
                Id = resume.Id,
                Name = resume.Name,
                Applications = resume.Applications,
            };
        }
    }
}
