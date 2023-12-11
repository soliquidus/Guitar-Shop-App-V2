using Microsoft.AspNetCore.Mvc;
using Shop.API.Models.Domain;
using Shop.API.Models.DTO;
using Shop.API.Repositories.Interface;
using Swashbuckle.AspNetCore.Annotations;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(
        IProductRepository productRepository,
        IBrandRepository brandRepository,
        ICategoryRepository categoryRepository) : ControllerBase
    {
        // -- Read requests -- \\
        [HttpGet]
        [SwaggerOperation(Summary = "Fetch all existing products", Description = "Get all the products in database")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllAsync();

            var response = products.Select(ConvertToDtoModel).ToList();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [SwaggerOperation(Summary = "Fetch one product by Id", Description = "Get an existing product by its Id")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null) return NotFound("Product not found");
            
            var response = ConvertToDtoModel(product);

            return Ok(response);

        }

        [HttpGet]
        [Route("{slug}")]
        [SwaggerOperation(Summary = "Fetch one product by Slug", Description = "Get an existing product by its Slug")]
        public async Task<IActionResult> GetProductBySlug(string slug)
        {
            var product = await productRepository.GetBySlugAsync(slug);

            if (product == null) return NotFound("Product not found");
            
            var response = ConvertToDtoModel(product);

            return Ok(response);

        }

        // -- Write requests -- \\
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new product", Description = "Create a new product in database")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var brand = await brandRepository.GetByNameAsync(productDto.BrandName) ??
                        throw new InvalidOperationException("Brand name missing");
            var category = await categoryRepository.GetByNameAsync(productDto.CategoryName) ??
                           throw new InvalidOperationException("Category name missing");

            var product = ConvertToDomainModel(productDto, brand, category);

            product = await productRepository.CreateAsync(product);

            var response = ConvertToDtoModel(product);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [SwaggerOperation(Summary = "Update a product", Description = "Update an existing product in database")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDto productDto)
        {
            productDto.Id = id;
            
            var brand = await brandRepository.GetByNameAsync(productDto.BrandName) ??
                        throw new InvalidOperationException("Brand name missing");
            var category = await categoryRepository.GetByNameAsync(productDto.CategoryName) ??
                           throw new InvalidOperationException("Category name missing");
            
            var product = ConvertToDomainModel(productDto, brand, category);
            var updatedProduct = await productRepository.UpdateAsync(product);

            if (updatedProduct is null) return NotFound();
            
            var response = ConvertToDtoModel(updatedProduct);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [SwaggerOperation(Summary = "Delete a product", Description = "Delete an existing product in database")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deletedProduct = await productRepository.DeleteAsync(id);

            if (deletedProduct is null) return NotFound();

            var response = ConvertToDtoModel(deletedProduct);

            return Ok(response);
        }
        
        // -- Utils -- \\
        private static ProductDto ConvertToDtoModel(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                EntryDate = product.EntryDate,
                Price = product.Price,
                Slug = product.Slug,
                BrandName = product.Brand.BrandName,
                CategoryName = product.Category.CategoryName,
                StockQuantity = product.StockQuantity,
                FeaturedImageUrl = product.FeaturedImageUrl,
                IsInStock = product.IsInStock
            };
        }

        private static Product ConvertToDomainModel(ProductDto productDto, Brand brand, Category category)
        {
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                ShortDescription = productDto.ShortDescription,
                FullDescription = productDto.FullDescription,
                EntryDate = productDto.EntryDate,
                Price = productDto.Price,
                Slug = productDto.Slug,
                StockQuantity = productDto.StockQuantity,
                FeaturedImageUrl = productDto.FeaturedImageUrl,
                IsInStock = productDto.IsInStock,
                Brand = brand,
                Category = category
            };
        }
    }
}