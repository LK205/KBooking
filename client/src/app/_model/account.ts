export interface Account {
    id: number,
    email: string,
    phoneNumber: string,
    password: string,
    creationTime: Date,
    role: number,



//cus 
    middleName: string,
    lastName: string,
    gender: number,
    dayOfBirth: Date,
    cityOfResidence: string,
    imageBase64: string,
    isActive: boolean,




//hotel
    hotelName: string,
    address_City: string,
    address_District: string,
    address_Ward: string,
    address_Specifically: string,
    avatar: string,
    website: string,
    locationDescription: string,
    generalDescription: string,
}