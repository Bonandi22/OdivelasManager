using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IcmOdivelas.Models;
using IcmOdivelas.Data;
using NuGet.Protocol.Core.Types;

namespace IcmOdivelas.Controllers
{
    public class MembersController : Controller
    {
        private readonly IRepository _repo;

        public MembersController(IRepository repository)
        {
            _repo = repository;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var memberList = await _repo.GetMembersAsync();
            return View(memberList);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _repo.GetMembersAsync() == null)
            {
                return NotFound();
            }

            var member = await _repo.GetMembersAsync();
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public async Task<IActionResult> Create()
        {
            var categories = _repo.GetAllCategoryAsync(); 
            var groups = _repo.GetAllGroupAsync(); 
            var situations = _repo.GetAllSituationAsync(); 

            ViewData["CategoryId"] = new SelectList(await _repo.GetAllCategoryAsync(), "Id", "Name");
            ViewData["GroupId"] = new SelectList(await _repo.GetAllGroupAsync(), "Id", "Name");
            ViewData["SituationId"] = new SelectList(await _repo.GetAllSituationAsync(), "Id", "Name");
            return View();
        }

        // POST: Members/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,BirthDate,Address,City,Region,Isbaptized,Nacionality,CategoryId,GroupId,SituationId")] Member member)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(member);
                _repo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _repo.GetAllCategoryAsync(), "Id", "Name", member.CategoryId);
            ViewData["GroupId"] = new SelectList(await _repo.GetAllGroupAsync(), "Id", "Name", member.GroupId);
            ViewData["SituationId"] = new SelectList(await _repo.GetAllSituationAsync(), "Id", "Name", member.SituationId);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _repo.GetMemberByIdAsync(id.Value);
            if (member == null)
            {
                return NotFound();
            }

            await _repo.ListDropdowns(ViewData, member.CategoryId, member.GroupId, member.SituationId);

            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,BirthDate,Address,City,Region,Isbaptized,Nacionality,CategoryId,GroupId,SituationId")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(member);
                    _repo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await _repo.ListDropdowns(ViewData, member.CategoryId, member.GroupId, member.SituationId);

            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _repo.GetMemberByIdAsync(id.Value);
            if (member == null)
            {
                return NotFound();
            }

            await _repo.ListDropdowns(ViewData, member.CategoryId, member.GroupId, member.SituationId);

            return View(member);

        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _repo.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _repo.Delete(member);
            _repo.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            var member =  _repo.GetMemberByIdAsync(id);
            return member != null;
        }
    }
}
