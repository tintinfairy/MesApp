using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAPI.Models
{
    public class Message
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string text { get; set; }

        public DateTime timeSent { get; set; }

        public string senderID { get; set; }

        public string receiverID { get; set; }

        public Message()
        {
            text = "defaultText";
            timeSent = DateTime.Now;
            senderID = "";
            receiverID = "";
        }

        public Message(string _text, DateTime _timeSent, string _senderID, string _receiverID)
        {
            text = _text;
            timeSent = _timeSent;
            senderID = _senderID;
            receiverID = _receiverID;
        }
    }
}
