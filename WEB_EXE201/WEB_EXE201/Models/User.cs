namespace WEB_EXE201.Models
{
    public class User
    {
        public int UserID { get; set; } // Khóa chính
        public string Username { get; set; } // Tên người dùng
        public string Password { get; set; } // Mật khẩu
        public string Email { get; set; } // Địa chỉ email

        // Liên kết với sản phẩm
        public virtual ICollection<Product> Products { get; set; }
    }
}
