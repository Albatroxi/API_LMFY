using API_LMFY.Models.users;

namespace API_LMFY.Interfaces.users
{
    public interface Iusers
    {
        Task InicializarDB();
        Task<List<usersModel>> usersGetALL();
        Task<usersModel> userGetID(int userID);
        Task<int> usersRegister(usersModel userINFO);
        Task<int> usersUpdate(usersModel userINFO);
        Task<int> usersDelete(usersModel userINFO);
    }
}
