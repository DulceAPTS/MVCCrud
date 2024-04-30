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
        // GET: Tabla
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
                }
                return Redirect("Tabla/");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}