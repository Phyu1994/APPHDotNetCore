// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string jsonStr = await File.ReadAllTextAsync("Data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);

string DreamjsonStr = await File.ReadAllTextAsync("DreamDictionary.json");
var modelStr = JsonConvert.DeserializeObject<DreamDto>(DreamjsonStr);



foreach (var question in model.questions)
{
    Console.WriteLine(question.questionNo);
}
foreach (var blogheader in modelStr.BlogHeader)
{
    Console.WriteLine(blogheader.BlogTitle);
}

Console.ReadLine();

public class MainDto
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}

public class DreamDto
{
    public Blogheader[] BlogHeader { get; set; }
    public Blogdetail[] BlogDetail { get; set; }
}

public class Blogheader
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
}

public class Blogdetail
{
    public int BlogDetailId { get; set; }
    public int BlogId { get; set; }
    public string BlogContent { get; set; }
}