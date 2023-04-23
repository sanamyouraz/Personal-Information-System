using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Person_Information_System.Models;


using System.Configuration;
using System.Data.SqlClient;


namespace Person_Information_System.DB
{
    public class DbConn
    {

        private string connectionString = string.Empty;

        private SqlConnection sqlcon;

        public DbConn()
        {
            connectionString = ConfigurationManager.ConnectionStrings["myConnection"].ToString();

        }
        public void createConnection()
        {
            sqlcon = new SqlConnection(connectionString);

        }

        public void SaveData(PersonInfo data, out string message)
        {
            try
            {
                createConnection();
                SqlCommand cmd = new SqlCommand("USP_Personal_Info", sqlcon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                string jsondata = JsonConvert.SerializeObject(data.AddressList);
                string Edujson = JsonConvert.SerializeObject(data.Educationlist);
                cmd.Parameters.AddWithValue("@Salutation", data.Salutation);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Age", data.Age);
                cmd.Parameters.AddWithValue("@Gender", data.Gender);
                cmd.Parameters.AddWithValue("@PhoneNumber", data.Phone);
                cmd.Parameters.AddWithValue("@Add_Json1", Edujson);
                cmd.Parameters.AddWithValue("@Nationality", data.nationality);
                cmd.Parameters.AddWithValue("@Add_Json", jsondata);

                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();

                message = "Success";

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }



        }
        public List<ShowDetails> ShowData()
        {


            createConnection();
            List<ShowDetails> ShowList = new List<ShowDetails>();
            SqlCommand cmd = new SqlCommand("[SelectPersonalInfo]", sqlcon);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcon.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var ShowDetails = new ShowDetails();
                ShowDetails.Salutation = rdr["Salutation"].ToString();
                ShowDetails.FirstName = rdr["FirstName"].ToString();
                ShowDetails.LastName = rdr["LastName"].ToString();
                ShowDetails.Phone = rdr["PhoneNumber"].ToString();
                ShowDetails.Email = rdr["Email"].ToString();
                ShowDetails.Age = rdr["Age"].ToString();
                ShowDetails.Gender = rdr["Gender"].ToString();
                ShowDetails.nationality = rdr["Nationality"].ToString();
                ShowDetails.PersonId = int.Parse(rdr["PersonId"].ToString());

                ShowList.Add(ShowDetails);



            }
            //if ( rdr.NextResult() && rdr.HasRows())
            //{

            //}
            sqlcon.Close();


            return ShowList;








        }
        public void DeleteData(int? Id, out string message)
        {
            try
            {
                createConnection();
                SqlCommand cmd = new SqlCommand("DeletePersonalInfo", sqlcon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PersonId", Id);
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();

                message = "Success";

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

        }

        public PersonInfo EditData(int? Id)
        {
            PersonInfo personalinfo = new PersonInfo();
            List<Educationdata> Edulist = new List<Educationdata>();
            List<Address> AddList = new List<Address>();
            string CS = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("EditPersonInfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonId", Id);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        personalinfo.PersonId = int.Parse(rdr["PersonId"].ToString());
                        personalinfo.Salutation = rdr["Salutation"].ToString();
                        personalinfo.FirstName = rdr["FirstName"].ToString();
                        personalinfo.LastName = rdr["LastName"].ToString();
                        personalinfo.Email = rdr["Email"].ToString();
                        personalinfo.Age = rdr["Age"].ToString();
                        personalinfo.Phone = rdr["PhoneNumber"].ToString();
                        personalinfo.Gender = rdr["Gender"].ToString();
                        personalinfo.nationality = rdr["Nationality"].ToString();
                    }

                }

                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        AddList.Add(new Address
                        {
                            AddressType = rdr["AddressType"].ToString(),
                            Province = rdr["Province"].ToString(),
                            City = rdr["City"].ToString(),
                            Ward = rdr["Ward"].ToString(),
                            selectedCat = rdr["City_Category"].ToString(),
                            Tole = rdr["Tole"].ToString()

                        });

                    }
                    personalinfo.AddressList = AddList;

                }
                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        Edulist.Add(new Educationdata
                        {
                            chosenEdu = rdr["chosenEdu"].ToString()
                        });

                    }
                    personalinfo.Educationlist = Edulist;
                }
            }
            return personalinfo;
        }

        public void UpdateData(PersonInfo data, out string message)
        {
            try
            {
                createConnection();
                SqlCommand cmd = new SqlCommand("UpdatePersonalInfo", sqlcon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                string jsondata = JsonConvert.SerializeObject(data.AddressList);
                string Edujson = JsonConvert.SerializeObject(data.Educationlist);
                cmd.Parameters.AddWithValue("@PersonId", data.PersonId);
                cmd.Parameters.AddWithValue("@Salutation", data.Salutation);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Age", data.Age);
                cmd.Parameters.AddWithValue("@Gender", data.Gender);
                cmd.Parameters.AddWithValue("@PhoneNumber", data.Phone);
                cmd.Parameters.AddWithValue("@Add_Json1", Edujson);
                cmd.Parameters.AddWithValue("@Nationality", data.nationality);
                cmd.Parameters.AddWithValue("@Add_Json", jsondata);

                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();

                message = "Success";

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }



        }






    }


}