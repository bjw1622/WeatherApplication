﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class WeathersController : Controller

    {   
        // sqlConnection 
        private SqlConnection con;

        // GET: Weathers
        public ActionResult Index()
        {
            return View();
        }

        //Post method to add details
        [HttpPost]
        public ActionResult AddWeathers(Weather obj)
        {
            Console.WriteLine(obj);
            AddDetails(obj);

            return View();
        }

        // DB연결
        private void Conn()
        {
            string constr = ConfigurationManager.ConnectionStrings["WeatherDB"].ToString();
            con = new SqlConnection(constr);

        }

        //DB에 추가 
        private void AddDetails(Weather obj)
        {
            Conn();
            con.Open();
            using (SqlCommand com = new SqlCommand("dbo.InsertWeather", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Main_Temp", obj.Main_Temp);
                com.Parameters.AddWithValue("@Min_Temp", obj.Min_Temp);
                com.Parameters.AddWithValue("@Max_Temp", obj.Max_Temp);
                com.Parameters.AddWithValue("@Feel_Temp", obj.Feel_Temp);
                com.Parameters.AddWithValue("@Wind", obj.Wind);
                com.Parameters.AddWithValue("@Weather_No", 1);
                int result = com.ExecuteNonQuery();
                Console.WriteLine("반영된 열의 개수 " + result);
                com.ExecuteNonQuery();
            }
            con.Close();
            con.Dispose();

        }
    }
}