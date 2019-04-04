using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Data.Sqlite;
using System.Text;
using Xamarin.Forms;
using System.Diagnostics;

namespace PinCode
{
    public class FirebaseDAO
    {
        /// <summary>
        /// Firebase authetication parameters
        /// </summary>
        private const String databaseUrl = "https://unlockpincode-d448d.firebaseio.com/";
        private const String databaseSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey";
        private FirebaseClient firebase;

        /// <summary>
        /// Establish connection with Firebase
        /// </summary>
        public void ConnectToFirebase()
        {
            this.firebase = new FirebaseClient(

                 databaseUrl,
                 new FirebaseOptions
                 {
                     AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                 });
        }


        /// <summary>
        /// Add userdetails to Firebase
        /// </summary>
        /// <param name="username"></param>
        /// <param name="firstname"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>
        /// <param name="street"></param>
        /// <param name="town"></param>
        /// <param name="country"></param>
        /// <param name="bestSquareScore"></param>
        /// <param name="bestRollutteScore"></param>
        public async void AddUserDetailsFB(string username, string firstname, string surname, string email, string telephone, string street, string town, string country, float bestSquareScore, float bestRollutteScore)
        {
            ConnectToFirebase();

            String node = username + "Details" + "/";

             UserDetailsData userDetails = new UserDetailsData
              {
                  username= username,
                  firstname= firstname,
                  surname= surname,
                  email= email,
                  telephone= telephone,
                  street= street,
                  town= town,
                  country= country,
                  bestSquareScore= bestSquareScore,
                  bestRollutteScore= bestRollutteScore

              };

              await firebase.Child(node).PostAsync<UserDetailsData>(userDetails);
   
        }

        /// <summary>
        /// Update the userdetails in Firebase
        /// </summary>
        /// <param name="username"></param>
        /// <param name="firstname"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>
        /// <param name="street"></param>
        /// <param name="town"></param>
        /// <param name="country"></param>
        /// <param name="bestSquareScore"></param>
        /// <param name="bestRollutteScore"></param>
        public async void UpdateUserDetailsFB(string username, string firstname, string surname, string email, string telephone, string street, string town, string country, float bestSquareScore, float bestRollutteScore)
        {
            ConnectToFirebase();

            String node = username + "Details" + "/";

            UserDetailsData userDetails = new UserDetailsData
            {
                username = username,
                firstname = firstname,
                surname = surname,
                email = email,
                telephone = telephone,
                street = street,
                town = town,
                country = country,
                bestSquareScore = bestSquareScore,
                bestRollutteScore = bestRollutteScore

            };

            var results = await firebase.Child(node).OnceAsync<UserDetailsData>();
            foreach (var details in results)
            {

                if (details.Object.username == username)
                {
                    //Delete the old row by key Id
                    await firebase.Child(node).Child(details.Key).DeleteAsync();
                    break;
                }
            }

            //Create a new row  with the updated values
            await firebase.Child(node).PostAsync<UserDetailsData>(userDetails);
        }

        public void UpdateBoardScores()
        {
            DAO d = new DAO();
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    if (App.EdittedAcount == true)
                    {
                        UpdateUserDetailsFB(App.MainUsername, App.MainFirstname, App.MainSurname, App.MainEmail, App.MainTelephone, App.MainStreet, App.MainTown, App.MainCountry, App.SquareBestScores, App.RoulleteBestScores);
                    }
                    else
                    {
                        UpdateUserDetailsFB(App.CurrentUser, "FirstName", "Surname", "Email", "Tellphone", "Street", "Town", "Country", App.SquareBestScores, App.RoulleteBestScores);
                    }
                     
                    break;
                case Device.UWP:
                    UpdateUserDetailsFB(App.MainUsername, App.MainFirstname, App.MainSurname, App.MainEmail, App.MainTelephone, App.MainStreet, App.MainTown, App.MainCountry, App.SquareBestScores, App.RoulleteBestScores);
                    d.UpdateScoresUWP(App.MainUsername, App.SquareBestScores, App.RoulleteBestScores);
                    break;
                default:
                    break;
            }
        }

            /// <summary>
            /// Delete User in Firebase
            /// </summary>
            /// <param name="username"></param>
            public async void DeletUserDetailsFB(string username)
        {
            ConnectToFirebase();

            String UserDetailsNode = username + "Details" + "/";
            String UserLoginNode = "details/";

            UserDetailsData userDetails = new UserDetailsData
            {
                username = username,
            };

            //Delete User details
            var results = await firebase.Child(UserDetailsNode).OnceAsync<UserDetailsData>();
            foreach (var details in results)
            {

                if (details.Object.username == username)
                {
                    //Delete the old row by key Id
                    await firebase.Child(UserDetailsNode).Child(details.Key).DeleteAsync();
                    break;
                }
            }

            //Delete Users Login Details
            var rlts = await firebase.Child(UserLoginNode).OnceAsync<Data>();
            foreach (var uName in rlts)
            {
                if (uName.Object.username == username)
                {
                    await firebase.Child(UserLoginNode).Child(uName.Key).DeleteAsync();
                    break;
                }
            }

        }

