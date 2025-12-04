namespace CINLAT.WebApiTest.Models.Business
{
    public class Personaje
    {
        public int id { get; set; }
        public required string name { get; set; }
        public int? age { get; set; }
        public required string description { get; set; }
    }
}
