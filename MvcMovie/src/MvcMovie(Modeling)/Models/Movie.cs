﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie_Modeling_.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength =3)]
        public string Title { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name ="Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Genre { get; set; }
        [Range(1,100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(5)]
        public string Rating { get; set; }
    }
}
