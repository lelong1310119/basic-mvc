using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities;

public partial class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }

    [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
    [MinLength(5, ErrorMessage = "Tên đăng nhập phải có ít nhất 5 kí tự")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
    [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự")]
    public string Password { get; set; } = null!;
}
