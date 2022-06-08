using System;
using System.Text;
using System.IO;
using System.Linq;

string file1 = @"Text.txt";
string path = @"D:\source\repos\Zaytseva.Lab8\Zaytseva.Lab8.Ex1\bin\Debug\net6.0\Text.txt";
DirectoryInfo dirInfo = new DirectoryInfo(path);
FileInfo info = new FileInfo(file1);
Console.WriteLine($"Абсолютный путь: {info.FullName}");
Console.WriteLine($"Относительный путь: {info.Name}");
Console.WriteLine($"Дата: {info.CreationTime}");
Console.WriteLine($"Размер:{info.Length} байт");

Console.WriteLine();


Console.WriteLine(Path.GetFileNameWithoutExtension(file1));
Console.WriteLine(Environment.CurrentDirectory);

Console.WriteLine();

if (File.Exists(file1))
{
    string text = File.ReadAllText(file1);


    int i = 0;
    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    for (i = 0; i < words.Length; i++)
    {
        i++;
    }

    Console.WriteLine($"Всего слов:{i.ToString()}");

    bool Flag1 = false;
    bool Flag2 = false;
    bool Flag3 = false;

    for (int k = 0; k < text.Length; k++)
    {

        if (text[k] >= 'a' && text[k] <= 'z' || text[k] >= 'A' && text[k] <= 'Z')
        {
            Flag1 = true;
        }
        if (text[k] >= 'а' && text[k] <= 'я' || text[k] >= 'А' && text[k] <= 'Я')
        {
            Flag2 = true;
        }
        if (Char.IsDigit(text[k]))
        {
            Flag3 = true;
        }


    }
    if (Flag3)
    {
        Console.WriteLine("В тексте есть цифры");
    }
    if (Flag2)
    {
        Console.WriteLine("В тексте есть кириллица");
    }
    if (Flag1)
    {
        Console.WriteLine("В тексте есть латиница");
    }

    var stat = words.Distinct()
    .ToDictionary(word => word, word => words.Count(x => x == word))
    .OrderByDescending(x => x.Value);
    Console.WriteLine();

    StringBuilder builder = new StringBuilder();
    foreach (var item in stat)
    {
        Console.WriteLine(item);
        builder.AppendLine($"{item.Key} {item.Value}");
    }

    StreamReader reader = new StreamReader("Text.txt");

    int j = 0;
    while ((text = reader.ReadLine()) != null)
    {
        j++;
    }
    Console.WriteLine("Всего строк:" + j.ToString());


    builder.AppendLine($"{info.FullName}\n{info.Name}\n{info.CreationTime}\n{info.Length} байт\nВсего строк: {j.ToString()}\nВсего слов: {i.ToString()}");

    File.WriteAllText("result1.txt", builder.ToString());

}
else
{
    Console.WriteLine("Такого файла не существует!");
}
