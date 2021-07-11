using System.Collections.Generic;
using Streamish.Models;


namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        void Add(UserProfile userProfile);
        void Delete(int id);
        UserProfile GetById(int id);
        void Update(UserProfile userProfile);
        UserProfile GetVideosByUser(int id);
    }
}