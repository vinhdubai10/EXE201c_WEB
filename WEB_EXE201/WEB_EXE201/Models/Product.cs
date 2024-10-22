namespace WEB_EXE201.Models
{
    public class Product
    {
        public int ProductID { get; set; } // Khóa chính
        public string ProductName { get; set; } // Tên sản phẩm
        public string Description { get; set; } // Mô tả sản phẩm
        public string ImageUrl { get; set; } // Đường dẫn đến ảnh sản phẩm

        // Khóa ngoại liên kết với User
        public int UserID { get; set; }
        public virtual User User { get; set; } // Thông tin tác giả

        // Ngày tạo và ngày chỉnh sửa
        public DateTime DateCreated { get; set; } // Ngày tạo sản phẩm
        public DateTime DateModified { get; set; } // Ngày chỉnh sửa sản phẩm
    }
}
