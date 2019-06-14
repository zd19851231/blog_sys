using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    /// <summary>
    /// 评论表
    /// </summary>
    public class Comment:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(800)]
        public string Content { get; set; }

        [ForeignKey(nameof(Article))]
        public Guid AritcleId { get; set; }
        public Article Article { get; set; }
    }
}
