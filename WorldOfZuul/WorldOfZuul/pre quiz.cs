using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul;

public class Question
{
    public string Text { get; set; }
    public List<string> Options { get; set; }
    public char CorrectAnswer { get; set; }

    public Question(string text, List<string> options, char correctAnswer)
    {
        Text = text;
        Options = options;
        CorrectAnswer = correctAnswer;
    }

    public void Display()
    {
        Console.WriteLine(Text);
        foreach (var option in Options)
        {
            Console.WriteLine(option);
        }
    }

    public bool IsValidAnswer(char answer)
    {
        return answer >= 'A' && answer <= 'C';
    }
    
    public bool CheckAnswer(char answer)
    {
        return answer == CorrectAnswer;
    }
}

public class PreQuiz
{
    private List<Question> questions;

    public PreQuiz()
    {
        questions = new List<Question>
        {
            new ("How many people already live in cities today?", new List<string> { "A) more than a 25% of the world population", "B) more than a 33% of the world population", "C) more than the 50% of the world population" }, 'C'),
            new ("Especially in cities it is getting hotter and hotter as a result of climate change. Which measure does NOT help to cool down?", new List<string> { "A) Venus", "B) Mars", "C) Jupiter", }, 'B'),
            new ("What is the largest mammal?", new List<string> { "A) Elephant", "B) Blue Whale", "C) Giraffe", }, 'B'),
            new ("Who wrote 'Hamlet'?", new List<string> { "A) Charles Dickens", "B) William Shakespeare", "C) Mark Twain", }, 'B'),
            new ("What is the chemical symbol for water?", new List<string> { "A) H2O", "B) CO2", "C) O2", }, 'A')
        };
    }

    public void StartPreQuiz()
    {
        int score1 = 0;

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
                        score1++;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (A, B or C).\n");
                }
            }
        }
        
        Game.DisplayTextSlowly($"You completed the quiz! Your final score is {score1}/{questions.Count}.");
        Game.DisplayTextSlowly("\nThis score will be used in the future to evaluate your evolution in sustanability knowledge :)\n");
    }
}
