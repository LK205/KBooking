using API.Db;
using API.Dto;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CustomerInforController : ApiControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerInforController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("GetAll")]
        public async Task<List<CustomerInforDto>> GetAll(string? request, int? gender, string? city, bool? isActive, long? role)
        {
            var result = from c in _dbContext.CustomerInfors
                         join j in _dbContext.Accounts on c.AccountId equals j.Id
                         where (string.IsNullOrEmpty(request) || c.CityOfResidence.Contains(request))
                            && (string.IsNullOrEmpty(city) || c.CityOfResidence.Contains(city))
                            && (isActive == null || c.IsActive == isActive)
                            && (gender == null || c.Gender == gender)
                            && (role == null || j.Role == role)
                         select new CustomerInforDto()
                         {
                             Id = c.Id,
                             AccountId = c.AccountId,
                             MiddleName = c.MiddleName,
                             LastName = c.LastName,
                             Gender = c.Gender,
                             DayOfBirth = c.DayOfBirth,
                             PhoneNumber = c.PhoneNumber,
                             CityOfResidence = c.CityOfResidence,
                             ImageBase64 = c.ImageBase64,
                             IsActive = c.IsActive,
                             Role = j.Role,
                             Email = j.Email
                         };
            return result.ToList();
        }

        [HttpGet("GetById")]
        public async Task<CustomerInforDto> GetById(long id)
        {
            var check = _dbContext.CustomerInfors.FirstOrDefault(p=> p.AccountId == id);
            if(check == null)
            {
                throw new Exception("Id does not exist!");
            }
            else
            {
                var result = new CustomerInforDto()
                {
                    Id = check.Id,
                    AccountId = check.AccountId,
                    MiddleName = check.MiddleName,
                    LastName = check.LastName,
                    Gender = check.Gender,
                    DayOfBirth = check.DayOfBirth,
                    PhoneNumber = check.PhoneNumber,
                    CityOfResidence = check.CityOfResidence,
                    ImageBase64 = check.ImageBase64,
                    IsActive = check.IsActive
                };
                return  result;

            }
        }

        [HttpPost("Create")]
        public async Task Create(CustomerInforDto dto)
        {
            var check = await _dbContext.CustomerInfors.FirstOrDefaultAsync(p => p.Id == dto.Id);
            if(check != null)
            {
                throw new Exception("Id does exist!");
            }
            else
            {
                var newCusInfor = new CustomerInfor();

                newCusInfor.Id = dto.Id;
                newCusInfor.AccountId = dto.AccountId;
                newCusInfor.MiddleName = dto.MiddleName;
                newCusInfor.LastName = dto.LastName;
                newCusInfor.Gender = dto.Gender;
                newCusInfor.DayOfBirth = dto.DayOfBirth;
                newCusInfor.PhoneNumber = dto.PhoneNumber;
                newCusInfor.CityOfResidence = dto.CityOfResidence;
                newCusInfor.ImageBase64 = dto.ImageBase64;
                newCusInfor.IsActive = true;

                await _dbContext.AddAsync(newCusInfor);
                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpPut("Edit")] 
        public async Task Edit(CustomerInforDto dto)
        {
            var check = await _dbContext.CustomerInfors.FirstOrDefaultAsync(p => p.Id == dto.Id);
            if(check == null)
            {
                throw new Exception("Id does not exist!");
            }
            else
            {
                check.Id = dto.Id;
                check.AccountId = dto.AccountId;
                check.MiddleName = dto.MiddleName;
                check.LastName = dto.LastName;
                check.Gender = dto.Gender;
                check.DayOfBirth = dto.DayOfBirth;
                check.PhoneNumber = dto.PhoneNumber;
                check.CityOfResidence = dto.CityOfResidence;
                check.ImageBase64 = dto.ImageBase64;
                check.IsActive = dto.IsActive;

                _dbContext.Update(check);
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
