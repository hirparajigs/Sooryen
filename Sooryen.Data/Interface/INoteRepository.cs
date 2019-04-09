using Sooryen.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sooryen.Data.Interface
{
    public interface INoteRepository
    {
        Task<List<Note>> GetNotes();
        Task<int> Add(Note Note);
        Task<bool> Update(Note note);
        Task<bool> Delete(int id);
    }
}
