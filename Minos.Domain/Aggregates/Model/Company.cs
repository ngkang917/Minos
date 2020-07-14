using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Minos.Domain.Define;
using Minos.Domain.SeedWork;

namespace Minos.Domain.Aggregates.Model
{
    [Table("Company")]
    public class Company : IAggregateRoot
    {
        public Company()
        {
            this.users = new List<User>();
            this.devices = new List<Device>();
            this.userGroups = new List<UserGroup>();
        }

        [Key]
        [Column("C_Idx")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? idx { get; set; }

        [Column("C_Id")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string id { get; set; }

        [Column("C_Name")]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string name { get; set; }

        [Column("C_email")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "DataTypeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string email { get; set; }

        [Column("C_Phone")]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "DataTypeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string phone { get; set; }

        [Column("C_UserMaxCount")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? userMaxCount { get; set; }

        [Column("C_DBVersion")]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string DBVersion { get; set; }

        [Column("C_DisplayFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string displayFlag { get; set; }

        [Column("C_UseFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string useFlag { get; set; }

        [Column("C_DeleteFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string deleteFlag { get; set; }

        [Column("C_InsDate")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public DateTime? insertDate { get; set; }

        public ICollection<User> users { get; set; }

        public ICollection<Device> devices { get; set; }

        public ICollection<UserGroup> userGroups { get; set; }
    }
}
