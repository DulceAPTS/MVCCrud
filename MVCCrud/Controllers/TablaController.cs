using MVCCrud.Models;
using MVCCrud.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVCCrud.Controllers
{
    public class TablaController : Controller
    {
        /// <summary>
        /// Metodo index, devuelve a la vista una lista de usuarios 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;
            using (CrudEntities db = new CrudEntities())
            {
                lst = (from d in db.tabla
                       select new ListTablaViewModel
                       {
                           ID = d.ID,
                           Nombre = d.Nombre,
                           Correo = d.Correo,
                           Fecha_nacimiento = (DateTime)d.Fecha_nacimiento
                       }).ToList();
            }

            return View(lst);

        }

        /// <summary>
        /// Metodo GET Nuevo, que redirige a  la vista para crear un usuario 
        /// </summary>
        /// <returns></returns>

        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    using (CrudEntities db = new CrudEntities()) 
                    {
                        var oTabla = new tabla();
                        oTabla.Correo = model.Correo;
                        oTabla.Fecha_nacimiento = model.Fecha_nacimiento;
                        oTabla.Nombre = model.Nombre;

                        db.tabla.Add(oTabla);
                        db.SaveChanges();
                    }

                    return Redirect("~/Tabla/Index");
                }

                return View(model);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int ID)
        {
            TablaViewModel model = new TablaViewModel();
            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.tabla.Find(ID);
                model.Nombre = oTabla.Nombre;
                model.Correo = oTabla.Correo;
                model.Fecha_nacimiento = (DateTime)oTabla.Fecha_nacimiento;
                model.ID = oTabla.ID;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = db.tabla.Find(model.ID);
                        oTabla.Correo = model.Correo;
                        oTabla.Fecha_nacimiento = model.Fecha_nacimiento;
                        oTabla.Nombre = model.Nombre;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("~/Tabla/");
                }

                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        public ActionResult Eliminar(int ID)
        {
            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.tabla.Find(ID);
                db.tabla.Remove(oTabla);
                db.SaveChanges();
               
            }
            return Redirect("~/Tabla/");
        }
    }
}