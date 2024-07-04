using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Data.Entities
{
    public partial class TblCountry
    {
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string CountryCode { get; set; }
        [StringLength(255)]
        public string CountryName { get; set; }
        [StringLength(100)]
        public string Region { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string LangCode { get; set; }
        [StringLength(50)]
        public string Zone { get; set; }
        public double? GoldBv { get; set; }
        public double? BronzeBv { get; set; }
        public double? PromoMco { get; set; }
        [StringLength(50)]
        public string Currency { get; set; }
        [StringLength(50)]
        public string CurrencyCode { get; set; }
        [StringLength(50)]
        public string CurrencySymbol { get; set; }
        public bool? IsDefault { get; set; }
        [StringLength(100)]
        public string Nationality { get; set; }
    }
}
