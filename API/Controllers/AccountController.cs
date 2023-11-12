using API.Db;
using API.Dto;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;

namespace API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetHotel")]
        public async Task<AccountDto> GetHotel(long id)
        {
            var check = _dbContext.Accounts.FirstOrDefault(p => p.Role == 1 && p.Id == id);
            if (check == null)
            {
                throw new Exception("Email or Password is not valid!");
            }
            else
            {
                var result = new AccountDto()
                {
                    Id = check.Id,
                    Email = check.Email,
                    PhoneNumber = check.PhoneNumber,
                    Password = check.Password,
                    CreationTime = check.CreationTime,
                    Role = check.Role,

                    MiddleName = check.MiddleName,
                    LastName = check.LastName,
                    Gender = check.Gender,
                    DayOfBirth = check.DayOfBirth,
                    CityOfResidence = check.CityOfResidence,
                    ImageBase64 = check.ImageBase64,
                    IsActive = check.IsActive,

                    HotelName = check.HotelName,
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

        [HttpGet("Login")]
        public async Task<AccountDto> Login(string email, string password)
        {
            var check = _dbContext.Accounts.FirstOrDefault(p=> p.Email == email && p.Password == HASH.ToSHA256(password));
            if(check == null)
            {
                throw new Exception("Email or Password is not valid!");
            }
            else
            {
                var result = new AccountDto()
                {
                    Id = check.Id,
                    Email = check.Email,
                    PhoneNumber = check.PhoneNumber,
                    Password = check.Password,
                    CreationTime = check.CreationTime,
                    Role = check.Role,

                    MiddleName = check.MiddleName,
                    LastName = check.LastName,
                    Gender = check.Gender,
                    DayOfBirth = check.DayOfBirth,
                    CityOfResidence = check.CityOfResidence,
                    ImageBase64 = check.ImageBase64,
                    IsActive = check.IsActive,

                    HotelName = check.HotelName,
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


        [HttpPost("Register")]
        public async Task<Account> Create(AccountDto dto)
        {
            var check = await _dbContext.Accounts.FirstOrDefaultAsync(p => p.Email == dto.Email);
            if (check != null)
            {
                throw new Exception("Email was used!");
            }
            else
            {
                var newAccount = new Account()
                {
                    Id = dto.Id,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Password = HASH.ToSHA256(dto.Password),
                    CreationTime = dto.CreationTime,
                    Role = dto.Role,

                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    Gender = dto.Gender,
                    DayOfBirth = dto.DayOfBirth,
                    CityOfResidence = dto.CityOfResidence,
                    ImageBase64 = dto.ImageBase64,
                    IsActive = dto.IsActive,

                    HotelName = dto.HotelName,
                    Address_City = dto.Address_City,
                    Address_District = dto.Address_District,
                    Address_Ward = dto.Address_Ward,
                    Address_Specifically = dto.Address_Specifically,
                    Avatar = dto.Avatar,
                    Website = dto.Website,
                    LocationDescription = dto.LocationDescription,
                    GeneralDescription = dto.GeneralDescription,
                };
                _dbContext.Accounts.Add(newAccount);
                await _dbContext.SaveChangesAsync();



                Account newA = await _dbContext.Accounts.FirstAsync(p => p.Email == dto.Email);
                return newA;
            }
        }

        [HttpPut("Edit")]
        public async Task Edit(long id, string password)
        {
            var check = await _dbContext.Accounts.FirstOrDefaultAsync(p => p.Id == id);
            if (check == null)
            {
                throw new Exception("Id does not exist!");
            }
            else
            {
                check.Password = HASH.ToSHA256(password);
                _dbContext.Update(check);
                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpDelete("Delete")]
        public async Task<bool> Delete(long id)
        {
            var check = await _dbContext.Accounts.FirstAsync(p => p.Id == id);
            if (check != null)
            {
                _dbContext.Accounts.Remove(check);
                _dbContext.SaveChanges();
                return true;
            }

            throw new Exception("Delete failed!");
        }
    }
}
