using CV_ASPMVC_GROUP2.Models;

namespace CV_ASPMVC_GROUP2.Repositories.Abstract
{
    public interface IUserService : IRepository<User>
    {
        Task<string> GetUserIdByUsernameAsync(string username);
    }
}
