﻿using APPHDotNetCore.NLayer.DataAccess.Services;
using APPHDotNetCore.NLayer.DataAccess.Models;

namespace APPHDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;
        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }
        public List<BlogModel> GetBlogs()
        {
            var lst = _daBlog.GetBlogs();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var item = _daBlog.GetBlog(id);
            return item;
        }
        public int CreateBlg(BlogModel requestModel)
        {
            var result = _daBlog.CreateBlg(requestModel);
            return result;
        }
        public int UpdateBlog(int id, BlogModel requstModel)
        {
            var result = _daBlog.UpdateBlog(id,requstModel);
            return result;
        }
        public int PatchBlog(int id, BlogModel requestModel)
        {
            var result = _daBlog.PatchBlog(id,requestModel);
            return result;
        }
        public int DeleteBlog(int id)
        {           
            var result = _daBlog.DeleteBlog(id);
            return result;
        }
    }
}
