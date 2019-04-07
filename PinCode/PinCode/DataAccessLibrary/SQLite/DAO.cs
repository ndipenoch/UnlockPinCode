using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.Sqlite;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace PinCode
{
    public class DAO
    {
        /// <summary>
        /// Create a SQLIte database and user details table
        /// </summary>
        public void InitializeDatabase()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    break;
                case Device.UWP:
                    using (SqliteConnection db = new SqliteConnection("Filename=PincodeUserFile.db"))
                    {
                        db.Open();
                        String usersDetails = "CREATE TABLE IF NOT EXISTS usersDetails(username varchar(20)NOT NULL, firstName varchar(30),surname varchar(30),email varchar(30),telephone varchar(15),street varchar(45),town varchar(30),country varchar(15),bestSquareScore float,bestRollutteScore float, PRIMARY KEY (username))";
                        SqliteCommand createUsersDetails = new SqliteCommand(usersDetails, db);
                        createUsersDetails.ExecuteReader();
                    }
                    break;
                default:
                    break;
            }

        }


        /// <summary>
        /// Insert user detailsinto SQLite DB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>
        /// <param name="street"></param>
        /// <param name="town"></param>
        /// <param name="country"></param>
        /// <param name="bestScores"></param>
        public void AddUserDetails(string username, string firstName, string surname, string email, string telephone, string street, string town, string country, float bestSquareScore, float bestRollutteScore)
        {
            try
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:

                        break;
                    case Device.UWP:
                        using (SqliteConnection db =
                        new SqliteConnection("Filename=PincodeUserFile.db"))

                        {
                            db.Open();

                            SqliteCommand insertCommand = new SqliteCommand();
                            insertCommand.Connection = db;

                            // Use parameterized query
                            insertCommand.CommandText = "INSERT INTO usersDetails (username,firstName,surname,email,telephone,street,town,country,bestSquareScore,bestRollutteScore) VALUES (@Username,@FirstName,@Surname,@Email,@Telephone,@Street,@Town,@Country,@BestSquareScore,@BestRollutteScore);";
                            insertCommand.Parameters.AddWithValue("@Username", username);
                            insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                            insertCommand.Parameters.AddWithValue("@Surname", surname);
                            insertCommand.Parameters.AddWithValue("@Email", email);
                            insertCommand.Parameters.AddWithValue("@Telephone", telephone);
                            insertCommand.Parameters.AddWithValue("@Street", street);
                            insertCommand.Parameters.AddWithValue("@Town", town);
                            insertCommand.Parameters.AddWithValue("@Country", country);
                            insertCommand.Parameters.AddWithValue("@BestSquareScore", bestSquareScore);
                            insertCommand.Parameters.AddWithValue("@BestRollutteScore", bestRollutteScore);

                            insertCommand.ExecuteReader();

                            db.Close();
                        }
                        break;
                    default:
                        break;
                }

            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Username already taken ");
            }

        }

        /// <summary>
        /// Update userdetails in SQLite DB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>
        /// <param name="street"></param>
        /// <param name="town"></param>
        /// <param name="country"></param>
        /// <param name="bestScores"></param>
        public void UpdateUserDetails(string username, string firstName, string surname, string email, string telephone, string street, string town, string country, float bestSquareScore, float bestRollutteScore)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    break;
                case Device.UWP:
                    using (SqliteConnection db =
                   new SqliteConnection("Filename=PincodeUserFile.db"))
                    {
                        db.Open();

                        SqliteCommand insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "UPDATE usersDetails SET firstName =@FirstName, surname =@Surname, email =@Email, telephone =@Telephone,street =@Street,town =@Town, country =@Country, bestSquareScore =@BestSquareScore, bestRollutteScore =@BestRollutteScore  where username=@Username;";
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                        insertCommand.Parameters.AddWithValue("@Surname", surname);
                        insertCommand.Parameters.AddWithValue("@Email", email);
                        insertCommand.Parameters.AddWithValue("@Telephone", telephone);
                        insertCommand.Parameters.AddWithValue("@Street", street);
                        insertCommand.Parameters.AddWithValue("@Town", town);
                        insertCommand.Parameters.AddWithValue("@Country", country);
                        insertCommand.Parameters.AddWithValue("@BestSquareScore", bestSquareScore);
                        insertCommand.Parameters.AddWithValue("@BestRollutteScore", bestRollutteScore);

                        insertCommand.ExecuteNonQuery();

                        db.Close();
                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Update SQLite if the userscores changes
        /// </summary>
        /// <param name="username"></param>
        /// <param name="bestSquareScore"></param>
        /// <param name="bestRollutteScore"></param>
        public void UpdateScoresUWP(string username, float bestSquareScore, float bestRollutteScore)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    break;
                case Device.UWP:
                    using (SqliteConnection db =
                   new SqliteConnection("Filename=PincodeUserFile.db"))
                    {
                        db.Open();

                        SqliteCommand insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "UPDATE usersDetails SET bestSquareScore =@BestSquareScore, bestRollutteScore =@BestRollutteScore  where username=@Username;";
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@BestSquareScore", bestSquareScore);
                        insertCommand.Parameters.AddWithValue("@BestRollutteScore", bestRollutteScore);

                        insertCommand.ExecuteNonQuery();

                        db.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Get userdetails by username from SQLite DB
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserDetails GetUserByUserName(String username)
        {
            UserDetails userDetails = null;

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    break;
                case Device.UWP:
                    using (SqliteConnection db =
                 new SqliteConnection("Filename=PincodeUserFile.db"))

                    {
                        db.Open();

                        SqliteCommand selectCommand = new SqliteCommand
                            ("SELECT * from usersDetails where username=@Username", db);
                        selectCommand.Parameters.AddWithValue("@Username", username);

                        SqliteDataReader query = selectCommand.ExecuteReader();

                        while (query.Read())
                        {
                            userDetails = new UserDetails(
                                query.GetString(0),
                                query.GetString(1),
                                query.GetString(2),
                                query.GetString(3),
                                query.GetString(4),
                                query.GetString(5),
                                query.GetString(6),
                                query.GetString(7),
                                query.GetFloat(8),
                                query.GetFloat(9)
                             );
                        }
                        db.Close();
                    }
                    break;
                default:
                    break;
            }

            return userDetails;
        }

        /// <summary>
        ///Get the number of Users in the SQLite DAO by the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetUserByUserName1(String username)
        {
            int count = 0;

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    break;
                case Device.UWP:
                    using (SqliteConnection db =
                 new SqliteConnection("Filename=PincodeUserFile.db"))

                    {
                        db.Open();

                        SqliteCommand selectCommand = new SqliteCommand
                            ("SELECT * from usersDetails where username=@Username", db);
                        selectCommand.Parameters.AddWithValue("@Username", username);

                        SqliteDataReader query = selectCommand.ExecuteReader();

                        while (query.Read())
                        {
                            count++;
                        }
                        db.Close();
                    }
                    break;
                default:
                    break;
            }

            return count;
        }

        /// <summary>
        /// Delete user account from SQLite DB
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<UserDetails> DeleteUserAccount(String username)
        {

            List<UserDetails> userDetails = new List<UserDetails>();

            switch (Device.RuntimePlatform)
            {
                case Device.Android:

                    break;
                case Device.UWP:

                    using (SqliteConnection db =
                new SqliteConnection("Filename=PincodeUserFile.db"))
                    {
                        db.Open();

                        SqliteCommand selectCommand = new SqliteCommand
                           ("DELETE from usersDetails where username=@Username", db);
  
                        selectCommand.Parameters.AddWithValue("@Username", username);

                        SqliteDataReader query = selectCommand.ExecuteReader();

                        while (query.Read())
                        {
                            userDetails.Add(new UserDetails(
                             query.GetString(0),
                                query.GetString(1),
                                query.GetString(2),
                                query.GetString(3),
                                query.GetString(4),
                                query.GetString(5),
                                query.GetString(6),
                                query.GetString(7),
                                query.GetFloat(8),
                                query.GetFloat(9)
                            ));
                        }

                        db.Close();
                    }
                    break;
                default:
                    break;
            }

            return userDetails;
        }

    }
}
