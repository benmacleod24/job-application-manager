using JPTBackend.DTOs.Response;
using System.ComponentModel.DataAnnotations.Schema;

namespace JPTBackend.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<JobApplication> Applications { get; set; }
    }
}
