using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fs_mvc_with_auth_2025_07_01.Data;
using fs_mvc_with_auth_2025_07_01.Models;

namespace fs_mvc_with_auth_2025_07_01.Controllers
{
    public class DoctorPatientModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorPatientModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DoctorPatientModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DoctorPatient.Include(d => d.Doctor).Include(d => d.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DoctorPatientModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorPatientModel = await _context.DoctorPatient
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctorPatientModel == null)
            {
                return NotFound();
            }

            return View(doctorPatientModel);
        }

        // GET: DoctorPatientModels/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PatientId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: DoctorPatientModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,PatientId")] DoctorPatientModel doctorPatientModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorPatientModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Users, "Id", "Id", doctorPatientModel.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Users, "Id", "Id", doctorPatientModel.PatientId);
            return View(doctorPatientModel);
        }

        // GET: DoctorPatientModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorPatientModel = await _context.DoctorPatient.FindAsync(id);
            if (doctorPatientModel == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Users, "Id", "Id", doctorPatientModel.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Users, "Id", "Id", doctorPatientModel.PatientId);
            return View(doctorPatientModel);
        }

        // POST: DoctorPatientModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DoctorId,PatientId")] DoctorPatientModel doctorPatientModel)
        {
            if (id != doctorPatientModel.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorPatientModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorPatientModelExists(doctorPatientModel.DoctorId))
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
            ViewData["DoctorId"] = new SelectList(_context.Users, "Id", "Id", doctorPatientModel.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Users, "Id", "Id", doctorPatientModel.PatientId);
            return View(doctorPatientModel);
        }

        // GET: DoctorPatientModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorPatientModel = await _context.DoctorPatient
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctorPatientModel == null)
            {
                return NotFound();
            }

            return View(doctorPatientModel);
        }

        // POST: DoctorPatientModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var doctorPatientModel = await _context.DoctorPatient.FindAsync(id);
            if (doctorPatientModel != null)
            {
                _context.DoctorPatient.Remove(doctorPatientModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorPatientModelExists(string id)
        {
            return _context.DoctorPatient.Any(e => e.DoctorId == id);
        }
    }
}
