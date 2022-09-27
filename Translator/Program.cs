using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json.Nodes;
using System;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Globalization;

string apiKey = System.IO.File.ReadAllText(@"..\..\..\apiKey.txt");
Console.WriteLine("Enter Your text in any language:");
string textInput = Console.ReadLine();
Console.WriteLine("Enter the language to translate to: (eg. EN)");
string langInput = Console.ReadLine();



HttpClient client = new HttpClient();

client.DefaultRequestHeaders.Add("Authorization", apiKey);


var values = new[]
{
    new KeyValuePair<string, string>("text", textInput),
    new KeyValuePair<string, string>("target_lang", langInput),
};

HttpResponseMessage response = await client.PostAsync(
    "https://api-free.deepl.com/v2/translate", new FormUrlEncodedContent(values));
//response.EnsureSuccessStatusCode();

string responseString = await response.Content.ReadAsStringAsync();

//JsonSerializerOptions options = new();
// get json string from file or http response
var summary = JsonSerializer.Deserialize<Recorder>(responseString);

// now, use json data like a normal c# object
//string summaryText = summary[0];
Console.WriteLine("Comes from language: " + summary.translations[0].detected_source_language);

Console.WriteLine("Text in " + langInput + " : " + summary.translations[0].text);


public class Translated
{
    public string detected_source_language { get; set; }
    public string text { get; set; }
}

public class Recorder
{
    public Translated[] translations { get; set; }
}
/*
 * {
 *   tanslations: [
 *    {
 *       detected_source_language: "",
 *       ...
 *    }, 
 *    {
 *    }
 *    ]
 * }
 *    
 * {
 *   translations: {
 *      detected_source_language: "",
 *   }

*/