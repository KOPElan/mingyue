using MingYue.Models;

namespace MingYue.Services
{
    public interface IApplicationService
    {
        Task<List<Application>> GetAllApplicationsAsync();
        Task<Application?> GetApplicationByIdAsync(int id);
        Task<Application?> GetApplicationByAppIdAsync(string appId);
        Task<Application> CreateApplicationAsync(Application application);
        Task<Application?> UpdateApplicationAsync(Application application);
        Task<bool> DeleteApplicationAsync(int id);
        Task<bool> ReorderApplicationsAsync(List<int> applicationIds);
    }
}
