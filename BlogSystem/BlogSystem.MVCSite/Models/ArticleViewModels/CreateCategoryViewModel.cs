using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModels
{
    public class CreateCategoryViewModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        [StringLength(200,MinimumLength = 2)]
        public string CategoryName { get; set; }
    }
}