       // public  UserDetails GetUserByUserName(String username)
        public async Task<String> ReadDataFbAndroid(String username)
        {
            //UserDetails userDetails = null;
            string t = "";

           // await firebase.Child("Details/").DeleteAsync();

            //Open connection with Firebase
            ConnectToFirebase();
            String UserDetailsNode = username + "Details" + "/";

            try
            {
                //Read from Firebase 
                var results = await firebase.Child(UserDetailsNode).OnceAsync<UserDetailsData>();

                  // UserDetailsData ud = new UserDetailsData();
                   foreach (var details in results)
                   {
                        if (App.IsLogin == true)
                         {
                           App.SquareBestScores = details.Object.bestSquareScore;
                           App.RoulleteBestScores = details.Object.bestRollutteScore;
                         }

                    t = details.Object.firstname + "*" + details.Object.surname
                        + "*" + details.Object.email + "*" + details.Object.telephone + "*" + details.Object.street
                        + "*" + details.Object.town + "*" + details.Object.country + "*" + details.Object.bestRollutteScore
                        + "*" + details.Object.bestSquareScore;

                    App.MainUsername = username;
                    App.MainFirstname = details.Object.firstname;
                    App.MainSurname = details.Object.surname;
                    App.MainEmail = details.Object.email;
                    App.MainTelephone = details.Object.telephone;
                    App.MainStreet = details.Object.street;
                    App.MainTown = details.Object.town;
                    App.MainCountry = details.Object.country;
                }

                }
            catch
            {
                System.Diagnostics.Debug.WriteLine("User not in Firebase");
            }
            return t;
        }


            /// <summary>
            /// Delete the sqlite local userdetails table and update it with the firebase information when you sign in.
            /// </summary>
            /// <param name="username"></param>
            public async void ReadDataFbUWP(string username)
        {
            
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
       
                    break;
                case Device.UWP:

                    //Open connection with Firebase
                    ConnectToFirebase();
                    String UserDetailsNode = username + "Details" + "/";

                    //Establish SQLite connection and populate treatment table
                    using (SqliteConnection db =
                                    new SqliteConnection("Filename=PincodeUserFile.db"))
                    {
                        //open sqlite Connection 
                        db.Open();

                        //Read from Firebase and populate the local datbase/SQLite usersDetails table
                        var results = await firebase.Child(UserDetailsNode).OnceAsync<UserDetailsData>();
                        foreach (var details in results)
                        {
                            SqliteCommand insertCommand = new SqliteCommand();
                            insertCommand.Connection = db;

                            //update the main scores to appear on the score boards.
                            if (App.IsLogin == true)
                            {
                                App.SquareBestScores = details.Object.bestSquareScore;
                                App.RoulleteBestScores = details.Object.bestRollutteScore;
                            }

                            //Update the current users details
                            App.MainUsername = username;
                            App.MainFirstname = details.Object.firstname;
                            App.MainSurname = details.Object.surname;
                            App.MainEmail = details.Object.email;
                            App.MainTelephone = details.Object.telephone;
                            App.MainStreet = details.Object.street;
                            App.MainTown = details.Object.town;
                            App.MainCountry = details.Object.country;


                            //Check if user is in local/SQLite db
                            DAO d = new DAO();
                            int cnt = d.GetUserByUserName1(username);

                            if (cnt > 0)
                            {
                                //Update local db
                                insertCommand.CommandText = "UPDATE usersDetails SET username=@Username, firstName =@FirstName, surname =@Surname, email =@Email, telephone =@Telephone,street =@Street,town =@Town, country =@Country, bestSquareScore =@BestSquareScore, bestRollutteScore =@BestRollutteScore  where username=@Username;";
                                insertCommand.Parameters.AddWithValue("@Username", details.Object.username);
                                insertCommand.Parameters.AddWithValue("@FirstName", details.Object.firstname);
                                insertCommand.Parameters.AddWithValue("@Surname", details.Object.surname);
                                insertCommand.Parameters.AddWithValue("@Email", details.Object.email);
                                insertCommand.Parameters.AddWithValue("@Telephone", details.Object.telephone);
                                insertCommand.Parameters.AddWithValue("@Street", details.Object.street);
                                insertCommand.Parameters.AddWithValue("@Town", details.Object.town);
                                insertCommand.Parameters.AddWithValue("@Country", details.Object.country);
                                insertCommand.Parameters.AddWithValue("@BestSquareScore", details.Object.bestSquareScore);
                                insertCommand.Parameters.AddWithValue("@BestRollutteScore", details.Object.bestRollutteScore);
                                insertCommand.ExecuteReader();
                            }
                            else
                            {
                                // create a new row with the new user details in the local db
                                insertCommand.CommandText = "INSERT INTO usersDetails (username,firstName,surname,email,telephone,street,town,country,bestSquareScore,bestRollutteScore) VALUES (@Username,@FirstName,@Surname,@Email,@Telephone,@Street,@Town,@Country,@BestSquareScore,@BestRollutteScore);";
                                insertCommand.Parameters.AddWithValue("@Username", details.Object.username);
                                insertCommand.Parameters.AddWithValue("@FirstName", details.Object.firstname);
                                insertCommand.Parameters.AddWithValue("@Surname", details.Object.surname);
                                insertCommand.Parameters.AddWithValue("@Email", details.Object.email);
                                insertCommand.Parameters.AddWithValue("@Telephone", details.Object.telephone);
                                insertCommand.Parameters.AddWithValue("@Street", details.Object.street);
                                insertCommand.Parameters.AddWithValue("@Town", details.Object.town);
                                insertCommand.Parameters.AddWithValue("@Country", details.Object.country);
                                insertCommand.Parameters.AddWithValue("@BestSquareScore", details.Object.bestSquareScore);
                                insertCommand.Parameters.AddWithValue("@BestRollutteScore", details.Object.bestRollutteScore);
                                insertCommand.ExecuteReader();
                            }
                         
                        }

                        db.Close();
                    }

                    break;
                default:
                    break;
            }

           
        }
    }
}
