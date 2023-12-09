using API.Db;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using RTCWeb.Common;

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
        public async Task<object> GetBillByHotelId(long HotelId, string request = "", string Status = "", int pageSize = 10, int pageNumber = 1)
        {
            List<RoomBillDto> roomBill = SQLHelper<RoomBillDto>.ProcedureToList("spGetAllRoomBillHoltel",
                new string[] { "@HotelId", "@Status", "@PageSize", "@PageNumber", "@Request" },
                new object[] { HotelId , Status, pageSize, pageNumber, request });
            List<Total> total = SQLHelper<Total>.ProcedureToList("spGetAllRoomBillHoltelTotal",
                new string[] { "@HotelId", "@Status", "@Request" },
                new object[] { HotelId, Status, request });

            return new { roomBill , total};
        }


        [HttpGet("GetAllBillByCusId")]
        public async Task<object> GetBillByCusId(long CuslId,string request ="", string Status = "", int pageSize = 10, int pageNumber = 1)
        {
            List<RoomBillDto> roomBill = SQLHelper<RoomBillDto>.ProcedureToList("spGetAllRoomBillCus",
                new string[] { "@CusId", "@Status", "@PageSize", "@PageNumber", "@Request" },
                new object[] {CuslId, Status, pageSize, pageNumber, request });
            List<Total> total = SQLHelper<Total>.ProcedureToList("spGetAllRoomBillCusTotal",
                new string[] { "@CusId", "@Status", "@Request" },
                new object[] { CuslId, Status, request });

            return new { roomBill, total };
        }


        [HttpPost("Create")]
        public async Task<bool> Create(RoomBill data)
        {


            data.Status = "Chờ xác nhận";
            data.CraetionTime = DateTime.Now;


            _db.RoomBills.Add(data);
            await _db.SaveChangesAsync();
            return true;
        }

        [HttpPut("Confirm")]
        public async Task<bool> ConfirmBill(RoomBill dto)
        {
            // 0: Đặt phòng thất bại
            // 1: Đặt phòng thành công
            // 2: Chờ xác nhận

            var check = _db.RoomBills.FirstOrDefault(p => p.Id == dto.Id);
            if (check == null)
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
