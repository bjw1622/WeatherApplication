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

        // sqlConnection 
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
            // 사용할 프로시저의 이름을 설정
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

        List<WeatherEntity> weathers = new List<WeatherEntity>();
        public List<WeatherEntity> GetList()
        {
            Conn();
            con.Open();
            // 사용할 프로시저의 이름을 설정
            using (SqlCommand com = new SqlCommand("dbo.SelectWeather", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                
                com.ExecuteNonQuery();
                // SqlDataReader 객체를 리턴
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    // C# 인덱서를 사용하여
                    // 필드 데이타 엑세스
                    WeatherEntity wea = new WeatherEntity();
                    wea.Region = Convert.ToString(rdr["Region"]);
                    wea.Main_Temp = (long)Convert.ToDouble(rdr["Temp_Main"]);
                    wea.Min_Temp = (long)Convert.ToDouble(rdr["Temp_Min"]);
                    wea.Max_Temp = (long)Convert.ToDouble(rdr["Temp_Max"]);
                    wea.Feel_Temp = (long)Convert.ToDouble(rdr["Temp_Feel"]);
                    wea.Wind = Convert.ToInt64(rdr["Wind"]);
                    wea.Date_T = Convert.ToString(rdr["Date_T"]);
                    Console.WriteLine(wea.Date_T);
                    weathers.Add(wea);

                }
                // 사용후 닫음
                rdr.Close();
            }
            con.Close();
            con.Dispose();
            return weathers;
        }
    }
}