/*
 *  ********** SVU **********
 ********** Barea_27786 **********
 *********** Exhibition Model **********
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace pro2.Models
{
    
    public class Exhibition
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}