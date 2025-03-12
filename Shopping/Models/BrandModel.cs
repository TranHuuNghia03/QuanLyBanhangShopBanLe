using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class BrandModel
    {
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Hãy nhập tên Thương hiệu")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Hãy nhập mô tả Thương hiệu")]
		public string Description { get; set; }
		public string Slug { get; set; }
		public int Status { get; set; }
	}
}
