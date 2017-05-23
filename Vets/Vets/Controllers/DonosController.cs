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
            //criar a var. que recebe o novo valor
            int novoID = 0;
            try
            {
                //determinar o novo ID
                /*novoID = (from d in db.Donos
                          orderby d.DonoID descending
                          select d.DonoID).FirstOrDefault() + 1;*/
                //novoID = db.Donos.Max(d => d.DonoID) + 1;

                //por isso atribuir manualmente o valor do 'novoID'
                novoID = 1;

                //Select max(d.DonoID)
                //from donos d

                //atribuir o 'novoID' ao objecto 'dono'
                dono.DonoID = novoID;
            }
            catch (System.Exception)
            {
                //tabela 'Donos' está vazia
                //não sendo possível devolver MAX de uma tabela

            }
            try
            {
                if (ModelState.IsValid)
                {
                    db.Donos.Add(dono);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }
            catch (System.Exception)
            {
                /*não guarda alterações
                 precisa no mínimo
                 notificar o user que o processo falhou
                 */
                ModelState.AddModelError("", "Ocorreu um erro na Adição do novo Dono.");
                /*notificar o admin/programador que ocorreu um erro
                 fazer: 1º enviar email ao programador da ocorrência de um erro
                 2º ter uma tabela, na BD, onde serão reportados os erros:
                 -Data
                 -Método
                 Controller
                 -Detalhes do erro
                 */
                 


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