namespace Portal.Extensions
{
    public static class ProductExtensions
    {
        public static string GetIsAlcoholic(this Product product) => ((bool)product.IsAlcoholic!) ? "bi bi-check-square-fill icon blue-text" : "bi bi-x-square-fill icon blue-text";
        public static string GetPicture(this Product product) => @String.Format("data:image/png;base64,{0}", Convert.ToBase64String(product.Picture!));
    }
}
