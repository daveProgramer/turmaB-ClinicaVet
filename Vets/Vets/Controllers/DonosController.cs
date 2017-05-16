using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class DonosController : Controller
    {
        private VetsDB db = new VetsDB();

        // GET: Donos
        public ActionResult Index()
        {
            return View(db.Donos.ToList().OrderBy(dd => dd.Nome));
        }

        // GET: Donos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // GET: Donos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,NIF")] Donos dono)
        {
            // determinar o ID a fornecer ao novo dono

            if (ModelState.IsValid)
            {
                db.Donos.Add(dono);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dono);
        }

        // GET: Donos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // POST: Donos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonoID,Nome,NIF")] Donos donos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donos);
        }

        // GET: Donos/Delete/5
        public ActionResult Delete(int? id)
        {

            // se o parâmetro ID não for fornecido...
            if (id == null)
            {
                // redirecionamos para a listagem dos 'donos'
                return RedirectToAction("Index");
            }

            // pesquisar o 'dono' associado ao ID
            Donos dono = db.Donos.Find(id);

            // se o 'dono' não for encontrado
            // redirecionamos para a listagem dos 'donos'
            if (dono == null)
            {
                // redirecionamos para a listagem dos 'donos'
                return RedirectToAction("Index");
            }

            // mostra a view com os dados do 'dono'
            return View(dono);
        }




        // POST: Donos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // procura na BD por um 'dono'
            // cujo DonoID seja igual ao parâmetro fornecido
            Donos dono = db.Donos.Find(id);

            try
            {
                // retira à BD o objeto identificado acima
                db.Donos.Remove(dono);

                // torna definitiva a alteração na BD
                // faz 'commit'
                db.SaveChanges();
                // redireciona o controlo da ação para a view 'Index'
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("",
                   string.Format("Ocorreu um erro " +
                            "na eliminação do dono {0}-{1}", id, dono.Nome)
                   );
                return View(dono);
            }
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