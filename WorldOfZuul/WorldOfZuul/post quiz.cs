namespace SkyGarden;

public class PostQuiz
{
    private List<Question> questions;

    public PostQuiz()
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
                Console.WriteLine();

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
