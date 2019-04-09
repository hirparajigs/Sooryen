using Sooryen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sooryen.Business.Interface
{
    public interface INoteManager
    {
        Task<List<NoteModel>> GetNotes();
        Task<int> Add(NoteModel note);
        Task<bool> Update(NoteModel note);
        Task<bool> Delete(int id);
    }
}
