﻿using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class LoaiVM
    {
        public int MaLoai { get; set; }
        public string? TenLoai { set; get; }
    }
}
