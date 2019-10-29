using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoatManagement.Models
{
    public class BoatInfo
    {
        public int BoatId { get; set; }
        [Required]
        [DisplayName("Boat Name")]
        [StringLength(50)]
        public string BoatName { get; set; }
        [Required]
        [DisplayName("Boat Reg. No.")]
        [StringLength(50)]
        public string BoatRegNo { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Owner Name")]
        public string OwnerName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Owner NID No.")]
        public string OwnerNidNo { get; set; }
        [Required]
        [RegularExpression(@"\+?(88)?0?1[56789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        [DisplayName("Owner Mobile No.")]
        public string OwnerMobileNo { get; set; }
        public List<Sailors> Sailors { get; set; }
        public List<Fishermans> Fishermans { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Starting Time")]
        public string StartingTime { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public int TotalPerson { get; set; }
        public bool Status { get; set; }
    }

    public class Sailors
    {
        public int SailorId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Sailor Name")]
        public string SailorName { get; set; }
        [Required]
        [RegularExpression(@"\+?(88)?0?1[56789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        [DisplayName("Mobile No.")]
        public string SailorMobileNo { get; set; }
        public int BoatId { get; set; }
    }
    public class Fishermans
    {
        public int FishermanId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Fisherman Name")]
        public string FishermanName { get; set; }
        [RegularExpression(@"\+?(88)?0?1[56789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        [DisplayName("Mobile No.")]
        public string FishermanMobileNo { get; set; }
        public int BoatId { get; set; }
    }
}