using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaiabiltyChecker.DataObjects
{
    public class CenterInformation
    {
        public int center_id { get; set; }
        public string name { get; set; }
        public string name_l { get; set; }
        public string address { get; set; }
        public string address_l { get; set; }
        public string state_name { get; set; }
        public string state_name_l { get; set; }
        public string district_name { get; set; }
        public string district_name_l { get; set; }
        public string block_name { get; set; }
        public string block_name_l { get; set; }
        public string pincode { get; set; }
        public double lat { get; set; }
        public double @long { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string fee_type { get; set; }
        public string fee { get; set; }
        public string session_id { get; set; }
        public string date { get; set; }
        public int available_capacity { get; set; }
        public int min_age_limit { get; set; }
        public string vaccine { get; set; }
        public List<string> slots { get; set; }

        public void CentreInformation()
        {
            Console.WriteLine(this.center_id + this.name + this.pincode);
        }
    }
}
