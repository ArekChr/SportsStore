using System.Collections.Generic;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDdContext context = new EFDdContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}
