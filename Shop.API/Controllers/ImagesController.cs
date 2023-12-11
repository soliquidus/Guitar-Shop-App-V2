using Microsoft.AspNetCore.Mvc;
using Shop.API.Models.Domain;
using Shop.API.Repositories.Interface;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IImageRepository imageRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await imageRepository.GetAllAsync();

            return Ok(images);
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string brand, string category, string modelName)
        {
            ValidateFileUpload(file);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var productImage = new ProductImage
            {
                FileName = $"{category.ToLower()}-{brand.ToLower()}{modelName.Replace(brand, string.Empty).ToLower()}"
                    .Replace(" ", "-"),
                FileExtension = Path.GetExtension(file.FileName).ToLower(),
                CreationDate = DateTime.Now
            };

            productImage = await imageRepository.UploadAsync(file, productImage);

            return Ok(productImage);

        }
        
        
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] {".jpg", ".jpeg", ".png"};

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }
    }
}
