using System;
using System.Collections.Generic;

#nullable disable

namespace IT_Inventory_rest_api.Data
{
    public partial class TeGepek
    {
        public int Nid { get; set; }
        public string ComputerName { get; set; }
        public string DeviceManufacturer { get; set; }
        public string DeviceModel { get; set; }
        public string SerialNumber { get; set; }
    }
}
