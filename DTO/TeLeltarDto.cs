using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IT_Inventory_rest_api.DTO
{
    public class TeLeltarDto
    {
        public int Nid { get; set; }
        public string Nev { get; set; }
        public string Hely { get; set; }
        public string Felhasznalo { get; set; }
        public string Csoport { get; set; }
        public string Statusz { get; set; }
        public string Tipusok { get; set; }
        public string Gyarto { get; set; }
        public string Modell { get; set; }
        public string Sorozatszam { get; set; }
        public string LeltariSzam { get; set; }
    }
}
