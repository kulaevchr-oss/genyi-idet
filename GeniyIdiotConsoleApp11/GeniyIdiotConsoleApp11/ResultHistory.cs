using System;
using System.Collections.Generic;
using System.IO;

static class ResultHistory
{
    static readonly string folder = Path.Combine(
        Environment.GetEnvironmentVariable("LOCALAPPDATA")
            ?? Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "GeniyIdiotConsoleApp11");
    static readonly string filePath = Path.Combine(folder, "results.tsv");

    public static void Save(string name, int score, string diagnosis)
    {
        try
        {
            Directory.CreateDirectory(folder);
            File.AppendAllText(filePath, $"{name}\t{score}\t{diagnosis}{Environment.NewLine}");
        }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException)
        {
            Console.WriteLine("Не удалось сохранить результат: " + error.Message);
        }
    }

    public static void Show()
    {
        const string nameHeader = "ФИО";
        const string scoreHeader = "Кол-во правильных ответов";
        const string diagnosisHeader = "Диагноз";
        int nameWidth = nameHeader.Length;
        var rows = new List<string[]>();
        int skipped = 0;

        try
        {
            foreach (string line in File.ReadLines(filePath))
            {
                string[] fields = line.Split('\t');
                if (fields.Length != 3 || !int.TryParse(fields[1], out _))
                {
                    skipped++;
                    continue;
                }

                rows.Add(fields);
                nameWidth = Math.Max(nameWidth, fields[0].Length);
            }
        }
        catch (Exception error) when (error is FileNotFoundException or DirectoryNotFoundException)
        {
            Console.WriteLine("Сохранённых результатов пока нет.");
            return;
        }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException)
        {
            Console.WriteLine("Не удалось прочитать результаты: " + error.Message);
            return;
        }

        if (skipped > 0)
            Console.WriteLine("Пропущено повреждённых строк: " + skipped);

        if (rows.Count == 0)
        {
            Console.WriteLine("Сохранённых результатов пока нет.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"{nameHeader.PadRight(nameWidth)} | {scoreHeader} | {diagnosisHeader}");
        foreach (string[] row in rows)
        {
            Console.WriteLine($"{row[0].PadRight(nameWidth)} | {row[1].PadLeft(scoreHeader.Length)} | {row[2]}");
        }
    }
}
