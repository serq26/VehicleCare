using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlpataProje.Models.Entity
{
    
    public class User
    {
        [Required(AllowEmptyStrings = false)]
        public int UserID { get; set; }
        [DisplayName("Ad")]
        public string Name { get; set; }

        [DisplayName("Soyad")]
        public string Surname { get; set; }

        [DisplayName("E-posta")]
        public string Email { get; set; }

        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }

        [DisplayName("Şifre")]
        public string Password { get; set; }

        [DisplayName("Cinsiyet")]
        public string Gender { get; set; }

        [DisplayName("Ev Telefonu")]
        public string HomePhone { get; set; }

        [DisplayName("İş Telefonu")]
        public string WorkPhone { get; set; }

        [DisplayName("Cep Telefonu")]
        public string CellPhone { get; set; }

        [DisplayName("Tc Kimlik Numarası(Opsiyonel)")]
        public string TC { get; set; }

        [DisplayName("Meslek")]
        public string Profession { get; set; }

        [DisplayName("İl")]
        public string City { get; set; }

        [DisplayName("İlçe")]
        public string County { get; set; }

        [DisplayName("Adres")]
        public string Address { get; set; }

        [DisplayName("Şirket arabası mı?")]
        public bool IsCompanyCar { get; set; }

        [DisplayName("Şirket Adı")]
        public string CompanyName { get; set; }

        [DisplayName("Şirket Adresi")]
        public string CompanyAddress { get; set; }

        [DisplayName("Araçlarım")]
        public virtual List<Vehicle> Vehicles { get; set; }

    }
}