using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace QuizBox.Model
{
    public class Root
    {
        [JsonPropertyName("quiz")]
        public List<Quiz> Quiz { get; set; }
    }

    public class Quiz
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("questions")]
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        [JsonPropertyName("questionID")]
        public string QuestionID { get; set; }

        [JsonPropertyName("question")]
        public string QuestionText { get; set; }

        [JsonPropertyName("answers")]
        public List<Answer> Answers { get; set; }

    }

    public class Answer
    {
        [JsonPropertyName("answer")]
        public string AnswerText { get; set; }

        [JsonPropertyName("isCorrect")]
        public bool IsCorrect { get; set; }
    }
}