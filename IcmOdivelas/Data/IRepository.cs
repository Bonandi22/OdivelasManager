using IcmOdivelas.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IcmOdivelas.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();
        Task ListDropdowns(ViewDataDictionary viewData, int? categoryId = null, int? groupId = null, int? situationId = null)

        //Member
        Task<List<Member>> GetMembersAsync();
        Task<Member?> GetMemberByIdAsync(int? id);

        //Category
        Task<List<Category>> GetAllCategoryAsync();

        //Function
        Task<List<Function>> GetAllFunctionAsync();

        //Group
        Task<List<Group>> GetAllGroupAsync();

        //Situation
        Task<List<Situation>> GetAllSituationAsync();

    }
}
