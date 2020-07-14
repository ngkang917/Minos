using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Minos.Domain.Define;
using Minos.Domain.SeedWork;

namespace Minos.Domain.Aggregates.Model
{
    [Table("User")]
    public class User : IAggregateRoot
    {
        public User()
        {
            this.userGroups = new List<UserGroup>();
        }

        [Key]
        [Column("U_Idx")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? idx { get; set; }

        [ForeignKey("companyIdx")]
        public Company company { get; set; }

        [Column("U_C_Id")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? companyIdx { get; set; }

        [Column("U_Id")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string id { get; set; }

        [Column("U_Name")]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string name { get; set; }

        [Column("U_Phone")]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "DataTypeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string phone { get; set; }

        [Column("U_Email")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "DataTypeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string email { get; set; }

        [Column("U_UseFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string useFlag { get; set; }

        [Column("U_DeleteFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string deleteFlag { get; set; }

        [Column("U_InsDate")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public DateTime? insertDate { get; set; }

        [Column("U_UptDate")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public DateTime? updateDate { get; set; }

        [ForeignKey("adminIdx")]
        public Admin admin { get; set; }

        [Column("A_UptIdx")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? adminIdx { get; set; }

        public ICollection<UserGroup> userGroups { get; set; }
    }
}
