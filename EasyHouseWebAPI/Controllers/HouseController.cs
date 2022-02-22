using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using EasyHouseWebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EasyHouseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string sqlDataSource;
        private readonly IWebHostEnvironment _env;
        public HouseController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("EasyHomeAppCon");
            _env = env;
            //
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT houseId, Address, city, state,FrontViewPhoto, price FROM dbo.House";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }
            }

            return new JsonResult(table);
        }

        //[Route("GetHouseById")]
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"SELECT houseId, Address, city, state,zipcode,
                            contactname,contactphone,FrontViewPhoto,price FROM dbo.House
                            where houseId = " + id + @"";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(House house)
        {
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {

                SqlCommand cmd = new SqlCommand("sp_AddHouse", mycon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@address", house.Address);
                cmd.Parameters.AddWithValue("@city", house.City);
                cmd.Parameters.AddWithValue("@state", house.State);
                cmd.Parameters.AddWithValue("@zipcode", house.ZipCode);
                cmd.Parameters.AddWithValue("@contactname", house.ContactName);
                cmd.Parameters.AddWithValue("@contactphone", house.ContactPhone);
                cmd.Parameters.AddWithValue("@frontviewphoto", house.FrontViewPhoto);
                cmd.Parameters.AddWithValue("@price", house.Price);

                mycon.Open();
                cmd.ExecuteNonQuery();
             }
            return new JsonResult("Added Successfully");

            
        
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);

            }
            catch (Exception)
            {

                return new JsonResult("NoAvailable.png");
            }


        }

    }
}
