﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.RolesDto
{
    public class CreateRolesDto
    {
        [Required(ErrorMessage = "Role Name is required")]
        public string RolesName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
