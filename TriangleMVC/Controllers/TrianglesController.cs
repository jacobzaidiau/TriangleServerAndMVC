using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TriangleMVC.Models;

namespace TriangleMVC.Controllers
{
    public class TrianglesController : Controller
    {
        private TriangleDBEntities db = new TriangleDBEntities();

        // GET: Triangles
        public ActionResult Index()
        {
            return View(db.Triangles.ToList());
        }

        // GET: Triangles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Triangle triangle = db.Triangles.Find(id);
            if (triangle == null)
            {
                return HttpNotFound();
            }
            return View(triangle);
        }

        // GET: Triangles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Triangles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Length_1,Length_2,Length_3,Equilateral,Isosceles,Scalene,Right_Angle,Publish_Time")] Triangle triangle)
        {
            if (ModelState.IsValid)
            {
                db.Triangles.Add(triangle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(triangle);
        }

        // GET: Triangles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Triangle triangle = db.Triangles.Find(id);
            if (triangle == null)
            {
                return HttpNotFound();
            }
            return View(triangle);
        }

        // POST: Triangles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Length_1,Length_2,Length_3,Equilateral,Isosceles,Scalene,Right_Angle,Publish_Time")] Triangle triangle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(triangle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(triangle);
        }

        // GET: Triangles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Triangle triangle = db.Triangles.Find(id);
            if (triangle == null)
            {
                return HttpNotFound();
            }
            return View(triangle);
        }

        // POST: Triangles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Triangle triangle = db.Triangles.Find(id);
            db.Triangles.Remove(triangle);
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
