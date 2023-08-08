using CasgemMicroservices.Discount.Dtos;
using CasgemMicroservices.Shared.Dtos;
using Dapper;
using Npgsql;
using System.Data;

namespace CasgemMicroservices.Discount.Services
{
   
    public class CouponService : ICouponService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public CouponService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> CreateCoupon(CreateCouponDto createCouponDto)
        {
            var values = await _dbConnection.ExecuteAsync("Insert Into Coupon (Rate,Code,CreatedTime) VALUES (@Rate,@Code,@CreatedTime)", createCouponDto);
            if (values > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Ekleme Sırasında Hata Oluştu", 500);

        }

        public async Task<Response<NoContent>> DeleteCoupon(int id)
        {
            var values = await _dbConnection.ExecuteAsync("delete from coupon where CouponID=@CouponID", new { CouponID = id });
            return values > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Kupon Bulunamadı", 404);
        }

        public async Task<Response<ResultCouponDto>> GetById(int id)
        {
            var values = (await _dbConnection.QueryAsync<ResultCouponDto>("select * from Coupon where CouponID=@CouponID")).FirstOrDefault();
            var paremeters = new DynamicParameters();
            paremeters.Add("@CouponID", id);
            if (values == null)
            {
                return Response<ResultCouponDto>.Fail("Kupon Bulunamadı", 404);

            }
            return Response<ResultCouponDto>.Success(values, 200);

        }

        public async Task<Response<List<ResultCouponDto>>> GetCouponList()
        {
            var values = await _dbConnection.QueryAsync<ResultCouponDto>("Select *From Coupon");
            return Response<List<ResultCouponDto>>.Success(values.ToList(), 200);

        }

        public async Task<Response<NoContent>> UpdateCoupon(UpdateCouponDto updateCouponDto)
        {
            var values = await _dbConnection.ExecuteAsync("Update Coupon set =Code=@Code,Rate=@Rate");
            var parameters = new DynamicParameters();
            parameters.Add("@Rate", updateCouponDto.Rate);
            parameters.Add("@Code", updateCouponDto.Code);
            parameters.Add("@CouponID", updateCouponDto.CouponID);
            if (values > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Kupon Bulunamadı", 404);

        }
    }
}
