﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.DomainServices.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<List<SelectListItem>> GetAllProductsInSelectListAsync();
        List<string> GetProductsFromPacketInList(Packet packet);
    }
}
