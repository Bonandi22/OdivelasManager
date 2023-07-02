using Common.Models;
using Database.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Common.Controllers
{
    public class MemberFunctionsController : Controller
    {
        private readonly DataContext _context;

        public MemberFunctionsController(DataContext context)
        {
            _context = context;
        }

        // GET: MemberFunctions
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.MemberFunctions.Include(m => m.Function).Include(m => m.Member);
            return View(await dataContext.ToListAsync());
        }

        // GET: MemberFunctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberFunctions == null)
            {
                return NotFound();
            }

            var memberFunction = await _context.MemberFunctions
                .Include(m => m.Function)
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (memberFunction == null)
            {
                return NotFound();
            }

            return View(memberFunction);
        }

        // GET: MemberFunctions/Create
        public IActionResult Create()
        {
            ViewData["FunctionId"] = new SelectList(_context.Functions, "Id", "Id");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            return View();
        }

        // POST: MemberFunctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,FunctionId")] MemberFunction memberFunction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberFunction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FunctionId"] = new SelectList(_context.Functions, "Id", "Id", memberFunction.FunctionId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberFunction.MemberId);
            return View(memberFunction);
        }

        // GET: MemberFunctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberFunctions == null)
            {
                return NotFound();
            }

            var memberFunction = await _context.MemberFunctions.FindAsync(id);
            if (memberFunction == null)
            {
                return NotFound();
            }
            ViewData["FunctionId"] = new SelectList(_context.Functions, "Id", "Id", memberFunction.FunctionId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberFunction.MemberId);
            return View(memberFunction);
        }

        // POST: MemberFunctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FunctionId")] MemberFunction memberFunction)
        {
            if (id != memberFunction.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberFunction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberFunctionExists(memberFunction.MemberId))
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
            ViewData["FunctionId"] = new SelectList(_context.Functions, "Id", "Id", memberFunction.FunctionId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberFunction.MemberId);
            return View(memberFunction);
        }

        // GET: MemberFunctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberFunctions == null)
            {
                return NotFound();
            }

            var memberFunction = await _context.MemberFunctions
                .Include(m => m.Function)
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (memberFunction == null)
            {
                return NotFound();
            }

            return View(memberFunction);
        }

        // POST: MemberFunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberFunctions == null)
            {
                return Problem("Entity set 'DataContext.MemberFunctions'  is null.");
            }
            var memberFunction = await _context.MemberFunctions.FindAsync(id);
            if (memberFunction != null)
            {
                _context.MemberFunctions.Remove(memberFunction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberFunctionExists(int id)
        {
          return (_context.MemberFunctions?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
