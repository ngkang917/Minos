using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Minos.Domain.Define;
using Minos.Domain.Exceptions;
using Minos.Domain.SeedWork;

namespace Minos.Domain.Aggregates.Model
{
    [Table("AdminGroup")]
    public class AdminGroup : IAggregateRoot
    {
        public AdminGroup()
        {
            this.admins = new List<Admin>();
            this.adminMenus = new List<AdminMenu>();
        }

        [Key]
        [Column("AG_Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? id { get; set; }

        [Column("AG_Name")]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceName = "RangeErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string name { get; set; }

        [Column("AG_Level")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public int? level
        {
            get
            {
                return level;
            }
            set
            {
                if (value > 0)
                    level = value;
                else
                {
                    throw new MinosException("0 이하의 값은 설정 할 수 없습니다.");
                };
            }
        }

        [Column("AG_Description")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceName = "NotOveredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string description { get; set; }

        [Column("AG_UseFlag", TypeName = "char(1)")]
        [MaxLength(1, ErrorMessageResourceName = "FixedErrorMessage", ErrorMessageResourceType = typeof(Message))]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public string useFlag { get; set; }

        [Column("AG_InsDate")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Message))]
        public DateTime? insertDate { get; set; }

        public ICollection<Admin> admins { get; set; }

        public ICollection<AdminMenu> adminMenus { get; set; }
    }
}
