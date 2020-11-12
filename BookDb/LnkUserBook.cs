using System;
using System.Collections.Generic;

namespace BookDb
{
    public partial class LnkUserBook
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
