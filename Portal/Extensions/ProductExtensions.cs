namespace Portal.Extensions
{
    public static class ProductExtensions
    {
        public static string GetIsAlcoholic(this Product product) => ((bool)product.IsAlcoholic!) ? "Ja" : "Nee";
        public static string GetPicture(this Product product) => @String.Format("data:image/png;base64,{0}", Convert.ToBase64String(product.Picture!));
    }
}
