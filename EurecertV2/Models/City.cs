﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EurecertV2.Models
{
    public class City
    {
        public int Id { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Şehir")]
        public string Name { get; set; }
        [Display(Name = "Ülke")]
        public int CountryId { get; set; }
        [Display(Name = "Ülke")]
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}
