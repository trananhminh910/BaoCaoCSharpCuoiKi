namespace Model.Model
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Product")]
    public partial class Product
    {
        public int ID { get; set; }

        [DisplayName("Tên Sản Phẩm")]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Số Lượng")]
        public int? Quantity { get; set; }

        [DisplayName("Đơn Giá")]
        public int? Price { get; set; }

        [DisplayName("Hình Ảnh")]
        [StringLength(200)]
        public string Image { get; set; }

        [DisplayName("Mô Tả")]
        [StringLength(200)]
        public string Description { get; set; }

        [DisplayName("Trạng Thái")]
        public bool Status { get; set; }

        [DisplayName("Danh Mục")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        
    }
}
