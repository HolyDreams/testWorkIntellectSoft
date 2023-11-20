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
            _context.Update(user);
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

            var userPhones = user.Phones.Where(a => a.PhoneNumber != null && !string.IsNullOrWhiteSpace(a.PhoneNumber)).ToArray();
            var phones = getDBPhone(userPhones);
            phones.AddRange(getDelPhones(dbUser.Phones.ToArray(), userPhones));

            dbUser = getDBUser(user);
            dbUser.Phones = phones;

            _context.Update(dbUser);

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
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthYear = user.Birthyear,
                Phones = (from a in user.Phones
                          select new PhoneDBStruct
                          {
                              PhoneID = a.PhoneID,
                              PhoneNumber = a.PhoneNumber
                          }).ToArray()
                
            };
        }

        private List<PhoneDBStruct> getDBPhone(params PhoneDTO[] phone)
        {
            return (from a in phone
                    select new PhoneDBStruct
                    {
                        PhoneID = a.PhoneID,
                        PhoneNumber = a.PhoneNumber,
                        DeleteStateCode = 0
                    }).ToList();
        }

        private List<PhoneDBStruct> getDelPhones(PhoneDBStruct[] delPhones, PhoneDTO[] phone)
        {
            return (from a in delPhones
                    join b in phone on a.PhoneID equals b.PhoneID
                    into b_d
                    from b in b_d.DefaultIfEmpty()

                    where a.PhoneID != b?.PhoneID
                    select new PhoneDBStruct
                    {
                        PhoneID = a.PhoneID,
                        PhoneNumber = a.PhoneNumber,
                        DeleteStateCode = 1
                    }).ToList();

        }
    }
}
