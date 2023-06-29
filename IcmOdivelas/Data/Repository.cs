using IcmOdivelas.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace IcmOdivelas.Data
{
    public class Repository: IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context) 
        { 
            _context = context; ;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task ListDropdowns(ViewDataDictionary viewData, int? categoryId = null, int? groupId = null, int? situationId = null)
        {
            var categories = await GetAllCategoryAsync();
            var groups = await GetAllFunctionAsync();
            var situations = await GetAllSituationAsync();
            viewData["CategoryId"] = new SelectList(categories, "Id", "Name", categoryId);
            viewData["GroupId"] = new SelectList(groups, "Id", "Name", groupId);
            viewData["SituationId"] = new SelectList(situations, "Id", "Name", situationId);
        }



        //Member
        public async Task<List<Member>> GetMembersAsync()
        {
            return await _context.Members
                .Include(m => m.Category)
                .Include(m => m.Group)
                .Include(m => m.Situation)
                .ToListAsync();
        }
        public async Task<Member?> GetMemberByIdAsync(int? id)
        {            
            return await _context.Members.FindAsync(id);
        }

        //Category
        public Task<List<Category>> GetAllCategoryAsync()
        {
            return _context.Categories.ToListAsync();
        }

        //Function
        public Task<List<Function>> GetAllFunctionAsync()
        {
            return _context.Functions.ToListAsync();
        }

        //Group
        public Task<List<Group>> GetAllGroupAsync()
        {
            return _context.Groups.ToListAsync();
        }

        //Situations
        public Task<List<Situation>> GetAllSituationAsync()
        {
            return _context.Situations.ToListAsync();
        }
    }
}
