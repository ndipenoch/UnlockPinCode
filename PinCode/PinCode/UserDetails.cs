using System;
using System.Collections.Generic;
using System.Text;

namespace PinCode
{
    public class UserDetails
    {
        private string uname;

        public string username { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string street { get; set; }
        public string town { get; set; }
        public string country { get; set; }
        public float bestScore { get; set; }


        public UserDetails(string username, string firstName, string surname, string email, string telephone, string street, string town, string country, float bestScores)
        {
            this.username = username;
            this.firstname = firstName;
            this.surname = surname;
            this.email = email;
            this.telephone = telephone;
            this.street = street;
            this.town = town;
            this.country = country;
            this.bestScore = bestScores;
       
        }

        public UserDetails(string v)
        {
            this.uname = uname;
        }
    }

  
}
