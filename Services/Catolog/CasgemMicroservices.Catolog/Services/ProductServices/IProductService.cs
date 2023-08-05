using CasgemMicroservices.Catolog.Dtos.CategoryDtos;
using CasgemMicroservices.Catolog.Dtos.ProductDtos;
using CasgemMicroservices.Shared.Dtos;

namespace CasgemMicroservices.Catolog.Services.ProductServices
{
    public interface IProductService
    {
        Task<Response<List<ResultProductDto>>> GetProductAsync();
        Task<Response<ResultProductDto>> GetProductByIdAsync(string id);
        Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createproduct);
        Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateproduct);
        Task<Response<NoContent>> DeleteProductAync(string id);
        Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync();
        
    }
}
