using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Landlyst.Models
{
    public class DALManager
    {
        //This is the connection string, which is placed on the eweb.config file
        private static string connString = ConfigurationManager.ConnectionStrings["LandlystConnString"].ConnectionString;

        //Searches the database for rooms based on the string returned from the CreateSearchParameters() method
        public static List<RoomModel> SearchRooms(string additions)
        {
            string param = CreateSearchParameters(additions);
            string sql = "USE Landlyst " +
                         "DECLARE @RoomAdditionResult TABLE(Room_Number int) " +
                         "DECLARE @RoomNumberResult TABLE(Room_Number int, Room_Price int, Room_Addition varchar(100), Addition_Price int) " +

                         "INSERT INTO @RoomNumberResult " +
                         "SELECT Room.RoomNumber,Room.Price,Addition.Addition,Addition.AdditionPrice " +
                         "FROM Room INNER JOIN RoomAdditions ra " +
                         "ON ra.RoomNumber = Room.RoomNumber INNER JOIN Addition " +
                         "ON ra.Addition_ID = Addition.Addition_ID " +
                         "WHERE Room.RoomNumber " +
                         "NOT IN(SELECT Reservation.RoomNumber FROM Reservation WHERE CheckinDate <= '2020-12-15' AND CheckoutDate >= '2020-12-19') " +
                         "INSERT INTO @RoomAdditionResult " +

                         CreateSearchParameters(additions) +

                         "SELECT Room_Number AS RoomNumber,r.Price AS RoomPrice,ad.Addition,ad.AdditionPrice AS AdditionPrice " +
                         "FROM @RoomAdditionResult " +
                         "INNER JOIN Room r " +
                         "ON r.RoomNumber = Room_Number " +
                         "INNER JOIN RoomAdditions ra " +
                         "ON ra.RoomNumber = Room_Number " +
                         "INNER JOIN Addition ad " +
                         "ON ad.Addition_ID = ra.Addition_ID ";
            using (var connection = new SqlConnection(connString))
            {
                var orderDictionary = new Dictionary<int, RoomModel>();
                var rooms = connection.Query<RoomModel, AdditionModel, RoomModel>(sql, (room, addition) =>
                    {
                        RoomModel Room;
                        if (!orderDictionary.TryGetValue(room.RoomNumber, out Room))
                        {
                            Room = room;
                            Room.Addition = new List<AdditionModel>();
                            orderDictionary.Add(Room.RoomNumber, Room);
                        }

                        Room.Addition.Add(addition);
                        return Room;
                    },
                    splitOn: "Addition")
                    .Distinct()
                    .ToList();
                return rooms;
            }
        }

        //Splits the string of parameter additions given from the view based on what the user chose
        public static string[] SplitParameter(string stringToSeparate, char separator)
        {
            string[] param = stringToSeparate.Split(separator);
            param = param.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return param;
        }

        //puts together a string of select statements based on how many additions were chosen
        public static string CreateSearchParameters(string additions)
        {
            string[] param = SplitParameter(additions, ',');
            string parameter = "";
            for (int i = 0; i < param.Length; i++)
            {
                if (i == param.Length - 1)
                {
                    parameter += $"SELECT Room_Number FROM @RoomNumberResult WHERE Room_Addition = '{param[i]}' ";
                }
                else
                {
                    parameter += $"SELECT Room_Number FROM @RoomNumberResult WHERE Room_Addition = '{param[i]}' INTERSECT ";
                }
            }
            return parameter;
        }

        //Creates a reservation based on inputs from the view. This also creates a customer if
        //a customer with the same entered phone number does not exist already
        public static void CreateReservation(CustomerModel model)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                var values = new
                {
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ZipCode = model.ZipCode,
                    Address = model.Address,
                    RoomNumber = model.Reservation.RoomNumber,
                    VisitorAmount = model.Reservation.VisitorAmount,
                    CheckinDate = model.Reservation.CheckinDate,
                    CheckoutDate = model.Reservation.CheckoutDate
                };
                conn.Query("CreateReservationAndCustomer", values, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}