using System.Collections.Generic;

namespace DB.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public List<User> Users { get; set; }
    }
}
