namespace Core.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Naam is verplicht!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Het aangeven van ofdat een product alcohol bevat is verplicht!")]
        public bool? IsAlcoholic { get; set; }
        [Required(ErrorMessage = "Foto van een product is verplicht!")]
        public byte[]? Picture { get; set; }
        public ICollection<Packet>? Packets { get; set; }
    }
}
