using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EAP_A1908G2_Quan.Models;
using PagedList;

namespace EAP_A1908G2_Quan.Controllers
{
    public class AlbumsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Albums
        [AllowAnonymous]
        public ActionResult Index(string search, int? Page_No, string SortOrder)
        {
            var albums = from a in db.Albums select a;
            if(!string.IsNullOrWhiteSpace(search))
                albums = albums.Where(X => X.Title.Contains(search));

            string order = SortOrder;
            switch (order)
            {
                case "Title":
                    albums = albums.OrderByDescending(a => a.Title);
                    order = "TitleIn";
                    break;
                case "TitleIn":
                    albums = albums.OrderBy(a => a.Title);
                    order = "Title";
                    break;
                case "ReleaseDate":
                    albums = albums.OrderByDescending(a => a.ReleaseDate);
                    order = "DateIn";
                    break;
                case "DateIn":
                    albums = albums.OrderBy(a => a.ReleaseDate);
                    order = "ReleaseDate";
                    break;
                default:
                    break;
            }

            albums = albums.OrderBy(x => x.AlbumId);
            int Page_Size = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(albums.ToPagedList(No_Of_Page, Page_Size));
        }

        // GET: Albums/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        [Authorize(Roles="admin",Users = "admin;")]
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,Title,ReleaseDate,Artist,Price,GenreId")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", album.GenreId);
            return View(album);
        }

        // GET: Albums/Edit/5
        [Authorize(Roles = "admin", Users = "admin;")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", album.GenreId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,Title,ReleaseDate,Artist,Price,GenreId")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", album.GenreId);
            return View(album);
        }

        // GET: Albums/Delete/5
        [Authorize(Roles = "admin", Users = "admin;")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
