using JPTBackend.Models;

namespace JPTBackend.DTOs
{
    public class ResumeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<JobApplication> Applications { get; set; }
    }
}
