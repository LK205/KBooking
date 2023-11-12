﻿using API.Db;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RoomBillController : ApiControllerBase
    {
        private readonly ApplicationDbContext _db;

        public RoomBillController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllBillByHotelId")]
        public async Task<List<RoomBillDto>> GetBillByHotelId(long HotelId)
        {
            var result = from c in _db.RoomBills
                         join j in _db.Accounts on c.HotelId equals j.Id
                         join d in _db.HotelRooms on c.RoomId equals d.Id
                         where c.HotelId == HotelId
                         select new RoomBillDto()
                         {
                             Id = c.Id,
                             CustomerId = c.CustomerId,
                             HotelId = c.HotelId,

                             HotelName = j.HotelName,
                             Address_City = j.Address_City,
                             Address_District = j.Address_District,
                             RoomId = c.RoomId,

                             RoomName = d.RoomName,
                             RoomType = d.RoomType,
                             RoomImage = d.RoomImage,
                             Price = d.Price,

                             Status = c.Status,
                             PriceTotal = c.PriceTotal,
                             AdditionalServices = c.AdditionalServices,
                             FromBokDate = c.FromBokDate,
                             ToBokDate = c.ToBokDate,
                             TotalDay = (c.FromBokDate.Date.Subtract(c.ToBokDate.Date).Days + 1),
                             CraetionTime = c.CraetionTime,
                         };

            return result.ToList();
        }


        [HttpGet("GetAllBillByCusId")]
        public async Task<List<RoomBillDto>> GetBillByCusId(long HotelId, string Status)
        {
            var result = from c in _db.RoomBills
                         join j in _db.Accounts on c.HotelId equals j.Id
                         join d in _db.HotelRooms on c.RoomId equals d.Id
                         where c.CustomerId == HotelId
                         && (string.IsNullOrEmpty(Status) || c.Status == Status ) 
                         select new RoomBillDto()
                         {
                             Id = c.Id,
                             CustomerId = c.CustomerId,
                             HotelId = c.HotelId,

                             HotelName = j.HotelName,
                             Address_City = j.Address_City,
                             Address_District = j.Address_District,
                             RoomId = c.RoomId,

                             RoomName = d.RoomName,
                             RoomType = d.RoomType,
                             RoomImage = d.RoomImage,
                             Price = d.Price,
                             Status = c.Status,
                             PriceTotal = c.PriceTotal,
                             AdditionalServices = c.AdditionalServices,
                             FromBokDate = c.FromBokDate,
                             ToBokDate = c.ToBokDate,
                             TotalDay = (c.FromBokDate.Date.Subtract(c.ToBokDate.Date).Days + 1),
                             CraetionTime = c.CraetionTime,
                         };

            return result.ToList();
        }


        [HttpPost("Create")]
        public async Task<bool> Create(RoomBillDto dto)
        {
            if (dto.Id > 0)
            {
                throw new Exception("Id was used!");
            }

            RoomBill newRoomBil = new RoomBill()
            {
                CustomerId = dto.CustomerId,
                HotelId = dto.HotelId,
                RoomId = dto.RoomId,
                Status = "Chờ xác nhận",
                PriceTotal = dto.PriceTotal,
                AdditionalServices = dto.AdditionalServices,
                FromBokDate = dto.FromBokDate,
                ToBokDate = dto.ToBokDate,
                CraetionTime = dto.CraetionTime,
            };

            _db.RoomBills.Add(newRoomBil);
            await _db.SaveChangesAsync();
            return true;
        }

        [HttpPut("Confirm")]
        public async Task<bool> ConfirmBill(RoomBillDto dto)
        {
            // 0: Đặt phòng thất bại
            // 1: Đặt phòng thành công
            // 2: Chờ xác nhận

            var check = _db.RoomBills.FirstOrDefault(p => p.Id == dto.Id);
            if(check == null)
            {
                throw new Exception("Id does not exist!");
            }

            check.Status = dto.Status;
            
            _db.RoomBills.Update(check);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}