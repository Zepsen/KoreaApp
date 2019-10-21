namespace Korea.Shared.Models
{
    public class Breadcrumb
    {
        public string Name { get; set; }
        public string Route { get; set; } = "/";
		public bool Active { get; set; } = false;
    }
}