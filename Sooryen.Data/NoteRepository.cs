using Sooryen.Data.Interface;
using Sooryen.Data.Models;
using Sooryen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sooryen.Data
{
    public class NoteRepository : INoteRepository
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork UnitOfWork { get; }

        private IGenericRepository<Note> NoteMasterRepository { get; }

        public NoteRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            NoteMasterRepository = UnitOfWork.GetRepository<Note>();
        }


        public async Task<List<Note>> GetNotes()
        {
            var notes = await NoteMasterRepository.FindAllAsync(x => x.Id > 0);
           
            return notes.ToList();
        }


        public async Task<int> Add(Note Note)
        {
            if (Note == null)
            {
                return 0;
            }

            await NoteMasterRepository.AddEntityAsync(Note);
            return Note.Id;
        }

        public async Task<bool> Update(Note note)
        {
            if (note == null)
            {
                return false;
            }
            var datanote = UnitOfWork.GetRepository<Note>().GetSingle(x => x.Id == note.Id);
            if (datanote != null)
            {
                datanote.Title = note.Title;
                datanote.Body = note.Body;

                datanote.UpdateUser = note.UpdateUser;
                datanote.UpdateDate = DateTime.Now;
                await NoteMasterRepository.UpdateEntityAsync(datanote);
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var note = NoteMasterRepository.GetSingle(x => x.Id == id);
            if (note == null)
            {
                return false;
            }

            await NoteMasterRepository.RemoveEntityAsync(note);
            return true;
        }

    }
}
