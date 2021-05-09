using System;
using System.Collections.Generic;

namespace AvaiabiltyChecker.DataObjects
{
    public class Session
    {
        public string session_id { get; set; }
        public string date { get; set; }
        public int available_capacity { get; set; }
        public int min_age_limit { get; set; }
        public string vaccine { get; set; }
        public List<string> slots { get; set; }

        public void SessionInformation(int pincode, int center_id, string center_name)
        {
            Console.WriteLine("***********New Slots avaialble************" + "Time: " + System.DateTime.Now);
            Console.WriteLine("PinCode: " + pincode + "\nAvailable Capacity: " + this.available_capacity + "\nCenter id: " + center_id + "  Center Name: " + center_name + "\nDate of appointment: " + this.date + "\nEligibilty Age: " + this.min_age_limit);
        }
    }
}
