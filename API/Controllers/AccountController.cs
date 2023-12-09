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
            var check = _repo.GetAll().FirstOrDefault(p=> p.Email == email && p.Password == HASH.ToSHA256(password));
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
            var check =  _repo.GetAll().FirstOrDefault(p => p.Email == dto.Email);
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

                    MiddleName = dto.MiddleName ?? "",
                    LastName = dto.LastName ?? "",
                    Gender = dto.Gender,
                    DayOfBirth = dto.DayOfBirth,
                    CityOfResidence = dto.CityOfResidence ?? "",
                    ImageBase64 = dto.ImageBase64 ?? "",
                    IsActive = dto.IsActive,

                    HotelName = dto.HotelName ?? "",
                    Address_City = dto.Address_City ?? "",
                    Address_District = dto.Address_District ?? "",
                    Address_Ward = dto.Address_Ward ?? "",
                    Address_Specifically = dto.Address_Specifically ?? "",
                    Avatar = dto.Avatar ?? "",
                    Website = dto.Website ?? "",
                    LocationDescription = dto.LocationDescription ?? "",
                    GeneralDescription = dto.GeneralDescription ?? "",
                };
                await _repo.CreateAsync(newAccount);



                Account newA =  _repo.GetAll().First(p => p.Email == dto.Email);
                return newA;
            }
        }


        [HttpPut("UpdateAccount")]
        public async Task UpdateAccount(AccountDto data)
        {
            Account account = _repo.GetByID(data.Id);


            //Cus
            account.MiddleName = data.MiddleName;
            account.LastName = data.LastName;
            account.Gender = data.Gender;
            account.DayOfBirth = data.DayOfBirth;
            account.CityOfResidence = data.CityOfResidence;
            account.ImageBase64 = data.ImageBase64;
            account.IsActive = data.IsActive;

            //Hotel
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
        }


        [HttpPut("UpdatePassword")]
        public async Task UpdatePassword(long id, string password)
        {
            var check =  _repo.GetAll().FirstOrDefault(p => p.Id == id);
            if (check == null)
            {
                throw new Exception("Id does not exist!");
            }
            else
            {
                check.Password = HASH.ToSHA256(password);
                await _repo.UpdateAsync(check);
            }
        }

        [HttpDelete("Delete")]
        public async Task<bool> Delete(long id)
        {
            var check = _repo.GetAll().FirstOrDefault(p => p.Id == id);
            if (check != null)
            {
                _repo.Delete(id);
                return true;
            }

            throw new Exception("Delete failed!");
        }
    }
}
