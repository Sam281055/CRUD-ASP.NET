namespace PandStore_Back.Model
{
    public class Producto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? picture { get; set; }
        public int price { get; set; }
        public int amount { get; set; }
    }
}
