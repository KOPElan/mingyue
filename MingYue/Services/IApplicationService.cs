using MingYue.Models;

namespace MingYue.Services
{
    /// <summary>
    /// Provides CRUD operations for managing applications
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Retrieves all applications ordered by their display order
        /// </summary>
        /// <returns>A list of all applications</returns>
        Task<List<Application>> GetAllApplicationsAsync();

        /// <summary>
        /// Retrieves an application by its numeric identifier
        /// </summary>
        /// <param name="id">The application's unique numeric identifier</param>
        /// <returns>The application if found; otherwise, null</returns>
        Task<Application?> GetApplicationByIdAsync(int id);

        /// <summary>
        /// Retrieves an application by its application identifier string
        /// </summary>
        /// <param name="appId">The application's unique string identifier</param>
        /// <returns>The application if found; otherwise, null</returns>
        Task<Application?> GetApplicationByAppIdAsync(string appId);

        /// <summary>
        /// Creates a new application. AppId is auto-generated if not provided.
        /// </summary>
        /// <param name="application">The application to create</param>
        /// <returns>The created application with generated values</returns>
        Task<Application> CreateApplicationAsync(Application application);

        /// <summary>
        /// Updates an existing application
        /// </summary>
        /// <param name="application">The application with updated values</param>
        /// <returns>The updated application if successful; otherwise, null</returns>
        Task<Application?> UpdateApplicationAsync(Application application);

        /// <summary>
        /// Deletes an application by its numeric identifier
        /// </summary>
        /// <param name="id">The application's unique numeric identifier</param>
        /// <returns>True if the deletion was successful; otherwise, false</returns>
        Task<bool> DeleteApplicationAsync(int id);

        /// <summary>
        /// Updates the display order of applications
        /// </summary>
        /// <param name="applicationIds">The ordered list of application identifiers</param>
        /// <returns>True if the reordering was successful; otherwise, false</returns>
        Task<bool> ReorderApplicationsAsync(List<int> applicationIds);
    }
}
