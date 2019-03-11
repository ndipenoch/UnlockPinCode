using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Text;

namespace PinCode
{
    public class DAO
    {
        /// <summary>
        /// Create a SQLIte database and user details table
        /// </summary>
        public  void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=PincodeUserFile.db"))
            {
                db.Open();

                String usersDetails = "CREATE TABLE IF NOT EXISTS usersDetails(username varchar(20)NOT NULL, firstName varchar(30)NOT NULL,surname varchar(30),email varchar(30),telephone varchar(15),street varchar(45),town varchar(30),country varchar(15),bestScores float,PRIMARY KEY (username))";
                SqliteCommand createUsersDetails = new SqliteCommand(usersDetails, db);
                createUsersDetails.ExecuteReader();
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
        public void AddUserDetails(string username, string firstName, string surname, string email, string telephone,string street,string town,string country,float bestScores)
        {
            try
            {
                using (SqliteConnection db =
               new SqliteConnection("Filename=PincodeUserFile.db"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    // Use parameterized query
                    insertCommand.CommandText = "INSERT INTO usersDetails (username,firstName,surname,email,telephone,street,town,country,bestScores) VALUES (@Username,@FirstName,@Surname,@Email,@Telephone,@Street,@Town,@Country,@BestScores);";
                    insertCommand.Parameters.AddWithValue("@Username", username);
                    insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                    insertCommand.Parameters.AddWithValue("@Surname", surname);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Telephone", telephone);
                    insertCommand.Parameters.AddWithValue("@Street", street);
                    insertCommand.Parameters.AddWithValue("@Town", town);
                    insertCommand.Parameters.AddWithValue("@Country", country);
                    insertCommand.Parameters.AddWithValue("@BestScores", bestScores);

                    insertCommand.ExecuteReader();

                    db.Close();
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
        public  void UpdateUserDetails(string username, string firstName, string surname, string email, string telephone, string street, string town, string country, float bestScores)
        {
            using (SqliteConnection db =
               new SqliteConnection("Filename=PincodeUserFile.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE usersDetails SET firstName =@FirstName, surname =@Surname, email =@Email, telephone =@Telephone,street =@Street,town =@Town, country =@Country, bestScores =@BestScores  where username=@Username;";
                insertCommand.Parameters.AddWithValue("@Username", username);
                insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                insertCommand.Parameters.AddWithValue("@Surname", surname);
                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.Parameters.AddWithValue("@Telephone", telephone);
                insertCommand.Parameters.AddWithValue("@Street", street);
                insertCommand.Parameters.AddWithValue("@Town", town);
                insertCommand.Parameters.AddWithValue("@Country", country);
                insertCommand.Parameters.AddWithValue("@BestScores", bestScores);

                insertCommand.ExecuteNonQuery();

                db.Close();
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
                        query.GetFloat(8)
       
                     );

                }

                db.Close();
            }

            return userDetails;
        }


        /// <summary>
        /// Delete user account fromSQLite DB
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<UserDetails> DeleteUserAccount(String username)
        {

            List<UserDetails> userDetails = new List<UserDetails>();

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
                        query.GetFloat(8)
                    ));
                }

                db.Close();
            }

            return userDetails;
        }

    }
}
