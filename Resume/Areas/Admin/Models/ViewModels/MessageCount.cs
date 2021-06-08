using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Areas.Admin.Models.ViewModels
{
    public class MessageCount
    {
        public int Inbox { get; set; }
        public int Read { get; set; }
        public int Answered { get; set; }
    }
}
