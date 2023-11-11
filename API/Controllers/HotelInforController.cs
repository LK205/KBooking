using API.Db;
using API.Dto;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class HotelInforController : ApiControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public HotelInforController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public async Task<List<HotelInforDto>> GetAll(string? name, string? local,string? request)
        {
            var result = from c in _dbContext.HotelInfors
                         join j in _dbContext.Accounts on c.Id equals j.Id
                         where (string.IsNullOrEmpty(name) || c.HotelName.Contains(name))
                            && (string.IsNullOrEmpty(request) || c.PhoneNumber.Contains(request) || j.Email.Contains(request))
                            && (string.IsNullOrEmpty(local) || c.Address_City.Contains(local) || c.Address_District.Contains(local)
                                    || c.Address_Ward.Contains(local) || c.Address_Specifically.Contains(local))
                         select new HotelInforDto
                         {
                             Id = c.Id,
                             AccountId = c.AccountId,
                             HotelName = c.HotelName,
                             PhoneNumber = c.PhoneNumber,
                             Address_City = c.Address_City,
                             Address_District = c.Address_District,
                             Address_Specifically = c.Address_Specifically,
                             Address_Ward = c.Address_Ward,
                             Avatar = c.Avatar,
                             Website = c.Website,
                             LocationDescription = c.LocationDescription,
                             GeneralDescription = c.GeneralDescription,
                             Role = j.Role,
                             Email = j.Email,
                         };
            return result.ToList();

        }
        [HttpGet("GetById")]
        public async Task<HotelInforDto> GetById(long id)
        {
            var check = await _dbContext.HotelInfors.FirstOrDefaultAsync(p=> p.AccountId == id);
            if (check == null)
            {
                throw new Exception("Id does not exist!");
            }
            else
            {
                var result = new HotelInforDto()
                {
                    Id = check.Id,
                    AccountId = check.AccountId,
                    HotelName = check.HotelName,
                    PhoneNumber = check.PhoneNumber,
                    Address_City = check.Address_City,
                    Address_District = check.Address_District,
                    Address_Ward = check.Address_Ward,
                    Address_Specifically = check.Address_Specifically,
                    Avatar = check.Avatar,
                    Website = check.Website,
                    LocationDescription = check.LocationDescription,
                    GeneralDescription = check.GeneralDescription,
                };
                return result;
            }
        }

        [HttpPost("Create")]
        public async Task Create(HotelInforDto dto)
        {
            var check =  _dbContext.HotelInfors.FirstOrDefault(p=> p.Id == dto.Id);
            if(check != null)
            {
                throw new Exception("Id was exist!");
            }
            else
            {
                var result = new HotelInfor()
                {
                    HotelName = check.HotelName,
                    AccountId = check.AccountId,
                    PhoneNumber = check.PhoneNumber,
                    Address_City = check.Address_City,
                    Address_District = check.Address_District,
                    Address_Ward = check.Address_Ward,
                    Address_Specifically = check.Address_Specifically,
                    Avatar = check.Avatar,
                    Website = check.Website,
                    LocationDescription = check.LocationDescription,
                    GeneralDescription = check.GeneralDescription,
                };
                await _dbContext.HotelInfors.AddAsync(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpPut("Edit")]
        public async Task Edit(HotelInforDto dto)
        {
            var check = _dbContext.HotelInfors.FirstOrDefault(p => p.Id == dto.Id);
            if (check == null)
            {
                throw new Exception("Id does not exist!");
            }
            else
            {
                check.HotelName = dto.HotelName;
                check.PhoneNumber = dto.PhoneNumber;
                check.Address_City = dto.Address_City;
                check.Address_District = dto.Address_District;
                check.Address_Ward = dto.Address_Ward;
                check.Address_Specifically = dto.Address_Specifically;
                check.Avatar = dto.Avatar;
                check.Website = dto.Website;
                check.LocationDescription = dto.LocationDescription;
                check.GeneralDescription = dto.GeneralDescription;



                _dbContext.HotelInfors.Update(check);
                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpDelete("Delete")]
        public async Task Delete(long id)
        {
            _dbContext.Remove(id);
        }
    }
}
