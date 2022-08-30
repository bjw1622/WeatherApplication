using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WeatherApplication.Models
{
    public class Weather
    {

        private SqlConnection con;

        public void Conn()
        {
            string constr = ConfigurationManager.ConnectionStrings["WeatherDB"].ToString();
            con = new SqlConnection(constr);
        }

        public void AddDetails(WeatherEntity obj)
        {
            Conn();
            con.Open();
            // 사용할 로시저의 이름을 설정
            using (SqlCommand com = new SqlCommand("dbo.InsertWeather", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Region", obj.Region);
                com.Parameters.AddWithValue("@Main_Temp", obj.Main_Temp);
                com.Parameters.AddWithValue("@Min_Temp", obj.Min_Temp);
                com.Parameters.AddWithValue("@Max_Temp", obj.Max_Temp);
                com.Parameters.AddWithValue("@Feel_Temp", obj.Feel_Temp);
                com.Parameters.AddWithValue("@Wind", obj.Wind);
                com.Parameters.AddWithValue("@Date_T", obj.Date_T);
                com.ExecuteNonQuery();
            }
            con.Close();
            con.Dispose();
        }

        public List<WeatherEntity> GetList()
        {
            List<WeatherEntity> weathers = new List<WeatherEntity>();
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.SelectWeather", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    WeatherEntity wea = new WeatherEntity();
                    wea.Region = Convert.ToString(rdr["Region"]);
                    wea.Main_Temp = Convert.ToDouble(rdr["Temp_Main"]);
                    wea.Min_Temp = Convert.ToDouble(rdr["Temp_Min"]);
                    wea.Max_Temp = Convert.ToDouble(rdr["Temp_Max"]);
                    wea.Feel_Temp = Convert.ToDouble(rdr["Temp_Feel"]);
                    wea.Wind = Convert.ToInt64(rdr["Wind"]);
                    wea.Date_T = Convert.ToString(rdr["Date_T"]);
                    weathers.Add(wea);

                }
                rdr.Close();
            }
            con.Close();
            con.Dispose();
            return weathers;
        }
    }
}