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
        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();
        }

        /// <summary>
        ///  Metodo Post Nuevo, que se encarga de dar de alta un usuario en la Base de datos
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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