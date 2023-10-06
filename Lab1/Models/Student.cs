using System.ComponentModel.DataAnnotations;

namespace TH01.Models
{
    public class Student
    {
        public int Id { get; set; }//Mã sinh viên
        [Required(ErrorMessage = "Ten bắt buộc phải được nhập")]

        [RegularExpression(@"^[A-Za-z\s]{4,100}$")]
        public string? Name { get; set; } //Họ tên
        [RegularExpression(@"^(?:[0-9](?:\.[0-9])?|10(?:\.0)?)$")]
        [Required(ErrorMessage ="bat buoc phai nhap diem")] 
        public string? Diem { get; set; }//Diem

        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$")]
        public string? Email { get; set; } //Email
        [StringLength(100, MinimumLength = 8)]
        [Required(ErrorMessage = "Mk bắt buộc phải được nhập")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$")]
        public string? Password { get; set; }//Mật khẩu
        [Required(ErrorMessage = "Nganh bắt buộc phải được nhập")]
        public Branch? Branch { get; set; }//Ngành học
        [Required(ErrorMessage = "GT bắt buộc phải được nhập")]
        public Gender? Gender { get; set; }//Giới tính
        [Required(ErrorMessage = "He bắt buộc phải được nhập")]
        public bool IsRegular { get; set; }//Hệ: true-chính qui, false-phi cq
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Dia chi bắt buộc phải được nhập")]
        public string? Address { get; set; }//Địa chỉ

        [Range(typeof(DateTime), "1/1/1963", "12/31/2005")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngay nhap bắt buộc phải được nhập")]
        public DateTime DateOfBorth { get; set; }//Ngày sinh
        public IFormFile formFile { get; set; }
    }
}
