using System;
using System.Collections.Generic;

#nullable disable

namespace IT_Inventory_rest_api.Data
{
    public partial class AdUsersTe
    {
        public int Edmxid { get; set; }
        public string SAmaccountName { get; set; }
        public string Sn { get; set; }
        public string Cn { get; set; }
        public string GivenName { get; set; }
        public string Title { get; set; }
        public string DisplayName { get; set; }
        public string PhysicalDeliveryOfficeName { get; set; }
        public byte[] ObjectGuid { get; set; }
        public byte[] ObjectSid { get; set; }
    }
}
