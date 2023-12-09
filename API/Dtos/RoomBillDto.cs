namespace API.Dtos
{
    public class RoomBillDto
    {
        public long Id { get; set; }


        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public long HotelId { get; set; }
        public string HotelName { get; set; }
        public string Address_City { get; set; }
        public string Address_District { get; set; }
        public string HotelPhone { get; set; }

        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public string RoomImage { get; set; }
        public long Price { get; set; }



        public string Status { get; set; }
        public long PriceTotal { get; set; }
        public string AdditionalServices { get; set; }


        public DateTime FromBokDate { get; set; }
        public DateTime ToBokDate { get; set; }
        public long TotalDay { get; set; }


        public DateTime CraetionTime { get; set; }
    }
}
