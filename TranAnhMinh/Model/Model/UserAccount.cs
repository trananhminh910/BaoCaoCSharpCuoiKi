namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        public int ID { get; set; }

        //[Required(ErrorMessage = "Bắt buộc phải nhập!")]
        //[StringLength(100, ErrorMessage = "Tên tài khoản phải ít hơn 100 ký tự.")]
        [DisplayName("Tài Khoản")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Bắt buộc phải nhập!")]
        //[StringLength(100, ErrorMessage = "Mật khẩu phải ít hơn 100 ký tự.")]
        [DisplayName("Mật Khẩu")]   
        public string Password { get; set; }

        [DisplayName("Trạng Thái")]
        public bool Status { get; set; }
        
    }
}
