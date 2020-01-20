using System;
using System.Collections.Generic;

namespace BlogModels
{
    public partial class OperationLog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Count { get; set; }
        public DateTime Time { get; set; }
        public string LoginIp { get; set; }
    }
}
