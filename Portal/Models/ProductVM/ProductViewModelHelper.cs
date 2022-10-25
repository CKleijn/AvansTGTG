namespace Portal.Models.ProductVM
{
    public static class ProductViewModelHelper
    {
        public static List<ProductViewModel> FormatProduct(this ICollection<Product> products)
        {
            var productList = new List<ProductViewModel>();

            foreach (var product in products!)
            {
                var productModel = new ProductViewModel()
                {
                    Name = product.Name,
                    IsAlcoholic = product.GetIsAlcoholic(),
                    Picture = product.GetPicture()
                };

                productList.Add(productModel);
            }

            return productList;
        }
    }
}
