using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Minos.Domain.Define;
using Minos.Domain.SeedWork;

namespace Minos.Domain.Aggregates.Model
{
    [Table("UserGroup")]
    public class UserGroup : IAggregateRoot
    {
        [Key]
        [Column("UG_Idx")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? idx { get; set; }

        [ForeignKey("companyIdx")]
        public Company company { get; set; }

        [Column("UG_C_Idx")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? companyIdx { get; set; }

        [ForeignKey("userIdx")]
        public User user { get; set; }

        [Column("UG_U_Idx")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? userIdx { get; set; }

        [Column("UG_level")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? level { get; set; }

        [Column("UG_UseFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string useFlag { get; set; }

        [Column("UG_DeleteFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string deleteFlag { get; set; }

        [Column("UG_InsDate")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public DateTime? insertDate { get; set; }
    }
}
