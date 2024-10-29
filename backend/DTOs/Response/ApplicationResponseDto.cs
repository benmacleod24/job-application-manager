using JPTBackend.Models;
using System.Text.Json.Serialization;

namespace JPTBackend.DTOs.Response
{
    public class ApplicationResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JobUrl { get; set; }
        public ResumeResponseDto Resume { get; set; }
    }
}
