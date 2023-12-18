using API.Db;
using API.Dto;
using API.Dtos;
using API.Models;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTCWeb.Common;
using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private AccountRepo _repo = new AccountRepo();

        [HttpGet("GetHotel")]
        public async Task<AccountDto> GetHotel(long id)
        {
            var check = _repo.GetAll().FirstOrDefault(p => p.Role == 1 && p.Id == id);
            if (check == null)
            {
                throw new Exception("");
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

        [HttpGet("GetAllHotel")]
        public async Task<object> GetAllHotel(string request = "", int pageSize = 10, int pageNumber = 1)
        {
            List<HotelDto> hotels = SQLHelper<HotelDto>.ProcedureToList("spGetAllHotel",
                new string[] { "@Request", "@PageSize", "@PageNumber" },
                new object[] { request , pageSize , pageNumber });
            List<Total> total = SQLHelper<Total>.ProcedureToList("spGetAllHotelTotal",
                new string[] { "@Request" },
                new object[] { request });

            return new {hotels , total};
        }

        [HttpGet("Login")]
        public async Task<AccountDto> Login(string email, string password)
        {
            var check = _repo.GetAll().FirstOrDefault(p=> p.Email.ToLower() == email.ToLower() && p.Password == HASH.ToSHA256(password));
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
        public async Task<Account> Create(Account dto)
        {
            var check =  _repo.GetAll().FirstOrDefault(p => p.Email.ToLower() == dto.Email.ToLower());
            if (check != null)
            {
                throw new Exception("Email was used!");
            }
            else
            {
                dto.Email = dto.Email.ToLower();
                dto.Password = HASH.ToSHA256(dto.Password);
                await _repo.CreateAsync(dto);

                Account newA =  _repo.GetAll().First(p => p.Email == dto.Email);
                return newA;
            }
        }


        [HttpPut("UpdateAccount")]
        public async Task<Account> UpdateAccount(Account data)
        {
            Account account = _repo.GetByID(data.Id);

            account.MiddleName = data.MiddleName;
            account.LastName = data.LastName;
            account.Gender = data.Gender;
            account.DayOfBirth = data.DayOfBirth;
            account.CityOfResidence = data.CityOfResidence;
            account.ImageBase64 = data.ImageBase64;
            account.IsActive = data.IsActive;
            account.HotelName = data.HotelName;
            account.Address_City = data.Address_City;
            account.Address_District = data.Address_District;
            account.Address_Ward = data.Address_Ward;
            account.Address_Specifically = data.Address_Specifically;
            account.Avatar = data.Avatar;
            account.Website = data.Website;
            account.LocationDescription = data.LocationDescription;
            account.GeneralDescription = data.GeneralDescription;

            await _repo.UpdateAsync(account);
            return _repo.GetByID(account.Id);
        }


        [HttpGet("UpdatePassword")]
        public async Task<bool> UpdatePassword(long id, string newPassword, string oldPassword)
        {
            var check = _repo.GetByID(id);
            if (check.Password != HASH.ToSHA256(oldPassword)) return false;

            check.Password = HASH.ToSHA256(newPassword);
            await _repo.UpdateAsync(check);
            return true;
        }
        [HttpGet("GetById")]
        public async Task<Account> GetByID(long Id)
        {
            return _repo.GetByID(Id);
        }
        [HttpDelete("Delete")]
        public async Task<bool> Delete(long id)
        {
            _repo.Delete(id);
            return true;
        }
    }
}
