using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBox.Model
{
    public class AnsweredQuestion
    {
        public Question Question { get; set; }
        public Answer SelectedAnswer { get; set; }
    }
}

