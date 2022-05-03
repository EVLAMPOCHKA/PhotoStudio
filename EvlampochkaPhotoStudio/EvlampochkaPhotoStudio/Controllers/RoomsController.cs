#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvlampochkaPhotoStudio.Data;
using EvlampochkaPhotoStudio.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;

namespace EvlampochkaPhotoStudio.Controllers
{
    public class RoomsController : Controller
    {
        private readonly EvlampochkaPhotoStudioContext _context;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly UserManager<User> _userManager;

        private static List<string> imageResources = new List<string>();

        public RoomsController(EvlampochkaPhotoStudioContext context, IWebHostEnvironment hostEnv, UserManager<User> userManager)
        {
            _context = context;
            _hostEnv = hostEnv;
            _userManager = userManager;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Room.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            else
            {
                room.Category = _context.Category.Find(room.CategoryId);
            }
            room.Comments = _context.Comment.Where(c => c.Room.Id == room.Id).ToList();
            ViewBag.Photos = _context.Photo.Where(p => p.Room.Id == room.Id).ToList();
            User user = await _userManager.GetUserAsync(User);
            ViewBag.IsInFavorite = _context.Favorite.Any(f => f.User == user && f.Room == room);
            ViewBag.IsInBooking = _context.Booking.Any(b => b.User == user && b.Room == room);
            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            List<Category> allCategories = _context.Category.ToList();
            ViewBag.AllCategories = new SelectList(allCategories, "Id", "Name");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,Description,Price")] Room room)
        {
            room.Category = _context.Category.Find(room.CategoryId);
            if (ModelState.IsValid)
            {
                room.Category = _context.Category.Find(room.CategoryId);
                _context.Add(room);
                room = _context.Room.ToList().OrderBy(x => x.Id).LastOrDefault();
                foreach (string url in imageResources)
                {
                    Photo photo = new Photo() { ImageResource = url, Room = room };
                    _context.Add(photo);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            else
            {
                room.Category = _context.Category.Find(room.CategoryId);
            }
            List<Category> allCategories = _context.Category.ToList();
            ViewBag.AllCategories = new SelectList(allCategories, "Id", "Name", room.CategoryId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Room.FindAsync(id);
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }

        [HttpPost]
        public void Upload(IFormFile file)
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("dlrmdokvi", "687753493891478", "2QB3giOBidYpol1__fj1J4G3Xrc");

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            var fileDic = "Files";
            var fileName = file.FileName;
            string filePath = Path.Combine(fileDic, fileName);
            var publicID = file.FileName;

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new CloudinaryDotNet.FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = publicID
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            string url = cloudinary.Api.UrlImgUp.BuildUrl(publicID + filePath.Substring(filePath.LastIndexOf(".")));

            imageResources.Add(url);
        }

        [HttpPost]
        public IActionResult Search(string info)
        {
            if (info != null)
            {

                info = info.ToLower();
                List<Room> rooms = new List<Room>();
                var dbRooms = _context.Room.ToList();

                foreach (var room in dbRooms)
                {
                    room.Category = _context.Category.Find(room.CategoryId);
                    if (room.Name.ToLower().Contains(info) || room.Category.Name.ToLower().Contains(info))
                    {
                        rooms.Add(room);
                    }

                }

                if (rooms.Any())
                    return View("Index", rooms);

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([Bind("Id,Text,CreationDate,RoomId")] Comment comment)
        {
            comment.User = await _userManager.GetUserAsync(User);
            comment.UserName = comment.User?.UserName ?? "Гость";
            comment.Room = _context.Room.Find(comment.RoomId);
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = comment.Room.Id });
        }


        public async Task<IActionResult> AddToFavorite(int? id)
        {
            Favorite favorite = new Favorite();
            favorite.RoomId = id;
            favorite.User = await _userManager.GetUserAsync(User);
            favorite.Room = _context.Room.Find(id);
            Favorite currentFavorite = _context.Favorite.FirstOrDefault(f => f.User == favorite.User && f.Room == favorite.Room);
            if (currentFavorite != null)
            {
                _context.Remove(currentFavorite);
            }
            else
            {
                _context.Add(favorite);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = favorite.Room.Id });
        }

        public async Task<IActionResult> Favorite()
        {
            User user = await _userManager.GetUserAsync(User);
            List<Favorite> favoriteList = _context.Favorite.Where(f => f.User == user).ToList();
            List<Room> roomList = new List<Room>();
            foreach (Favorite f in favoriteList)
            {
                roomList.Add(_context.Room.Find(f.RoomId));
            }
            return View("Favorite", roomList);
        }


        public async Task<IActionResult> BookedRoom()
        {
            User user = await _userManager.GetUserAsync(User);
            List<Booking> bookingList = _context.Booking.Where(f => f.User == user).ToList();
            List<Room> roomList = new List<Room>();
            foreach (Booking b in bookingList)
            {
                roomList.Add(_context.Room.Find(b.RoomId));
            }
            return View("BookedRoom", roomList);
        }

        public async Task<IActionResult> RemoveFromFavorite(int? id)
        {
            User user = await _userManager.GetUserAsync(User);
            Room room = _context.Room.Find(id);
            Favorite favorite = _context.Favorite.FirstOrDefault(f => f.User == user && f.Room == room);
            if (favorite != null)
            {
                _context.Remove(favorite);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Favorite));
        }

        public async Task<IActionResult> RemoveFromBooking(int? id)
        {
            User user = await _userManager.GetUserAsync(User);
            Room room = _context.Room.Find(id);
            Booking booking = _context.Booking.FirstOrDefault(f => f.User == user && f.Room == room);
            if (booking != null)
            {
                BookedDates bookedDates = _context.BookedDates.FirstOrDefault(b=>b.RoomId == booking.RoomId && b.Date == booking.BookingDate);
                _context.Remove(booking);
                _context.Remove(bookedDates);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BookedRoom));
        }
    }
}
