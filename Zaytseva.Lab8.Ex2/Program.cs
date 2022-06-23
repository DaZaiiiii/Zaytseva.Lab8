using System.Text;
using System.Text.Encodings;
using System.Text.Encodings.Web;
using System.Text.Json;

var path = "table.csv";

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

Encoding encoding = Encoding.GetEncoding(1251);


var lines = File.ReadAllLines(path, encoding);
var Arts = new Art[lines.Length - 1];
for (var i = 1; i < lines.Length; i++)
{
    var splits = lines[i].Split(';');
    var art = new Art();
    art.Тип = splits[0];
    art.Общиеиздержки = Convert.ToInt32(splits[1]);
    art.Цена = Convert.ToInt32(splits[2]);
    art.Время = Convert.ToInt32(splits[3]);
    Arts[i - 1] = art;
    Console.WriteLine(art);
}

var result = "result.csv";
using (StreamWriter streamWriter = new StreamWriter(result, false, encoding))
{
    streamWriter.WriteLine($"Type;FC;Price;Time;Profit");
    for (int i = 0; i < Arts.Length; i++)
    {
        streamWriter.WriteLine(Arts[i].ToExcel());
    }
}

var jsonOptions = new JsonSerializerOptions()
{
    Encoder = JavaScriptEncoder.Default
};

var json = JsonSerializer.Serialize(Arts, jsonOptions);
File.WriteAllText("result.json", json);

var stringJson = File.ReadAllText("result.json");
var array = JsonSerializer.Deserialize<Art[]>(stringJson);

public class Art
{
    public string Тип { get; set; }
    public int Общиеиздержки { get; set; }
    public int Цена { get; set; }
    public int Время { get; set; }
    public double Доход { get => (Цена * Время) - Общиеиздержки; }
    public override string ToString()
    {
        return $"Тип арта:{Тип} Общие издержки: {Общиеиздержки} Цена: {Цена} Время: {Время} Доход:{Доход}";
    }

    public string ToExcel()
    {
        return $"{Тип};{Общиеиздержки};{Цена};{Время};{Доход}";
    }
}
