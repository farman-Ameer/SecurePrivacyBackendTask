using System;
using System.Collections.Generic;
using System.Text;
using MedicineShopBackEnd.Model.Entities;
namespace MedicineShopBackEnd.Model
{
    public class AuthenticateResponse : User
    {
        //public int userId { get; set; }
        //public string Name { get; set; }
        //public string email { get; set; }
         //public string profilePicture { get; set; }
         
        public string Token { get; set; }

        //public AuthenticateResponse(User user, string token)
        //{
        //    userId = user.userId;
        //    Name = user.userName;
        //    email = user.userEmail;
        //    profilePicture = user.userProfileImage;
        //    Token = token;
        //}

        public AuthenticateResponse(User user, string token)
        {
            userId = user.userId;
            firstName = user.firstName;
            lastName = user.lastName;
            userEmail = user.userEmail;
            contact = user.userPhone;
            userZipCode = user.userZipCode;
            street = user.street;
            state = user.state;
            userAddress = user.userAddress;
            apartment = user.apartment;
            city = user.city;
            roleID = user.roleID;
            userProfileImage = user.userProfileImage;
            Token = token;
        }
    }
}
