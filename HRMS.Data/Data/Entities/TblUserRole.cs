using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Data.Entities
{
    [Keyless]
    [Table("tbl_user_roles")]
    public partial class TblUserRole
    {
        
        public int? UserId { get; set; }
        public int? RoleId { get; set;}

        [ForeignKey(nameof(RoleId))]
        public virtual TblRole Role { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual  TblUser User { get; set; }   
        
    }
}
