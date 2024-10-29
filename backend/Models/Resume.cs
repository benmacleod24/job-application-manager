using JPTBackend.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace JPTBackend.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<JobApplication> Applications { get; set; }

        public ResumeResponseDto MapToDTO(Resume resume)
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
