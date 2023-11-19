using Microsoft.EntityFrameworkCore;
using testWorkIntellectSoft.API.Data;
using testWorkIntellectSoft.API.Models.DTO;
using testWorkIntellectSoft.API.Models.Struct;

namespace testWorkIntellectSoft.API.Methods
{
    public class UserListWork
    {
        UserContext _context;
        public UserListWork(UserContext context)
        {
            _context = context;
        }

        public int GetCountPages(string? search = null)
        {
            return search == null ? _context.Users.Where(a => a.DeleteStateCode == 0).Count() / 50 : getSearchUser(search).Count();
        }
        public async Task<UserDTO[]> GetUsers(int page, string? search = null)
        {
            var usersList = search == null ? await _context.Users.Include(a => a.Phones).Where(a => a.DeleteStateCode == 0).Skip(50 * page).Take(50).ToListAsync() : await getSearchUser(search).Skip(page * 50).Take(50).ToListAsync();
            return getUserDTO(usersList);
        }

        private UserDTO[] getUserDTO(List<UserDBStruct> users)
        {
            return (from a in users
                    select new UserDTO
                    {
                        ID = a.ID,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Birthyear = a.BirthYear,
                        Phones = (from b in a.Phones
                                  select new PhoneDTO
                                  {
                                      PhoneID = b.PhoneID,
                                      PhoneNumber = b.PhoneNumber,
                                  }).ToArray()
                    }).ToArray();
        }
        private IQueryable<UserDBStruct> getSearchUser(string search)
        {
            search = search.ToLower();
            return _context.Users.Include(a => a.Phones).Where(a => a.DeleteStateCode == 0 &&
                                             (a.FirstName != null && a.FirstName.ToLower().Contains(search)) ||
                                             (a.LastName != null && a.LastName.ToLower().Contains(search)) ||
                                             (a.Phones != null && a.Phones.Any(q => q.PhoneNumber != null && q.DeleteStateCode == 0 && q.PhoneNumber.Contains(search))));
        }
    }
}
