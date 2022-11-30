﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_State_Catalog_WCF.Models;
using Real_State_Catalog_WCF.Data;

namespace Real_State_Catalog_WCF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PictureController : Controller
    {
        private readonly AppContextDb _context;
        private readonly IWebHostEnvironment _environment;

        public PictureController(AppContextDb context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Accommodation/ManagePictures
        [HttpGet]
        [Route("ManagePictures/{id:guid?}")]
        public async Task<IActionResult> ManagePictures(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                .Include(a => a.Pictures)
                .Include(a => a.Rooms)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (accommodation == null)
            {
                return NotFound();
            }

            if (accommodation.Pictures.Count == 0)
            {
                ViewBag.AlertType = "Warning";
                ViewBag.AlertMsg = "Please add at least one photo to your listing!";
            }

            if (TempData["AlertType"] != null && TempData["AlertMsg"] != null)
            {
                ViewBag.AlertType = TempData["AlertType"];
                ViewBag.AlertMsg = TempData["AlertMsg"];
            }

            return View(accommodation);
        }

        // POST: Accommodation/ManagePictures
        [HttpPost]
        [Route("ManagePictures/{id:guid?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePictures(Guid? id, List<IFormFile> files)
        {
            if (id == null)
            {
                return NotFound();
            }

            Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "upload"));

            foreach (var formFile in files)
            {
                if (await _context.Pictures.CountAsync(p => p.AccommodationId == (Guid)id) == 12)
                {
                    TempData["AlertType"] = "Warning";
                    TempData["AlertMsg"] = "You have reached the maximum number of photos! One or more photos could not be added.";

                    return RedirectToAction("ManagePictures", new { id });
                }

                if (formFile.Length > 0)
                {
                    string fileName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + "_" + Guid.NewGuid().ToString("N")
                        + Path.GetExtension(formFile.FileName);

                    string filePath = Path.Combine(_environment.WebRootPath, "upload", fileName);

                    using var stream = System.IO.File.Create(filePath);
                    await formFile.CopyToAsync(stream);

                    await _context.Pictures.AddAsync(new Picture((Guid)id, fileName));
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("ManagePictures", new { id });
        }

        // GET: Accommodation/DeletePicture
        [HttpDelete]
        [Route("DeletePicture/{id:guid}")]
        public async Task<IActionResult> DeletePicture(Guid id, Guid accommodationId)
        {
            //Перевірка чи є у користувача фото
            var picture = await _context.Pictures.FindAsync(id);

            string filePath = Path.Combine(_environment.WebRootPath, "upload", picture.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManagePictures", new { id = accommodationId });
        }
    }
}
