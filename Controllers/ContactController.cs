using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using Umbraco.Cms.Web;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using ShopAccessory.Models;
using Umbraco.Cms.Web.Website.Controllers;
using System.Linq;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;


namespace ShopAccessory.Controllers
{
    
    [PluginController("ShopAccessory")]
    public class ContactController : UmbracoApiController
    {

        private readonly IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult SubmitContactForm(ContactUsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SaveToSqlServer(model);
                    return Content("Dữ liệu đã được xử lý thành công!");
                }
                return Content("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại."); ;

            }
            catch (Exception ex)
            {

                return Content($"Đã xảy ra lỗi: {ex.Message}");


            }
        }
        private void SaveToSqlServer(ContactUsModel model)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("umbracoDbDSN")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ContactUs (Name, Email, Phone, Message) VALUES (@Name, @Email, @Phone, @Message)", connection);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Phone", model.Phone);
                cmd.Parameters.AddWithValue("@Message", model.Message);
                cmd.ExecuteNonQuery();
            }
        }
    }
    

}
