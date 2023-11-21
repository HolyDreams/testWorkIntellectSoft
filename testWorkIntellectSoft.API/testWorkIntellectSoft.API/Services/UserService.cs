using Microsoft.EntityFrameworkCore;
using testWorkIntellectSoft.API.Data;
using testWorkIntellectSoft.API.Models.DTO;
using testWorkIntellectSoft.API.Models.Struct;

namespace testWorkIntellectSoft.API.Services
{
    public class UserService : IUserService
    {
        UserContext _context;
        public UserService(UserContext context)
        {
            _context = context;
        }
        public void CheckNull()
        {
            if (_context.Users == null)
                throw new Exception("Нет подключения к базе данных!");
        }

        public int GetCountPages(string? search = null)
        {
            return search == null ? _context.Users.Where(a => a.DeleteStateCode == 0).Count() / 50 : getSearchUser(search).Count();
        }

        public UserAnswerDTO GetAnswer(List<UserDTO> users, int page = 0, int? totalPages = null)
        {
            return new UserAnswerDTO
            {
                CurentPage = page,
                TotalPage = totalPages ?? GetCountPages(),
                Users = users
            };
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var userList = await _context.Users.Include(a => a.Phones.Where(q => q.DeleteStateCode == 0)).Where(a => a.DeleteStateCode == 0).ToArrayAsync();
            return getDBFromDtoUser(userList);
        }
        public async Task<List<UserDTO>> GetUsersAsync(int page, string? search = null)
        {
            var usersList = search == null ? await _context.Users.Include(a => a.Phones.Where(q => q.DeleteStateCode == 0)).Where(a => a.DeleteStateCode == 0).Skip(50 * page).Take(50).ToArrayAsync() : await getSearchUser(search).Skip(page * 50).Take(50).ToArrayAsync();
            return getDBFromDtoUser(usersList);
        }
        

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await getUser(id);
            return getDBFromDtoUser(user).First();
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO user)
        {
            await _context.Users.AddAsync(getDBFromDtoUser(user));
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task EditUserAsync(UserDTO user)
        {
            var dbUser = await getUser(user.ID);

            var userPhones = user.Phones.Where(a => a.PhoneNumber != null && !string.IsNullOrWhiteSpace(a.PhoneNumber)).ToArray();
            var phones = getDBPhone(userPhones);
            phones.AddRange(getDelPhones(dbUser.Phones.ToArray(), userPhones));

            dbUser = getDBFromDtoUser(user);
            dbUser.Phones = phones;

            _context.Update(dbUser);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await getUser(id);
            user.DeleteStateCode = 1;
            _context.Update(user);
            await _context.SaveChangesAsync();
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

        private UserDBStruct getDBFromDtoUser(UserDTO user)
        {
            return new UserDBStruct
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthyear = user.Birthyear,
                Phones = (from a in user.Phones
                          select new PhoneDBStruct
                          {
                              PhoneID = a.PhoneID,
                              PhoneNumber = a.PhoneNumber
                          }).ToArray()

            };
        }

        private List<UserDTO> getDBFromDtoUser(params UserDBStruct[] users)
        {
            return (from a in users
                    select new UserDTO
                    {
                        ID = a.ID,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Birthyear = a.Birthyear,
                        Phones = (from b in a.Phones
                                  select new PhoneDTO
                                  {
                                      PhoneID = b.PhoneID,
                                      PhoneNumber = b.PhoneNumber,
                                  }).ToList()
                    }).ToList();
        }

        private async Task<UserDBStruct> getUser(int id)
        {
            var user = await _context.Users.Include(a => a.Phones).FirstOrDefaultAsync(a => a.ID == id);
            if (user == null)
                throw new Exception("По ID '" + id + "' не найдено пользователя!");

            return user;
        }

        private IQueryable<UserDBStruct> getSearchUser(string search)
        {
            search = search.ToLower();
            return _context.Users.Include(a => a.Phones.Where(q => q.DeleteStateCode == 0)).Where(a => a.DeleteStateCode == 0 &&
                                               (a.FirstName != null && a.FirstName.ToLower().Contains(search)) ||
                                               (a.LastName != null && a.LastName.ToLower().Contains(search)) ||
                                               (a.Phones != null && a.Phones.Any(q => q.PhoneNumber != null && q.DeleteStateCode == 0 && q.PhoneNumber.Contains(search))));
        }
    }
}
