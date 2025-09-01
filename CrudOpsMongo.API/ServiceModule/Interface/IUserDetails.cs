using CrudOpsMongo.API.Models;

namespace CrudOpsMongo.API.ServiceModule.Interface
{
    public interface IUserDetails
    {
        Task<ResponseMessage> InsertUserDetailsAsync(UserDetailModel model);
        Task<ResponseMessage> GetUserDetailsAsync();
        Task<ResponseMessage> GetUserDetailsByIdAsync(string? id);
        Task<ResponseMessage> GetUserDetailsByNameAsync(string? name);
        Task<ResponseMessage> UpdateUserDetailsByIdAsync(UserDetailModel model);
        Task<ResponseMessage> UpdateUserSalaryByIdAsync(UserDetailModel model);
        Task<ResponseMessage> DeleteUserByIdAsync(string? id);
        Task<ResponseMessage> DeleteUserByGenderAsync(string? gender);
        Task<ResponseMessage> DeleteAllUsersAsync();
    }
}
