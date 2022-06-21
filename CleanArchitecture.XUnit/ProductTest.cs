using Application.Exceptions;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetProductById;
using Application.Interfaces.Repositories;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Products.Queries.GetProductById.GetProductByIdQuery;

namespace CleanArchitecture.XUnit
{
    public class ProductTest
    {

        public Mock<IProductRepositoryAsync> mock = new Mock<IProductRepositoryAsync>();

        private readonly IMapper _mapper;



        public ProductTest()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<GeneralProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async void CreateProduct()
        { 
            

            CreateProductCommand cmd = new CreateProductCommand()
            {
                Name = "Produc1",
                Price = 12,
                Type = "Book",
            };
            CreateProductCommandHandler handler = new CreateProductCommandHandler(mock.Object,_mapper);
            var result = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(result.Succeeded);

        }


       

        [Fact]
        public async void GetProductbyId()
        {
            mock.Setup(p => p.GetByIdAsync(1)).ReturnsAsync(new Product() {
                Id = 1,
                Name = "Product1",
                Price = 12,
                Type = "Book",
                Active =true
            });

            GetProductByIdQuery cmd = new GetProductByIdQuery() { Id  = 1};
            GetProductByIdQueryHandler handler = new GetProductByIdQueryHandler(mock.Object);
           var result = await handler.Handle(cmd,CancellationToken.None);

            Assert.True(result.Data != null);
            Assert.Equal("Product1", result.Data.Name);


        }

        [Fact]
        public async void GetProductbyId_Notfound()
        {
            mock.Setup(p => p.GetByIdAsync(1)).ReturnsAsync(new Product()
            {
                Id = 1,
                Name = "Product1",
                Price = 12,
                Type = "Book",
                Active = true
            });

            GetProductByIdQuery cmd = new GetProductByIdQuery() { Id = 3 };
            GetProductByIdQueryHandler handler = new GetProductByIdQueryHandler(mock.Object);

            async Task action()
            {
                var result = await handler.Handle(cmd, CancellationToken.None);
            }

            var exception = await Assert.ThrowsAsync<ApiException>(action);
            Assert.Equal("Product Not Found.", exception.Message);
        }

       
    }
}
