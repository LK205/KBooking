using API.Db;
using API.Dtos;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API.Controllers
{
    public class HotelRoomController : ApiControllerBase
    {
        private readonly ApplicationDbContext _db;
        private HotelRoomRepo _repo = new HotelRoomRepo();
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
        public async Task<List<HotelRoomDto>> GetAllRoomByHotelId(long HotelId)
        {
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
            var check = _repo.GetAll().FirstOrDefault(r => r.Id == id);
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

        [HttpPost("CreateOrUpdate")]
        public async Task<bool> Create(HotelRoom data)
        {
            HotelRoom obj = _repo.GetByID(data.Id) ?? new HotelRoom();

            obj.HotelId = data.HotelId;
            obj.RoomName = data.RoomName;
            obj.RoomType = data.RoomType;
            obj.BedType = data.BedType;
            obj.RoomImage = data.RoomImage;
            obj.Price = data.Price;
            obj.RoomArea = data.RoomArea;
            obj.Decription = data.Decription;

            if(obj.Id > 0) await _repo.UpdateAsync(obj);
            else await _repo.CreateAsync(obj);

            return true;
        }


        [HttpDelete("Delete")]
        public async Task<bool> Delete(long id)
        {
            _repo.Delete(id);
            return true;
        }
    }
}
