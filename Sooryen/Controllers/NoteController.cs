using Sooryen.Business.Interface;
using Sooryen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sooryen.Controllers
{
    public class NoteController : Controller
    {

        public INoteManager NoteManger { get; }

        public NoteController(INoteManager noteManger)
        {
            NoteManger = noteManger;
        }


        // GET: Note
        public ActionResult Notes()
        {
            return View();
        }

        public async Task<JsonResult> GetAllNotes()
        {
            return Json(await NoteManger.GetNotes(), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddNote(NoteModel item)
        {
            item.Id= await NoteManger.Add(item);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditNote(int id, NoteModel Note)
        {
            Note.Id = id;
            var result = await NoteManger.Update(Note);
            if (result)
            {
                return Json(await NoteManger.GetNotes(), JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        public async Task<JsonResult> DeleteNote(int id)
        {

            return Json(new { Status = await NoteManger.Delete(id) }, JsonRequestBehavior.AllowGet);

        }
    }
}