﻿using Shopping.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models
{
    public class ProductModel
    {

		[Key]
		public int Id { get; set; }
		[Required, MinLength(4, ErrorMessage = "Hãy nhập tên Sản phẩm")]
		public string Name { get; set; }
		public string Slug { get; set; }
		[Required, MinLength(4, ErrorMessage = "Hãy nhập mô tả sản phẩm")]
		public string Description { get; set; }
		[Required(ErrorMessage = "Hãy nhập giá sản phẩm")]
		[Range(0.01, double.MaxValue)]
		[Column(TypeName = "Decimal(8, 2)")]
		public Decimal Price { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Hãy chọn một thương hiệu")]
        public int BrandId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Hãy chọn một danh mục")]
        public int CategoryId { get; set; }

        public BrandModel Brand { get; set; }
        public CategoryModel Category { get; set; }

		public string Image { get; set; }
		[NotMapped]
		[FileExtension]
		public IFormFile? ImageUpload {  get; set; }

	}
}
