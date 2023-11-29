using API.Db;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API.Controllers
{
    public class HotelRoomController : ApiControllerBase
    {
        private readonly ApplicationDbContext _db;

        public HotelRoomController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllRoom")]
        public async Task<List<HotelRoomDto>> GetAll(string? type, string? local, long pricefrom = 0, long priceTo = 100000000)
        {
            var result = from c in _db.HotelRooms
                         join j in _db.Accounts on c.HotelId equals j.Id
                         where (string.IsNullOrEmpty(local) || j.Address_City.Contains(local) || j.Address_District.Contains(local))
                           && ( string.IsNullOrEmpty(type)  || c.RoomType.Contains(type))
                           && (c.Price >= pricefrom && c.Price <= priceTo)
                         select new HotelRoomDto()
                         {
                             Id = c.Id,

                             HotelId = c.HotelId,
                             HotelName = j.HotelName,
                             Address_City = j.Address_City,
                             Address_District = j.Address_District,

                             BedType = c.BedType,
                             RoomName = c.RoomName,
                             RoomType = c.RoomType,
                             RoomImage = c.RoomImage,
                             Price = c.Price,
                             RoomArea = c.RoomArea,
                             Decription = c.Decription,
                         };

            return result.ToList();
        }


        [HttpGet("GetAllRoomByHotelID")]
        public async Task<List<HotelRoomDto>> GetRoomByHotelId(long HotelId)
        {
            var check = await _db.Accounts.FirstOrDefaultAsync(p => p.Id == HotelId);
            if (check != null)
            {
                if (check.HotelName == null) throw new Exception("HotelId does not exist!");

            }
            else throw new Exception("HotelId does not exist!");


            var result = from c in _db.HotelRooms
                         join j in _db.Accounts on c.HotelId equals j.Id
                         where c.HotelId == HotelId
                         select new HotelRoomDto()
                         {
                             Id = c.Id,

                             HotelId = c.HotelId,
                             HotelName = j.HotelName,
                             Address_City = j.Address_City,
                             Address_District = j.Address_District,

                             BedType = c.BedType,
                             RoomName = c.RoomName,
                             RoomType = c.RoomType,
                             RoomImage = c.RoomImage,
                             Price = c.Price,
                             RoomArea = c.RoomArea,
                             Decription = c.Decription,
                         };

            return result.ToList();
        }

        [HttpGet("GetRoom")]
        public async Task<HotelRoomDto> GetRoom(long id)
        {
            var check = await _db.HotelRooms.FirstOrDefaultAsync(r => r.Id == id);
            if(check == null)
            {
                throw new Exception("Id does not exist!");
            }

            HotelRoomDto result = new HotelRoomDto()
                         {
                            Id = check.Id,
                            HotelId = check.HotelId,
                            BedType = check.BedType,
                            RoomName = check.RoomName,
                            RoomType = check.RoomType,
                            RoomImage = check.RoomImage,
                            Price = check.Price,
                            RoomArea = check.RoomArea,
                            Decription = check.Decription,
                        };

            return result;
        }

        [HttpPost("Create")]
        public async Task<bool> Create(HotelRoomDto dto)
        {

            if (dto.Id > 0 )
            {
                throw new Exception("Id was used!");
            }

            HotelRoom newHoRoom = new HotelRoom()
            {
                HotelId = dto.HotelId,
                RoomName = dto.RoomName,
                RoomType = dto.RoomType,
                BedType = dto.BedType,
                RoomImage = dto.RoomImage,
                Price = dto.Price,
                RoomArea = dto.RoomArea,
                Decription = dto.Decription
            };

            await _db.HotelRooms.AddAsync(newHoRoom);
            _db.SaveChanges();
            return true;
        }


        [HttpPut("Update")]
        public async Task<bool> Update(HotelRoomDto dto)
        {
            var checkHotelId = await _db.Accounts.FirstOrDefaultAsync(p => p.Id == dto.HotelId);
            if (checkHotelId != null)
            {
                if (checkHotelId.HotelName == null) throw new Exception("HotelId does not exist!");

            }
            else throw new Exception("HotelId does not exist!");


            var check = _db.HotelRooms.FirstOrDefault(p => p.Id == dto.Id);
            if (check == null)
            {
                throw new Exception("Id does not exist!");
            }

            check.HotelId = dto.HotelId;
            check.RoomName = dto.RoomName;
            check.RoomType = dto.RoomType;
            check.BedType = dto.BedType;
            check.RoomImage = dto.RoomImage;
            check.Price = dto.Price;
            check.RoomArea = dto.RoomArea;
            check.Decription = dto.Decription;

            _db.HotelRooms.Update(check);
            _db.SaveChanges();
            return true;
        }


        [HttpDelete("Delete")]
        public async Task<bool> Delete(long id)
        {
            var check = await _db.HotelRooms.FirstAsync(p => p.Id == id);
            if (check != null)
            {
                _db.HotelRooms.Remove(check);
                _db.SaveChanges();
                return true;
            }

            throw new Exception("Delete failed!");
        }
    }
}
