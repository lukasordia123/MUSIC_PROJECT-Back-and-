using Models;

namespace Service.interfaces
{
    public interface IDatabaseConnection
    {
        Task<User> RegisterUser(User model);
        //Task<Posts> UploadRitual(Posts model);
        //Task<Posts> UploadRitual2(Posts posts); 
        Task<List<User>> GetAllUSers();
        Task<List<Posts>> GetAllRituals();

        Task<Posts> InsertRitual(Posts model);
    }
}