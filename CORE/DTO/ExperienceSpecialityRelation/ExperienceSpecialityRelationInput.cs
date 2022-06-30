﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
   public class ExperienceSpecialityRelationInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Name { get; set; }
    }
}
