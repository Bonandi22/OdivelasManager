using Common.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Database.Repositories
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();
        Task ListDropdowns(ViewDataDictionary viewData, 
                            int? categoryId = null, 
                            int? groupId = null, 
                            int? situationId = null);
        //User
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<List<User>> GetAllUsersAsync(string username);


        //Member
        Task<List<Member>> GetMembersAsync();
        Task<Member?> GetMemberByIdAsync(int? id);
        Task<Dictionary<string, List<Member>>> GetMembersByGroupAsync();

        //Category
        Task<List<Category>> GetAllCategoryAsync();

        //Function
        Task<List<Function>> GetAllFunctionAsync();

        //Group
        Task<List<Group>> GetAllGroupAsync();
        Task<Group?> GetGroupByIdAsync(int? id);

        //Situation
        Task<List<Situation>> GetAllSituationAsync();

    }
}
