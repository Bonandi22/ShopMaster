using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IFileService fileService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _fileService = fileService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products.");
                return StatusCode(500, "Internal server error while retrieving products.");
            }
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    return NotFound($"Product with ID {id} not found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID {id}.");
                return StatusCode(500, $"Internal server error while retrieving product with ID {id}.");
            }
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">The product data.</param>
        /// <param name="image">The product image.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProductDto productDto, IFormFile? image)
        {
            if (productDto == null)
            {
                return BadRequest("Product data cannot be null.");
            }

            if (string.IsNullOrEmpty(productDto.Name))
            {
                return BadRequest("Product name is required.");
            }

            if (productDto.Price <= 0)
            {
                return BadRequest("Product price must be greater than zero.");
            }

            if (productDto.CategoryId <= 0)
            {
                return BadRequest("CategoryId must be a valid integer.");
            }

            if (image != null)
            {
                // Validação do tipo de arquivo
                var allowedFileTypes = new[] { "image/jpeg", "image/png" };
                if (!allowedFileTypes.Contains(image.ContentType))
                {
                    return BadRequest("Invalid image file type. Only JPEG and PNG are allowed.");
                }

                // Validação do tamanho do arquivo
                const int maxFileSize = 2 * 1024 * 1024; // 2 MB
                if (image.Length > maxFileSize)
                {
                    return BadRequest("Image file size exceeds the limit (2 MB).");
                }
            }
            try
            {
                // Tratamento da imagem
                if (image != null)
                {
                    var imagePath = await _fileService.UploadFileAsync(image);
                    productDto.ImagePath = imagePath;
                }
                await _productService.AddAsync(productDto);

                return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument when creating product.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error while creating product.");
                return StatusCode(500, $"Internal server error while creating the product: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDto">The updated product data.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("ID mismatch between route and data.");
            }

            try
            {
                var existingProduct = await _productService.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    return NotFound($"Product with ID {id} not found.");
                }

                await _productService.UpdateAsync(productDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Internal server error while updating product with ID {id}.");
                return StatusCode(500, $"Internal server error while updating product with ID {id}.");
            }
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>Result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    return NotFound($"Product with ID {id} not found.");
                }

                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Internal server error while deleting product with ID {id}.");
                return StatusCode(500, $"Internal server error while deleting product with ID {id}.");
            }
        }
    }
}