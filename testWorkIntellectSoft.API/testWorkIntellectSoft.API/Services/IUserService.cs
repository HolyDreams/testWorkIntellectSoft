using testWorkIntellectSoft.API.Models.DTO;
using testWorkIntellectSoft.API.Models.Struct;

namespace testWorkIntellectSoft.API.Services
{
    public interface IUserService
    {
        public int GetCountPages(string? search = null);
        public void CheckNull();
        public Task<List<UserDTO>> GetUsersAsync(int page = 0, string? search = null);
        public UserAnswerDTO GetAnswer(List<UserDTO> users, int page = 0, int? totalPage = null);
        public Task<UserDTO> GetUserByIdAsync(int id);
        public Task DeleteUserAsync(int id);
        public Task<UserDTO> CreateUserAsync(UserDTO user);
        public Task EditUserAsync(UserDTO user);
    }
}
