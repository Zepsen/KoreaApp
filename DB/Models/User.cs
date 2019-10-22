namespace DB.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
