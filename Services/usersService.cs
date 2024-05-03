using API_LMFY.Interfaces.users;
using API_LMFY.Models.users;

namespace API_LMFY.Services
{
    public class usersService : Iusers
    {
        public Task InicializarDB()
        {
            throw new NotImplementedException();
        }

        public Task<usersModel> userGetID(int userID)
        {
            Console.WriteLine("userID DIGITADO: ", userID);
            return null;
        }

        public Task<int> usersDelete(usersModel userINFO)
        {
            throw new NotImplementedException();
        }

        public Task<List<usersModel>> usersGetALL()
        {
            throw new NotImplementedException();
        }

        public Task<int> usersRegister(usersModel userINFO)
        {
            throw new NotImplementedException();
        }

        public Task<int> usersUpdate(usersModel userINFO)
        {
            throw new NotImplementedException();
        }
    }
}
