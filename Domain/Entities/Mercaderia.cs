namespace Domain.Entities
{
    public class Mercaderia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TipoMercaderiaId { get; set; }
        public int Price { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public string Image { get; set; }

        public TipoMercaderia TipoMercaderia { get; set; }

    }
}
