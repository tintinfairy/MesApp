using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppAPI.Models
{
    public class UserContacts
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string owner { get; set; }

        //https://docs.microsoft.com/en-us/ef/core/modeling/backing-field
        private string _extendedData;

        [NotMapped]
        public JObject contacts
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(_extendedData) ? "{}" : _extendedData);
            }
            set
            {
                _extendedData = value.ToString();
            }
        }

        public UserContacts()
        {
            owner = "";
            contacts = new JObject();
        }

        public UserContacts(string _owner, JObject _contacts)
        {
            owner = _owner;
            contacts = _contacts;
        }
    }
}
