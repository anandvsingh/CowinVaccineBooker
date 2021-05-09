using System.Collections.Generic;
using System.Linq;


namespace AvaiabiltyChecker.DataObjects
{

    public class Center
    {
        public int center_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string block_name { get; set; }
        public int pincode { get; set; }
        public int lat { get; set; }
        public int @long { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string fee_type { get; set; }
        public List<Session> sessions { get; set; }
        public List<VaccineFee> vaccine_fees { get; set; }

        public bool AvailableSessions ()
        {
            int flag = 0;
            this.sessions.Where(_ => _.available_capacity > 0)
                         .Where(_ => _.min_age_limit == 18)
                         .ToList().ForEach(x => {
                             x.SessionInformation(pincode, center_id, name, fee_type);
                             flag++; });
            if (flag > 0)
                return true;
            return false;
        }
    }


}
