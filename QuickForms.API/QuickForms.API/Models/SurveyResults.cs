using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Models;
public class SurveyResults
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ResultId { get; set; }

    [BsonElement("surveyId")]
    public string SurveyId { get; set; }

    [BsonElement("surveyResult")]
    public BsonDocument? SurveyResult { get; set; }
}
