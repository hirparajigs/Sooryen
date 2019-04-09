using System;
using System.Collections.Generic;

namespace Sooryen.Data.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<int> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public string UserMaster { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
