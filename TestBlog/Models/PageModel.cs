using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Models
{
    public class PageModel
    {
        private int _TableCount = 0;
        private int _PageCount = 0;
        private int _PageIndex = 0;
        private int _PageSize = 20;


        public int TableCount { 
            get { return _TableCount; } 
            set { _TableCount = value; } 
        }
        public int PageCount { 
            get { return _PageCount; } 
            set { _PageCount = value; } 
        }
        public int PageIndex { 
            get { return _PageIndex; } 
            set { _PageIndex = value; } 
        }
        public int PageSize {
            get { return _PageSize; } 
            set { _PageSize = value; }
        }
    }
}
