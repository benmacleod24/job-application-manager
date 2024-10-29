using JPTBackend.DTOs.Request;
using JPTBackend.DTOs.Response;
using JPTBackend.Models;

namespace JPTBackend.Services
{
    public interface IResumeService
    {
        public Task<ResumeResponseDto> CreateResume(CreateResumeDto Resume);
        public Task<ResumeResponseDto> UpdateResume(int ResumeId, CreateResumeDto Resume);
        public Task<bool> DeleteResume(int ResumeId);
        public Task<ResumeResponseDto> GetResumeById(int ResumeId);
        public Task<List<ResumeResponseDto>> GetResumes();
    }
}
