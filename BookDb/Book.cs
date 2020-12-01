using System;
using System.Collections.Generic;

namespace BookDb
{
    public partial class Book
    {
        public Book()
        {
            LnkUserBook = new HashSet<LnkUserBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LnkUserBook> LnkUserBook { get; set; }
    }
}
