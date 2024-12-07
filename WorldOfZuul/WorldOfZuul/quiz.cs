namespace SkyGarden;

class Quiz
{
    private List<Question> questions;

    public Quiz()
    {
        questions = new List<Question>
        {
            new Question("What is the capital of France?", new List<string> { "A) Paris", "B) Madrid", "C) Rome", "D) Berlin" }, 'A'),
            new Question("Which planet is known as the Red Planet?", new List<string> { "A) Venus", "B) Mars", "C) Jupiter", "D) Saturn" }, 'B'),
            new Question("What is the largest mammal?", new List<string> { "A) Elephant", "B) Blue Whale", "C) Giraffe", "D) Hippopotamus" }, 'B'),
            new Question("Who wrote 'Hamlet'?", new List<string> { "A) Charles Dickens", "B) William Shakespeare", "C) Mark Twain", "D) Jane Austen" }, 'B'),
            new Question("What is the chemical symbol for water?", new List<string> { "A) H2O", "B) CO2", "C) O2", "D) N2" }, 'A')
        };
    }
}

public StartQuiz()
{
    int score = 0;

    foreach (var question in questions)
    {
        bool answeredCorrectly = false;

        while (!answeredCorrectly)
        {
            question.Display();
            Console.Write("Enter your answer (A, B, C, or D): ");
            char userAnswer = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            switch (userAnswer)
            {
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                    if (question.CheckAnswer(userAnswer))
                    {
                          Console.WriteLine("Correct!\n");
                        score++;
                        answeredCorrectly = true;
                    }
                    else
                    {
                        Console.WriteLine($"Wrong! The correct answer was {question.CorrectAnswer}. Try again!\n");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter A, B, C, or D.\n");
                    break;
            }
        }
    }

    Console.WriteLine($"You completed the quiz! Your final score is {score}/{questions.Count}.");
}

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

    public bool CheckAnswer(char answer)
    {
        return answer == CorrectAnswer;
    }
};
