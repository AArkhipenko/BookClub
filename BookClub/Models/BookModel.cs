using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Models
{
    public class BookModel
    {
        public int? lnkId { get; set; }
        public int? bookId { get; set; }
        public string bookName { get; set; }
    }
}
