using System;
using System.Collections.Generic;

namespace BlogModels
{
    public partial class BrowseLog
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Equipment { get; set; }
    }
}
