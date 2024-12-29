using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGarden;

public class PostQuiz
{
    public void StartPostQuiz()
    {
        int score2 = 0;

        foreach (var question in Question.QuestionBank.Questions)
        {
            while (true) // Repeat until valid input
            {
                question.Display();
                Console.Write("Enter your answer (A, B or C): ");
                char userAnswer = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine("\n");

                if (question.IsValidAnswer(userAnswer))
                {
                    if (question.IsValidAnswer(userAnswer))
                    {
                        if (question.CheckAnswer(userAnswer))
                        {
                            Console.WriteLine("Correct!\n");
                            score2++;
                        }
                        else
                        {
                            Console.WriteLine($"\nWrong! The correct answer was {question.CorrectAnswer}.\n");
                        }

                        if (!string.IsNullOrEmpty(question.Explanation))
                        {
                            Console.WriteLine($"Explanation: {question.Explanation}\n");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid option (A, B, C, or D).\n");
                    }
                }
            }
        }
        
        Game.DisplayTextSlowly($"You completed the quiz! Your final score is {score2}/{Question.QuestionBank.Questions.Count}.");
        Game.DisplayTextSlowly("\n");
    }
}