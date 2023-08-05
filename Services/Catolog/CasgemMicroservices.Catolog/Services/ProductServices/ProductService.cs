using AutoMapper;
using CasgemMicroservices.Catolog.Dtos.CategoryDtos;
using CasgemMicroservices.Catolog.Dtos.ProductDtos;
using CasgemMicroservices.Catolog.Models;
using CasgemMicroservices.Catolog.Settings.Abstract;
using CasgemMicroservices.Shared.Dtos;
using MongoDB.Driver;

namespace CasgemMicroservices.Catolog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public ProductService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createproduct)
        {
            var values=_mapper.Map<Product>(createproduct);
            await _productCollection.InsertOneAsync(values);
            return Response<CreateProductDto>.Success(_mapper.Map<CreateProductDto>(values), 200);
        }

        public async Task<Response<NoContent>> DeleteProductAync(string id)
        {
            var value = await _productCollection.DeleteOneAsync(id);
            if (value.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Silinecek Ürün Bulunamadı",404);
            }
        }

        public async Task<Response<List<ResultProductDto>>> GetProductAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);

               
        }

        public async Task<Response<ResultProductDto>> GetProductByIdAsync(string id)
        {
            var values = await _productCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync();
            if(values == null)
            {
                return Response<ResultProductDto>.Fail("Böyle Bir Id Bulunamadı", 404);
            }
            else
            {
                return Response<ResultProductDto>.Success(_mapper.Map<ResultProductDto>(values), 200);
            }


        }

        public Task<Response<ResultProductDto>> GetProductByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            if (values.Any())
            {
                foreach (var item in values)
                {
                    item.Category = await _categoryCollection.Find<Category>(x => x.CategoryID == item.CategoryID).FirstOrDefaultAsync();
                }
            }
            else
            {
                values = new List<Product>();

            }
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateproductDto)
        {
            var value = _mapper.Map<Product>(updateproductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateproductDto.ProductID, value);
            if (result == null)
            {
                return Response<UpdateProductDto>.Fail("Güncellenecek Veri Bulunamadı", 404);
            }
            else
            {
                return Response<UpdateProductDto>.Success(204);
            }

        }

       
    }
}
