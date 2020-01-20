using System;
using System.Collections.Generic;

namespace BlogModels
{
    public partial class SystemParam
    {
        public int Id { get; set; }
        public string ParameKey { get; set; }
        public string ParameValues { get; set; }
        public string ParameType { get; set; }
        public bool? Editable { get; set; }
    }
}
