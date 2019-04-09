using System;
using System.Collections.Generic;

namespace Sooryen.Data.Models
{
    public partial class User
    {
        public User()
        {
            this.Notes = new List<Note>();
            this.Notes1 = new List<Note>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<int> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public string UserMaster { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Note> Notes1 { get; set; }
    }
}
