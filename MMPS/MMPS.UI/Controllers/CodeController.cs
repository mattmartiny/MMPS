﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMPS.UI.Models;

namespace MMPS.UI.Controllers
{
    public class CodeController : Controller
    {
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }




        public ActionResult QBRating(CodeModel.QBRatingModel model)
        {
            if (model.league == "nfl")
            {


                double a = ((model.comp / model.att) - 0.3) * 5; //part 1 of formula
                double b = ((model.yds / model.att - 3) * 0.25); //part 2 of formula
                double c = ((model.td / model.att) * 20);// part 3 of formula
                double d = 2.375 - ((model.inter / model.att) * 25); //part 4 of formula

                /*
                 * NFL QBR requires the result of any calculation to be non-negative and no more than 2.375
                 * It also states, if it is a negative number, set it to 0
                 * or if it's greater than 2.375, set it to 2.375
                 */


                a = Math.Max(a, 0);
                b = Math.Max(b, 0);
                c = Math.Max(c, 0);
                d = Math.Max(d, 0);

                a = Math.Min(a, 2.375);
                b = Math.Min(b, 2.375);
                c = Math.Min(c, 2.375);
                d = Math.Min(d, 2.375);


                var rating = (((a + b + c + d) / 6) * 100);

                model.QBR = Math.Round(rating, 1);

                var result = model.QBR.ToString();
                double num = -1;

                if (!double.TryParse(result, out num))
                {
                    ViewBag.result = $"The NFL QB Rating is: ";
                }
                else {

                    ViewBag.result = $"The NFL QB Rating is: {result}";
                }


            }
            else
            {
                //The NCAA doesn't have a minimum or maximum limit, 


                var rating = (((8.4 * model.yds) + (330 * model.td) + (100 * model.comp) - (200 * model.inter)) / model.att);

                model.QBR = Math.Round(rating, 1);


                var result = model.QBR.ToString();
                double num = -1;

                if (!double.TryParse(result, out num))
                {

                    ViewBag.result = "The NCAA QB Rating is:";



                }
                else {


                    ViewBag.result = $"The NCAA QB Rating is: {result}";

                }

            }

            return View();

        }



        public ActionResult MWQuiz()
        {
            var evalVM = new CodeModel.Evaluation();





            var q1 = new CodeModel.Question { ID = 1, question = "What is your best asset on the field?" };
            q1.Answers.Add(new CodeModel.Answer { ID = 10, answer = "Power Hitting" });
            q1.Answers.Add(new CodeModel.Answer { ID = 20, answer = "Fielding and Strong Throwing arm" });
            q1.Answers.Add(new CodeModel.Answer { ID = 30, answer = "Ability to get on base" });
            q1.Answers.Add(new CodeModel.Answer { ID = 40, answer = "Leadership and Balanced Play" });
            evalVM.Questions.Add(q1);

            var q2 = new CodeModel.Question { ID = 2, question = "What is your worst asset in softball?" };
            q2.Answers.Add(new CodeModel.Answer { ID = 11, answer = "Cockiness/Arrogance" });
            q2.Answers.Add(new CodeModel.Answer { ID = 21, answer = "Injury Prone" });
            q2.Answers.Add(new CodeModel.Answer { ID = 31, answer = "Lack of Speed, Foul out tendency" });
            q2.Answers.Add(new CodeModel.Answer { ID = 41, answer = "Lack of speed/Inconsistency" });
            evalVM.Questions.Add(q2);

            var q3 = new CodeModel.Question { ID = 3, question = "Who is your favorite college sports team?" };
            q3.Answers.Add(new CodeModel.Answer { ID = 12, answer = "Florida Gators" });
            q3.Answers.Add(new CodeModel.Answer { ID = 22,  answer = "Kansas Jayhawks" });
            q3.Answers.Add(new CodeModel.Answer { ID = 32, answer = "Kansas State" });
            q3.Answers.Add(new CodeModel.Answer { ID = 42, answer = "Missouri Tigers" });
            evalVM.Questions.Add(q3);


            var q4 = new CodeModel.Question { ID = 4, question = "What is your position on the field?" };
            q4.Answers.Add(new CodeModel.Answer { ID = 13, answer = "Corner Outfield/Catcher" });
            q4.Answers.Add(new CodeModel.Answer { ID = 23, answer = "Utility/Anywhere" });
            q4.Answers.Add(new CodeModel.Answer { ID = 33, answer = "1st Base/Former Pitcher" });
            q4.Answers.Add(new CodeModel.Answer { ID = 43, answer = "Right Side of the Infield/Catcher" });
            evalVM.Questions.Add(q4);

            var q5 = new CodeModel.Question { ID = 5, question = "Favorite number of these?" };
            q5.Answers.Add(new CodeModel.Answer { ID = 14, answer = "24" });
            q5.Answers.Add(new CodeModel.Answer { ID = 24, answer = "21" });
            q5.Answers.Add(new CodeModel.Answer { ID = 34, answer = "52" });
            q5.Answers.Add(new CodeModel.Answer { ID = 44, answer = "12" });
            evalVM.Questions.Add(q5);
     
            var i = evalVM.Questions;


         



           TempData["data"] = evalVM;

            return View(evalVM);
        }


