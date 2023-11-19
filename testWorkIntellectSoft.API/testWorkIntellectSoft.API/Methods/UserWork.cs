using Microsoft.EntityFrameworkCore;
using testWorkIntellectSoft.API.Data;
using testWorkIntellectSoft.API.Models.DTO;
using testWorkIntellectSoft.API.Models.Struct;

namespace testWorkIntellectSoft.API.Methods
{
    public class UserWork
    {
        UserContext _context;
        public UserWork(UserContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await getUser(id);

            return new UserDTO()
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthyear = user.BirthYear,
                Phones = (from b in user.Phones
                          select new PhoneDTO
                          {
                              PhoneID = b.PhoneID,
                              PhoneNumber = b.PhoneNumber,
                          }).ToArray()
            };
        }
        public async Task DeleteUser(int id)
        {
            var user = await getUser(id);
            user.DeleteStateCode = 1;
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> CreateUser(UserDTO user)
        {
            await _context.Users.AddAsync(getDBUser(user));
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task EditUser(UserDTO user)
        {
            var dbUser = await getUser(user.ID);
            dbUser = getDBUser(user);

            await _context.SaveChangesAsync();
        }
        private async Task<UserDBStruct> getUser(int id)
        {
            var user = await _context.Users.Include(a => a.Phones).FirstOrDefaultAsync(a => a.ID == id);
            if (user == null)
                throw new Exception("По ID '" + id + "' не найдено пользователя!");

            return user;
        }

        private UserDBStruct getDBUser(UserDTO user)
        {
            return new UserDBStruct
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthYear = user.Birthyear,
                Phones = (from a in user.Phones
                          select new PhoneDBStruct
                          {
                              PhoneNumber = a.PhoneNumber,
                          }).ToArray()
            };

        }
    }
}
