﻿using APPHDotNetCore.RestApi.Models;
using APPHDotNetCore.Shared;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APPHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        //private readonly DapperService _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringbuilder.ConnectionString);

        private readonly DapperService _dapperService;

        public BlogDapper2Controller(DapperService dapperService)
        {
            _dapperService = dapperService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "Select * from tbl_Blog";
            //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringbuilder.ConnectionString);
            //List<BlogModel> lst = db.Query<BlogModel>(query).ToList();

            var lst = _dapperService.Query<BlogModel>(query).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "Select * from tbl_Blog where BlogId = @BlogId";
            
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query,new BlogModel { BlogId = id });
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreatetBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringbuilder.ConnectionString);
            //int result = db.Execute(query, blog);

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found.");
            }
            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [BlogTitle] = @BlogTitle
            ,[BlogAuthor] = @BlogAuthor
            ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";

            //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringbuilder.ConnectionString);
            //int result = db.Execute(query,blog);

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Successful Updating." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            string conditions = string.Empty;
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if(!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent,";
            }

            if(conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
            SET {conditions}
            WHERE BlogId = @BlogId";

            //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringbuilder.ConnectionString);
            //int result = db.Execute(query, blog);

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found.");
            }

            string query = @"DELETE [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringbuilder.ConnectionString);
            int result = _dapperService.Execute(query, new BlogModel { BlogId = id});

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "Select * from tbl_Blog where BlogId = @BlogId";
            //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringbuilder.ConnectionString);
            //var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();

            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query,new BlogModel { BlogId = id });
            return item;
        }

    }
}