        [HttpPost]
        public ActionResult MWQuiz(CodeModel.Evaluation model)
        {
          List<int> p = (from x in model.Questions
                     select x.SelectedAnswer).ToList();
            //Get a list of the select answer Ids 
            
            TempData["data3"] = p;

            TempData["data2"] = (CodeModel.Evaluation)TempData["data"];
            
            return RedirectToAction("_QuizResult");


        }


        //[HttpPost]
        public ActionResult _QuizResult(CodeModel.Evaluation model)
        {

            CodeModel.Evaluation evalVM = (CodeModel.Evaluation)TempData["data2"]; 
            //Get the data for all of the Information

            List<int> sa = (List<int>)TempData["data3"];  
            //Get the data for the selected answer ID


            var p = (from x in evalVM.Questions
                  select x.Answers).SelectMany(x => x).ToList(); 
            //Select all the possible answers individually

            var Ans = (from x in p
                       select x.ID).ToList();  
            //Select all of the answer ID's and put them in a list
                   
            List<int> selectedAnswer = Ans.Intersect(sa).ToList(); 
            //Find out the selected answers by comparing the two lists


            List<string> results = selectedAnswer.ConvertAll(x => x.ToString()); 
            //convert all the selected answers to strings
                                                                    
           int results1 = results.Where(x => x.StartsWith("1")).Count(); 

           int results2 = results.Where(x => x.StartsWith("2")).Count();

           int results3 = results.Where(x => x.StartsWith("3")).Count();

           int results4 = results.Where(x => x.StartsWith("4")).Count();
           //Counts the selected answers whose Id's start with a certain number

           int pl = new[] { results1, results2, results3, results4 }.Max(); 
           //Find out which Count has the most

                if (pl == results1)
                {
                    var result = "You're Ryan Sufflebean. You hit the ball hard.";
                    ViewBag.Result = result;
                    return View();
                }

                if (pl == results2)
                {
                    var result = "You're Corey Moyer. You hustle no matter what.";
                    ViewBag.Result = result;
                    return View();
                }

                if (pl == results3)
                {
                    var result = "You're Ben Hohly, you hit back side like no ones business";
                    ViewBag.Result = result;
                    return View();
                }
                if (pl == results4)
                {
                    var result = "You're Matt Martiny. You have many intangibles";
                    ViewBag.Result = result;
                    return View();
                }

                else
                {
                    var result = "You could be anyone of 4 people! Take the quiz again!";
                    ViewBag.Result = result;
                    return View();
                }
            //Returns the Asnwer of which MW Shock player you are With Text!
        }
     


}

}