using Sooryen.Business.Interface;
using Sooryen.Data.Interface;
using Sooryen.Data.Models;
using Sooryen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sooryen.Business
{
    public class NoteManager: INoteManager
    {

        private INoteRepository NoteRepository { get; }

        public NoteManager(INoteRepository noteRepository)
        {
            NoteRepository = noteRepository;
        }

        public async Task<List<NoteModel>> GetNotes()
        {
          
            var notes = await NoteRepository.GetNotes();
            var noteList = new List<NoteModel>();
            foreach (var item in notes)
            {
                var note = new NoteModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Body = item.Body,
                };
                noteList.Add(note);
            }
           
            return noteList;
        }


         public async Task<int> Add(NoteModel note)
        {
            var notedb = new Note();
            notedb.Id = 0;
            notedb.Title=note.Title;
            notedb.Body = note.Body ;
            notedb.InsertDate = note.InsertDate;
            notedb.InsertUser = note.InsertUser;
           return  await NoteRepository.Add(notedb);

        }

        public async Task<bool> Update(NoteModel note)
        {
            var notedb = new Note();
            notedb.Id = note.Id ;
            notedb.Title = note.Title;
            notedb.Body = note.Body;
            notedb.InsertDate = note.InsertDate;
            notedb.InsertUser = note.InsertUser;
            return await NoteRepository.Update(notedb);
        }

        public async Task<bool> Delete(int id)
        {

            return await NoteRepository.Delete(id);
        }

    }
}
