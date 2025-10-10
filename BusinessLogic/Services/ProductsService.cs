using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public ProductsService(ShopDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public ProductDto Create(CreateProductDto model)
        {
            var entity = mapper.Map<Product>(model);

            ctx.Products.Add(entity);
            ctx.SaveChanges();

            return mapper.Map<ProductDto>(entity);
        }

        public void Delete(int id)
        {
            if (id < 0) return;

            var item = ctx.Products.Find(id);

            if (item == null) return;

            ctx.Products.Remove(item);
            ctx.SaveChanges(true);
        }

        public void Edit(EditProductDto model)
        {
            ctx.Products.Update(mapper.Map<Product>(model));
            ctx.SaveChanges();
        }

        public ProductDto? Get(int id)
        {
            if (id < 0)
                return null;

            var item = ctx.Products.Find(id);

            if (item == null)
                return null;

            return mapper.Map<ProductDto>(item);
        }

        public IList<ProductDto> GetAll(int? filterCategoryId, string? searchTitle)
        {
            IQueryable<Product> query = ctx.Products
                .Include(x => x.Category);

            if(filterCategoryId != null)
                query = query.Where(x => x.CategoryId == filterCategoryId);

            if (searchTitle != null)
                query = query.Where(x => x.Title.ToLower().Contains(searchTitle.ToLower()));

            return mapper.Map<IList<ProductDto>>(query.ToList());
        }
    }
}
