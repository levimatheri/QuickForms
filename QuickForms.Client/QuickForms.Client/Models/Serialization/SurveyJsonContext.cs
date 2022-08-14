using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuickForms.Client.Models.Serialization;

[JsonSerializable(typeof(Survey))]
[JsonSerializable(typeof(List<Survey>))]
[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class SurveyJsonContext : JsonSerializerContext
{
}
