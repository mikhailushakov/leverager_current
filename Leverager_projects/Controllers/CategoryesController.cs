﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Leverager_projects.Models;

namespace Leverager_projects.Controllers
{
    public class CategoryesController : Controller
    {
        private LeveragerDBContext db = new LeveragerDBContext();

        private void CategoryDropDownList(object selectedCategory = null)
        {
            //var ParentList = new List<string>();

            /*var ParentQry = from d in db.Categoryes
                            where d.Master == true
                            select new {d.Name, d.ID};*/

            var ParentQry = (from d in db.Categoryes
                             where d.Master == true
                             select d).ToList<Category>();

            //ParentList.AddRange(ParentQry.Distinct());
            //ViewBag.ParentName = new SelectList(ParentList);
            ViewBag.ParentID = new SelectList(ParentQry, "ID", "Name", selectedCategory);
            //ViewBag.ParentName = ParentQry.ToList();

        }
             
        // GET: /Categoryes/
        public ActionResult Index()
        {
            return View(db.Categoryes.ToList());
        }

        // GET: /Categoryes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categoryes.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Categoryes/Create
        public ActionResult Create(object selectedDepartment = null)
        {
            CategoryDropDownList();

            return View();
        }

        // POST: /Categoryes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Descriptions,Active,Master,ParentId")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Master == true)  {
                    category.ParentID = null;
                }
                db.Categoryes.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: /Categoryes/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categoryes.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            CategoryDropDownList(category.ParentID);

            return View(category);
        }

        // POST: /Categoryes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Descriptions,Active,Master,ParentName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Categoryes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categoryes.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Categoryes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categoryes.Find(id);
            db.Categoryes.Remove(category);
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
