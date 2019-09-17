using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlpataProje.Models.Entity
{
    public class Vehicle
    {
        [Required(AllowEmptyStrings = false)]
        public int VehicleID { get; set; }
        [DisplayName("Marka")]
        public string Brand { get; set; }
        [DisplayName("Model")]
        public string Marque { get; set; }
        [DisplayName("Motor Tipi")]
        public string MotorType { get; set; }
        [DisplayName("Araç Tipi")]
        public string VehicleType { get; set; }
        [DisplayName("Yıl")]
        public string Year { get; set; }
        [DisplayName("Km")]
        public int Km { get; set; }
        [DisplayName("Yakıt Tipi")]
        public string FuelType { get; set; }
        [DisplayName("Vites Tipi")]
        public string GearType { get; set; }
        [DisplayName("Plaka")]
        public string Plate { get; set; }
        [DisplayName("Araç Sahibinin Adı")]
        public string VehicleOwnerName { get; set; }
        [DisplayName("Araç Sahibinin Soyadı")]
        public string VehicleOwnerSurname { get; set; }
        [DisplayName("En Son Bakım Yapılan Yer")]
        public string LastCare { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public virtual List<CareRequests> CareRequests { get; set; }
    }
}