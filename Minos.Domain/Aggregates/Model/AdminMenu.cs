using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Minos.Domain.Define;
using Minos.Domain.SeedWork;

namespace Minos.Domain.Aggregates.Model
{
    [Table("AdminMenu")]
    public class AdminMenu : IAggregateRoot
    {
        [Key]
        [Column("AM_Idx")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int idx { get; set; }

        [ForeignKey("AG_Id")]
        public AdminGroup adminGroup { get; set; }

        [Column("AM_AG_Id")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? AG_Id { get; set; }

        [Column("AM_Up_Idx")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? Up_Idx { get; set; }

        [Column("AM_Ordered")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? Ordered { get; set; }

        [Column("AM_Title")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string title { get; set; }


        [Column("AM_Link")]
        [StringLength(100, MinimumLength = 10, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string link { get; set; }


        [Column("AM_Target")]
        [StringLength(100, MinimumLength = 10, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string target { get; set; }

        [Column("AM_DisplayFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string displayFlag { get; set; }

        [Column("AM_UseFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string useFlag { get; set; }

        [Column("AM_InsDate")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public DateTime? insertDate { get; set; }
    }
}
