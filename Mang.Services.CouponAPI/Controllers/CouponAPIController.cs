using AutoMapper;
using Mang.Services.CouponAPI.Data;
using Mang.Services.CouponAPI.Models;
using Mang.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mang.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        public CouponAPIController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = new ResponseDto();
        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _dbContext.Coupones.ToList();
                _response.Resulte = _mapper.Map<IEnumerable<CouponDto>>(objList);
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false ;
                _response.Message = ex.Message ;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _dbContext.Coupones.First(f => f.CouponId == id);
                _response.Resulte = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _dbContext.Coupones.First(f => f.CouponCode.ToLower() == code.ToLower());
                _response.Resulte = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto )
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupones.Add(obj);
                _dbContext.SaveChanges();
                _response.Resulte = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupones.Update(obj);
                _dbContext.SaveChanges();
                _response.Resulte = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delet(int id)
        {
            try
            {
                Coupon obj = _dbContext.Coupones.First(f=>f.CouponId == id);
                _dbContext.Coupones.Remove(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
