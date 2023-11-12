namespace API.Dtos
{
    public class HotelRoomDto
    {
        public long Id { get; set; }

        public long HotelId { get; set; }
        public string HotelName { get; set; }
        public string Address_City { get; set; }
        public string Address_District { get; set; }


        public string BedType { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public string RoomImage { get; set; }
        public long Price { get; set; }
        public string RoomArea { get; set; }
        public string Decription { get; set; }
    }
}
