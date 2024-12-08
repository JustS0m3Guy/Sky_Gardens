namespace SkyGarden;

using System;
using System.Collections.Generic;

class Question
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
            new Question("What is the capital of France?", new List<string> { "A) Paris", "B) Madrid", "C) Rome" }, 'A'),
            new Question("Which planet is known as the Red Planet?", new List<string> { "A) Venus", "B) Mars", "C) Jupiter", }, 'B'),
            new Question("What is the largest mammal?", new List<string> { "A) Elephant", "B) Blue Whale", "C) Giraffe", }, 'B'),
            new Question("Who wrote 'Hamlet'?", new List<string> { "A) Charles Dickens", "B) William Shakespeare", "C) Mark Twain", }, 'B'),
            new Question("What is the chemical symbol for water?", new List<string> { "A) H2O", "B) CO2", "C) O2", }, 'A')
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
        
        Console.WriteLine($"You completed the quiz! Your final score is {score1}/{questions.Count}.");
    }
}
