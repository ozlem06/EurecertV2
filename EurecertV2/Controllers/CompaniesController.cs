using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EurecertV2.Data;
using EurecertV2.Models;
using Microsoft.AspNetCore.Authorization;

namespace EurecertV2.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Companies.Include(c => c.City).Include(c => c.CompanyFunction).Include(c => c.Country).Include(c => c.SalesPerson).Include(c => c.Source);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.City)
                .Include(c => c.CompanyFunction)
                .Include(c => c.Country)
                .Include(c => c.SalesPerson)
                .Include(c => c.Source)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["CompanyFunctionId"] = new SelectList(_context.CompanyFunctions, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["SalesPersonId"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            ViewData["SourceId"] = new SelectList(_context.Sources, "Id", "Name");
            var model = new Company();
            model.CreatedBy = User.Identity.Name;
            model.UpdatedBy = User.Identity.Name;
            return View(model);
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CompanyFunctionId,CountryId,CityId,Address,Email,Phone,Website,SourceId,SalesPersonId,ProposalAbstract,ProposalFile,ProposalResult,DownPayment,TotalAmount,CompanyRequests,VisitCount,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", company.CityId);
            ViewData["CompanyFunctionId"] = new SelectList(_context.CompanyFunctions, "Id", "Name", company.CompanyFunctionId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", company.CountryId);
            ViewData["SalesPersonId"] = new SelectList(_context.ApplicationUser, "Id", "FullName", company.SalesPersonId);
            ViewData["SourceId"] = new SelectList(_context.Sources, "Id", "Name", company.SourceId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", company.CityId);
            ViewData["CompanyFunctionId"] = new SelectList(_context.CompanyFunctions, "Id", "Name", company.CompanyFunctionId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", company.CountryId);
            ViewData["SalesPersonId"] = new SelectList(_context.ApplicationUser, "Id", "FullName", company.SalesPersonId);
            ViewData["SourceId"] = new SelectList(_context.Sources, "Id", "Name", company.SourceId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CompanyFunctionId,CountryId,CityId,Address,Email,Phone,Website,SourceId,SalesPersonId,ProposalAbstract,ProposalFile,ProposalResult,DownPayment,TotalAmount,CompanyRequests,VisitCount,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", company.CityId);
            ViewData["CompanyFunctionId"] = new SelectList(_context.CompanyFunctions, "Id", "Name", company.CompanyFunctionId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", company.CountryId);
            ViewData["SalesPersonId"] = new SelectList(_context.ApplicationUser, "Id", "FullName", company.SalesPersonId);
            ViewData["SourceId"] = new SelectList(_context.Sources, "Id", "Name", company.SourceId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.City)
                .Include(c => c.CompanyFunction)
                .Include(c => c.Country)
                .Include(c => c.SalesPerson)
                .Include(c => c.Source)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
