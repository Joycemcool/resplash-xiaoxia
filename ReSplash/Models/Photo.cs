using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReSplash.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }

        [DisplayName("File Name")]
        public string FileName { get; set; } = string.Empty;

        [DisplayName("Published")]
        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        public string Description { get; set; } = string.Empty;

        [DisplayName("Views")]
        public int ImageViews { get; set; }

        [DisplayName("Downloads")]
        public int ImageDownloads { get; set; }

        public string Location { get; set; } = string.Empty;

        public User User { get; set; } = new(); 

        public Category Category { get; set; } = new();

    }
}
