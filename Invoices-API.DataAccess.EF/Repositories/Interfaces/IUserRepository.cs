using Invoices_API.DataAccess.EF.DTO;
using Invoices_API.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices_API.DataAccess.EF.Repositories.Interfaces
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> GetUserByName(string username);

        Task<User> CreateUser(UserDTO user);

        Task<User> UpdateUser(int id, string email, string password);

        Task DeleteUser(int id);
    }
}
