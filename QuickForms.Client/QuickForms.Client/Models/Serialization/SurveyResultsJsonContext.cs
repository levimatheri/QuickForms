using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuickForms.Client.Models.Serialization;
[JsonSerializable(typeof(SurveyResults))]
[JsonSerializable(typeof(List<SurveyResults>))]
[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class SurveyResultsJsonContext : JsonSerializerContext
{
}
