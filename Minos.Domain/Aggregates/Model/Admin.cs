using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Minos.Domain.Define;
using Minos.Domain.SeedWork;

namespace Minos.Domain.Aggregates.Model
{
    [Table("Admin")]
    public class Admin: IAggregateRoot
    {
        public Admin()
        {
            this.users = new List<User>();
        }

        [Key]
        [Column("A_Idx")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? idx { get; set; }

        [ForeignKey("AG_Id")]
        public AdminGroup adminGroup { get; set; }

        [Column("A_AG_Id")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? AG_Id { get; set; }

        [Column("A_Id")]
        [StringLength(50, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string id { get; set; }

        [Column("A_Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [DataType(DataType.Password, ErrorMessageResourceName = "DataTypeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string password { get; set; }

        [Column("A_Name")]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string name { get; set; }

        [Column("A_email")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "DataTypeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string email { get; set; }

        [Column("A_Phone")]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "DataTypeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string phone { get; set; }

        [Column("A_UseFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string useFlag { get; set; }

        [Column("A_InsDate")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public DateTime? insertDate { get; set; }

        public ICollection<User> users { get; set; }
    }
}
