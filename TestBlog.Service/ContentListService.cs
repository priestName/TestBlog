using BlogModels;
using System;
using System.Collections.Generic;
using System.Text;
using TestBlog.IRepositoty;
using TestBlog.IService;

namespace TestBlog.Service
{
    public class ContentListService : BaseService<ContentList>, IContentListService
    {
        public ContentListService(IContentListRepositoty contentListRepositoty) : base(contentListRepositoty)
        {

        }
    }
}
