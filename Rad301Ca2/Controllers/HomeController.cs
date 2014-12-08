using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ca2.Models;

namespace Rad301Ca2.Controllers
{
    public class HomeController : Controller
    {
        private NewDatabase0 db = new NewDatabase0();

        //
        // GET: /Home/
        //index to show list of movies
        public ActionResult Index(string sort)
        {
            //create a sort
            if (sort == null) sort = "ascActors";
            //no.actors sort
            ViewBag.numberOrder = (sort == "ascActors") ? "descActors" : "ascActors";
            //sort by movie name
            ViewBag.nameOrder = (sort == "ascName") ? "descName" : "ascName";
            //sort by Id
            ViewBag.orderId = (sort == "ascId");

            IQueryable<Movie> movie = db.Movie;
            switch (sort)
            {
                case "descName":
                    ViewBag.nameOrder = "ascName";
                    movie = movie.OrderByDescending(m => m.movieName);
                    break;

                case "descActors":
                    ViewBag.numberOrder = "ascActors";
                    movie = movie.OrderByDescending(m => m.actorsInMovie.Count);
                    break;

                case "ascName":
                    ViewBag.nameOrder = "descName";
                    movie = movie.OrderBy(m => m.movieName);
                    break;

                case "ascActors":
                    ViewBag.numberOrder = "descActors";
                    movie = movie.OrderBy(m => m.actorsInMovie.Count);
                    break;

                default:
                    ViewBag.orderId = "ascId";
                    movie = movie.OrderBy(m => m.movieId);
                    break;
            }

            return View(movie.ToList());
        }

        //
        // GET: /Actors/

        //public ActionResult Actors()
        //{
        //    return View(db.Actor.ToList());
        //}
        //
        // GET: /Home/Details/5

        public ActionResult Details(int id = 0)
        {
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        //Get: /Home/Details/AddActor

        public ActionResult AddActor(int id = 0)
        {
            //ViewBag.ActorsList = db.Actor.ToList();
            ViewBag.Actor = new SelectList(db.Actor, "actorId", "actorFName");
            return View(db.Movie.Find(id));
        }

        //
        //POST: //Home/Details/AddActor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddActor(Movie moviesActorIn)
        {
                db.Movie.Add(moviesActorIn);
                db.SaveChanges();
                return RedirectToAction("Details");
                //return View(actorsInMovie);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movie.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}