using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Models
{
    public class Posts
    {
        [Required]
        public int PostId { get; set; }
        [Required  ]
        [MaxLength(50)]
        
        [DisplayName ("Title" )]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Location")]
        public string Location { get; set; }
        [Required]
        [MaxLength(150)]
        [DisplayName("SearchAddress")]
        public string SearchAddress { get; set; }
        [Required]
        [DisplayName("Text")]
        public string Text { get; set; }
        [Required]
        public string ImgUrl { get; set; } = "https://www.freeiconspng.com/thumbs/no-image-icon/no-image-icon-6.png";


        [Required]
        public DateTime Datetime { get; set; } = DateTime.Now;

        //public int UserId { get; set; }

        //public User User { get; set; }


    }
}
