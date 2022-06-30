﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CORE.DAL
{
    [Index(nameof(Degree), Name = "UQ__Grade__C831FD881056A0A5", IsUnique = true)]
    public partial class Grade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int Degree { get; set; }
    }
}