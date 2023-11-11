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


        [HttpGet("Login")]
        public async Task<AccountDto> Login(string email, string password)
        {
            var Password = HASH.ToSHA256(password);
            var check = _dbContext.Accounts.FirstOrDefault(p=> p.Email == email && p.Password == Password);
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
                    Password = check.Password,
                    CreationTime = check.CreationTime,
                    Role = check.Role,
                };
                return result;
            }
        }


        [HttpPost("Register")]
        public async Task<long> Create(string email, string password)
        {
            var check = await _dbContext.Accounts.FirstOrDefaultAsync(p => p.Email == email);
            if (check != null)
            {
                throw new Exception("Email was used!");
            }
            else
            {
                var newAccount = new Account()
                {
                    Email = email,
                    Password = HASH.ToSHA256(password),
                    CreationTime = DateTime.Now,
                    Role = 0,
                };
                _dbContext.Accounts.Add(newAccount);
                await _dbContext.SaveChangesAsync();


                var result = await _dbContext.Accounts.FirstAsync(p => p.Email == email);
                return result.Id;
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
        public async Task Delete(long id)
        {
            _dbContext.Remove(id);
        }
    }
}
