using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMPS.UI.Models
{
    public class CodeModel
   {
        public class QBRatingModel
        {
            public double QBR { get; set; }

            public string league { get; set; }
            [Display(Name = "Passing Attempts")]
            public double att { get; set; }
            [Display(Name = "Pass Completions")]
            public double comp { get; set; }
            [Display(Name = "Passing Yards")]
            public double yds { get; set; }
            [Display(Name = "Passing Touchdowns")]
            public double td { get; set; }
            [Display(Name = "Interceptions")]
            public double inter { get; set; }
        }

        
        public class Question
        {

            public int ID { get; set; }
            public string question { get; set; }
            public List<Answer> Answers { get; set; }
            public int SelectedAnswer { get; set; }
            public Question()
            {
                Answers = new List<Answer>();

            }

        }
        public class Answer
        {
            public int ID { get; set; }
            public string answer { get; set;  }
        }

        public class Evaluation
        {
            public List<Question> Questions { set; get; }
            public Evaluation()
            {
                Questions = new List<Question>();
            }
        }
    }
}