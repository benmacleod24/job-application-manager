using JPTBackend.DTOs.Request;
using JPTBackend.DTOs.Response;

namespace JPTBackend.Services
{
    public interface IApplicationService
    {
        public Task<List<ApplicationResponseDto>> GetApplications();
        public Task<ApplicationResponseDto> GetApplicationById(int ApplicationId);
        public Task<ApplicationResponseDto> CreateApplication(CreateUpdateApplicationDto ApplicationName);
        public Task<ApplicationResponseDto> UpdateApplication(int ApplicationId, CreateUpdateApplicationDto application);
        public Task<bool> DeleteApplication(int ApplicationId);
    }
}
