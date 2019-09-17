using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlpataProje.Models.Entity
{
    public class CareRequests
    {
        [Required(AllowEmptyStrings = false)]
        [Key]
        public int RequestID { get; set; }

        [Column(TypeName ="DateTime2")]
        [DisplayName("Randevu Tarih - Saat")]
        public DateTime Date { get; set; }
        [DisplayName("Detay")]
        public string Details { get; set; }
        [DisplayName("Talep Durumu")]
        public string RequestStatus { get; set; }
        public int UserID { get; set; }       
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}