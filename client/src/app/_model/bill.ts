export interface Bill {
    id : number,
    customerId : number,
    name: string,
    phoneNumber: string,
    hotelId : number,
    roomId : number,
    status : string,
    priceTotal : number,
    additionalServices : string,
    fromBokDate : Date,
    toBokDate : Date,
    craetionTime : Date,
}