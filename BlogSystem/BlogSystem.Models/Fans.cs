using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Fans:BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 关注的用户编号
        /// </summary>
        [ForeignKey(nameof(FocusUser))]
        public Guid FocusUserId { get; set; }
        public User FocusUser { get; set; }
    }
}
