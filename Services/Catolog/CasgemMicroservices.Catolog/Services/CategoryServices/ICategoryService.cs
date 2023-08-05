using CasgemMicroservices.Catolog.Dtos.CategoryDtos;
using CasgemMicroservices.Shared.Dtos;


namespace CasgemMicroservices.Catolog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Response<List<ResultCategoryDto>>> GetCategoryListAsync();
        Task<Response<ResultCategoryDto>> GetCategoryByIdAsync(string id);
        Task<Response<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategory);
        Task<Response<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto updateCategory);
        Task<Response<NoContent>> DeleteCategoryAsync(string id);

    }
}