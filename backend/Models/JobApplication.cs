using System.Text.Json.Serialization;

namespace JPTBackend.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JobUrl { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }

    }
}
