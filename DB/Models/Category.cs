using System.Collections.Generic;

namespace DB.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public List<Good> Goods { get; set; }
    }
}
