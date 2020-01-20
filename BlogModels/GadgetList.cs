using System;
using System.Collections.Generic;

namespace BlogModels
{
    public partial class GadgetList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HeadImg { get; set; }
        public string Content { get; set; }
        public bool? IsShow { get; set; }
        public string Label { get; set; }
        public DateTime Time { get; set; }
        public string LinkAddress { get; set; }
    }
}
