using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientChat.Model
{
    class MessageModel
    {
        public DateTime Time { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public byte[] Attache { get; set; }
    }
}
