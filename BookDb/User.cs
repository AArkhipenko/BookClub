using System;
using System.Collections.Generic;

namespace BookDb
{
    public partial class User
    {
        public User()
        {
            LnkUserBook = new HashSet<LnkUserBook>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public virtual ICollection<LnkUserBook> LnkUserBook { get; set; }
    }
}
