using System;

class Program
{static void Main(string[] args)
    {

    static string GetDiagnosis(int score, int count, string[] results)
    {
        int index = score * (results.Length - 1) / count;
        return results[index];
    }

    static void Main()
    {
        Console.Write("Как вас зовут? ");
        string name = Console.ReadLine();

        string[] questions =
        {
            "Сколько будет 2 + 2 * 2?",
            "Бревно нужно распилить на 10 частей. Сколько надо сделать распилов?",
            "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
            "Укол делают каждые полчаса. Сколько минут нужно для трех уколов, если первый делают сразу?",
            "5 свечей горело, 2 потухли. Сколько свечей останется, когда остальные полностью сгорят?",
        };

        int[] correctAnswers = { 6, 9, 25, 60, 2 };

        string[] results =
        {
            "Идиот. 🙃",
            "Кретин. 😅",
            "Дурак. 🤔",
            "Нормальный. 👍",
            "Талант! 🌟",
            "Гений! 🧠"
        };

        Random random = new Random();
        string repeat;

        do
        {
            int score = 0;
            int[] userAnswers = new int[questions.Length];

            for (int i = questions.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                string tempQuestion = questions[i];
                questions[i] = questions[j];
                questions[j] = tempQuestion;

                int tempAnswer = correctAnswers[i];
                correctAnswers[i] = correctAnswers[j];
                correctAnswers[j] = tempAnswer;
            }

            Console.WriteLine();
            Console.WriteLine(name + ", ответьте на " + questions.Length + " вопросов.");
            Console.WriteLine("Вводите ответы числами.");

            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine((i + 1) + ". " + questions[i]);

                Console.Write("Ваш ответ: ");

                while (!int.TryParse(Console.ReadLine(), out userAnswers[i]))
                {
                    Console.WriteLine("Пожалуйста, введите число!");
                    Console.Write("Ваш ответ: ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Результаты:");

            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine((i + 1) + ". " + questions[i]);
                Console.WriteLine("Ваш ответ: " + userAnswers[i]);
                Console.WriteLine("Правильный ответ: " + correctAnswers[i]);

                if (userAnswers[i] == correctAnswers[i])
                {
                    Console.WriteLine("Правильно!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Неправильно!");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Правильных ответов: " + score + " из " + questions.Length);

            string diagnosis = GetDiagnosis(score, questions.Length, results);

            Console.WriteLine(name + ", ваш результат: " + diagnosis);

            Console.WriteLine();
            Console.Write("Хотите пройти тест снова? (да/нет): ");
            repeat = Console.ReadLine().Trim().ToLower();

        } while (repeat == "да");

        Console.WriteLine("До свидания, " + name + "!");
    }
}