using Common.Models;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Common.Controllers
{
    public class GroupsController : Controller
    {        
        private readonly IRepository _repo;

        public GroupsController(IRepository repository)
        {
            _repo = repository;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var groupList = await _repo.GetAllGroupAsync();
            return View(groupList);
        }

        public async Task<IActionResult> GroupA()
        {
            var memberList = await _repo.GetMembersAsync();
            var GrupoA = memberList.Where(member => member.GroupId! == 1).ToList();
            return View(GrupoA);
        }
        public async Task<IActionResult> GroupB()
        {
            var memberList = await _repo.GetMembersAsync();
            var GrupoB = memberList.Where(member => member.GroupId! == 2).ToList();
            return View(GrupoB);
        }
        public async Task<IActionResult> GroupC()
        {
            var memberList = await _repo.GetMembersAsync();
            var GrupoC = memberList.Where(member => member.GroupId! == 3).ToList();
            return View(GrupoC);
        }
        public async Task<IActionResult> GroupD()
        {
            var memberList = await _repo.GetMembersAsync();
            var GrupoD = memberList.Where(member => member.GroupId! == 4).ToList();
            return View(GrupoD);
        }


    }
}
