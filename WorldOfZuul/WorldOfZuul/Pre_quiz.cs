using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGarden
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public char CorrectAnswer { get; set; }
        public string Explanation { get; set; }

        public Question(string text, List<string> options, char correctAnswer, string explanation)
        {
            Text = text;
            Options = options;
            CorrectAnswer = correctAnswer;
            Explanation = explanation;
        }

        public static class QuestionBank
        {
            public static List<Question> Questions = new List<Question>
            {
                //BASIC QUESTIONS
                new ("How many people already live in cities today?", new List<string> { "A) more than a 25% of the world population", "B) more than a 33% of the world population", "C) more than the 50% of the world population" }, 'C', "Currently, more than 50 percent of the 7.7 billion people worldwide live in cities - around 4.2 billion. By 2050 this proportion is expected to rise to 68 percent. There are currently 28 megacities worldwide, each with more than 10 million inhabitants."),
                new ("Especially in cities it is getting hotter and hotter as a result of climate change. Which measure does NOT help to cool down?", new List<string> { "A) Greening roofs", "B) Paint streets white", "C) Abandonment of pesticides on municipal grounds", }, 'C', "To cool urban space, urban planners have been implementing various construction measures for many years. Bright roofs and streets help to keep temperatures down. On the other hand, although the absence of pesticides helps animals, insects and plants living on green spaces, it does not contribute to cooling in cities."),
                new ("At a noise level of 65 decibels or higher, continuous exposure poses a risk to health. How much noise does traffic on a busy road cause?", new List<string> { "A) 70 decibels", "B) 80 decibel", "C) 100 decibel", }, 'B', "Constant exposure to noise at 65 decibels or higher can lead to physical and mental health problems. The traffic on a busy road causes a noise level of around 80 decibels. The music in a disco or at a rock concert reaches up to 110 decibels, an aircraft engine around 120 decibels."),
                new ("How do trees react to great heat?", new List<string> { "A) They sweat and give off cooling water vapour", "B) They discolour their leaves already in summer", "C) They increase their CO2 absorption", }, 'A', "Did you know? When it is very hot, even trees can sweat to survive. They release water through the pores in their leaves. However, the more they sweat, the more they stop absorbing CO2."),
                new ("What is the fastest mode of transport in the city for distances of up to five kilometres?", new List<string> { "A) Bicycle", "B) Bus", "C) Car", }, 'A', "In a city, the fastest way to get around is usually by bicycle. The Traffic Club Germany (VCD) has calculated a comparison for Berlin: For example, it takes almost a quarter of an hour to cover four kilometres by bike, 26 minutes by bus and 23 minutes by car."),
                new ("What must be taken into account in sustainable building?", new List<string> { "A) Ecological and regional building materials", "B) Digital equipment", "C) Large garden", }, 'A', "Sustainable building includes everything from construction to the actual use and later deconstruction. The use of ecological and regional building materials is an important aspect of this."),
                //ADVANCED QUESTIONS
                new ("Which transport measure does NOT increase sustainability?", new List<string> { "A) The establishment of car-free zones", "B) The rental of electric scooters per app", "C) The development of cycle paths", }, 'B', "Contrary to the promises made by the manufacturers, electric scooters do not reduce traffic, because users do not use them instead of the conventional means of transport, but in addition. Since they also have to be collected and charged every night, their ecological balance is poor."),
                new ("What is Copenhagen's target for 2025? By then the Danish capital wants to...", new List<string> { "A) be CO2 neutral", "B) have completely eliminated the waste of food", "C) recycle almost 100 percent of the plastic waste produced", }, 'A', "Copenhagen has set itself the goal of becoming the world's first climate-neutral capital by 2025. Since 2009, work has been underway to make the city greener and more CO2-neutral. The most important areas are energy production, energy consumption and green mobility."),
                new ("How many degrees can tree-covered parks cool the air in the surrounding area?", new List<string> { "A) by one degree", "B) by three degrees", "C) by five degrees", }, 'C', "The tree population in urban parks can reduce the temperature of the surrounding area by up to five degrees. A large part of the solar energy is converted by the green leaves through photosynthesis. During this process, moisture is released into the environment through the breathing of the leaves and thus cooled."),
                new ("Almost 100 major international cities have joined together in the C40 networt, with which aim?", new List<string> { "A) They want to drive forward climate protection measures", "B) They want to make the metropolises energy self-sufficient", "C) They provide information on the risks of climate change", }, 'A', "The major cities have joined forces in the C40 network to take appropriate measures to improve climate protection. Cities play an important role in climate protection, since they consume a quarter of the world's energy requirements and are responsible for 80 percent of greenhouse gas emissions."),
                new ("Which UN organisation aims to promote sustainable cities?", new List<string> { "A) UNEP", "B) UN-HABITAT", "C) UNESCO", }, 'B', "The organisation UN-Habitat, based in Nairobi (Kenya), is currently working to promote sustainable urban development."),
                //EXPERT QUESTIONS
                new ("Air pollution - such as particulate matter, sulphur dioxide and nitrogen oxides - shortens the life of Europeans...", new List<string> { "A) by half a year", "B) by two years", "C) by five years", }, 'B', "Air pollution shortens the life of a European on average by about two years. Air polluted by particulate matter and other pollutants is dangerous to health and increases the risk of respiratory and cardiovascular diseases."),
                new ("What does NOT help cities to strengthen their resilience?", new List<string> { "A) Urban gardening", "B) Urban mining", "C) Urban expanding", }, 'C', "Resilience of cities is understood as their resistance to challenging events such as floods, heat waves, sea level rise. 'Urban expansion', on the other hand, describes the constant growth of cities and megacities. Since their expansion is mostly at the expense of agricultural land, it does not contribute to resilience, but increases food insecurity."),
                new ("In 2050, around 22 percent of the world's population will live in megacities. How many of them are located in coastal areas that are considered risk areas due to rising sea levels?", new List<string> { "A) Less than 15 percent", "B) Around 30 percent", "C) More than 62 percent", }, 'C', "Worldwide urbanisation will continue to increase until 2050, generating a particularly rapid growth of global megacities. More than 62% of these metropolises are located in risk areas on the coast, for example Tokyo, New York, Jakarta, Bangkok."),
                new ("Many millions of people worldwide live in slums. In which country is it more than 70 percent of the population?", new List<string> { "A) Haiti", "B) India", "C) Pakistan", }, 'A', "The Caribbean state of Haiti with its 10.8 million people is considered the poorest country in the Western Hemisphere. Around 70 percent of its inhabitants live in slums and earn less than two US dollars a day, which is below the national poverty line. It is currently estimated that more than 900 million people worldwide live in slums."),
            };
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
        
        public void StartPreQuiz()
        {
            int score1 = 0;

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

            Game.DisplayTextSlowly($"You completed the quiz! Your final score is {score1}/{Question.QuestionBank.Questions.Count}.");
            Game.DisplayTextSlowly("\nThis score will be used in the future to evaluate your evolution in sustanability knowledge :)\n");
        }
    }
}