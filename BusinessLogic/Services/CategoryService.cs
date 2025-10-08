using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using System.Net;

namespace BusinessLogic.Services
{
    public class CategoryService : ICategoriesService
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public CategoryService(ShopDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public async Task<IList<CategoryDto>> Get(int pageNumber = 1)
        {
            var items = await PaginatedList<Category>.CreateAsync(ctx.Categories, pageNumber, 5);

            return mapper.Map<IList<CategoryDto>>(items);
        }

        public async Task<CategoryDto> Create(CategoryDto model)
        {
            var entity = mapper.Map<Category>(model);
            ctx.Categories.Add(entity);
            await ctx.SaveChangesAsync();

            return mapper.Map<CategoryDto>(entity);
        }

        public async Task Delete(int id)
        {
            var item = await ctx.Categories.FindAsync(id);

            ctx.Categories.Remove(item);
            await ctx.SaveChangesAsync();
        }

        public async Task Edit(CategoryDto model)
        {
            var existing = await GetEntityById(model.Id);
            mapper.Map(model, existing);
            await ctx.SaveChangesAsync();
        }


        public async Task<CategoryDto> GetById(int id)
        {
            var item = await GetEntityById(id);

            return mapper.Map<CategoryDto>(item);
        }

        private async Task<Category> GetEntityById(int id)
        {
            if (id < 0)
                throw new HttpException("Id can not be negative.", HttpStatusCode.BadRequest);
            var item = await ctx.Categories.FindAsync(id);
            if (item == null)
                throw new HttpException($"Category with id:{id} not found.", HttpStatusCode.NotFound);
            return item;


        }
    }
}
