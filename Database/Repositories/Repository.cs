using Common.Models;
using Database.DataContext;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;


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

        public async Task ListDropdowns(ViewDataDictionary viewData, int? categoryId, int? groupId, int? situationId)
        {            
            // Obter todas as categorias
            var categories = await _context.Categories.ToListAsync();
            viewData["Categories"] = new SelectList(categories, "Id", "Name", categoryId);

            // Obter todos os grupos
            var groups = await _context.Groups.ToListAsync();
            viewData["Groups"] = new SelectList(groups, "Id", "Name", groupId);

            // Obter todas as situações
            var situations = await _context.Situations.ToListAsync();
            viewData["Situations"] = new SelectList(situations, "Id", "Name", situationId);
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
            return await _context.Members
                .Include(m => m.Category)
                .Include(m => m.Group)
                .Include(m => m.Situation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //public async Task<Member?> GetMemberByIdAsync(int? id)
        //{            
        //    return await _context.Members.FindAsync(id);
        //}
        public async Task<Dictionary<string, List<Member>>> GetMembersByGroupAsync()
        {
            var membersByGroup = await _context.Members
                .Include(m => m.Category)
                .Include(m => m.Group)
                .Include(m => m.Situation)
                .ToListAsync();

            var membersDictionary = new Dictionary<string, List<Member>>();

            foreach (var member in membersByGroup)
            {
                var groupName = member.Group?.Name ?? "Unknown";

                if (!membersDictionary.ContainsKey(groupName))
                {
                    membersDictionary[groupName] = new List<Member>();
                }

                membersDictionary[groupName].Add(member);
            }

            return membersDictionary;
        }

        //Category
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        //Function
        public async Task<List<Function>> GetAllFunctionAsync()
        {
            return await _context.Functions.ToListAsync();
        }

        //Group
        public async Task<List<Group>> GetAllGroupAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group?> GetGroupByIdAsync(int? id)
        {
            return await _context.Groups.FindAsync(id);
        }

        //Situations
        public async Task<List<Situation>> GetAllSituationAsync()
        {
            return await _context.Situations.ToListAsync();
        }
    }

