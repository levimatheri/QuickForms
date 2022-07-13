﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Models;
public class UpdateSurveyDto
{
    public string? Id { get; set; }

    public string? Name { get; set; } 

    public string? Content { get; set; }
}
