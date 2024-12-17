using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGarden;

public class PostQuiz
{
    private List<Question> questions;

    public PostQuiz()
    {
        questions = new List<Question>
        {
            new Question("How many people already live in cities today?", new List<string> { "A) more than a 25% of the world population", "B) more than a 33% of the world population", "C) more than the 50% of the world population" }, 'C'),
            new Question("Especially in cities it is getting hotter and hotter as a result of climate change. Which measure does NOT help to cool down?", new List<string> { "A) Venus", "B) Mars", "C) Jupiter", }, 'B'),
            new Question("What is the largest mammal?", new List<string> { "A) Elephant", "B) Blue Whale", "C) Giraffe", }, 'B'),
            new Question("Who wrote 'Hamlet'?", new List<string> { "A) Charles Dickens", "B) William Shakespeare", "C) Mark Twain", }, 'B'),
            new Question("What is the chemical symbol for water?", new List<string> { "A) H2O", "B) CO2", "C) O2", }, 'A')
        };
    }

    public void StartPostQuiz()
    {
        int score2 = 0;

        foreach (var question in questions)
        {
            while (true) // Repeat until valid input
            {
                question.Display();
                Console.Write("Enter your answer (A, B or C): ");
                char userAnswer = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine("\n");

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
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (A, B, C, or D).\n");
                }
            }
        }
        
        Console.WriteLine($"You completed the quiz! Your final score is {score2}/{questions.Count}.");
    }
}
