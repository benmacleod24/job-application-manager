using JPTBackend.Models;

namespace JPTBackend.DTOs
{
    public class CreateUpdateApplicationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string JobLink { get; set; }
        public int ResumeId { get; set; }
    }
}
