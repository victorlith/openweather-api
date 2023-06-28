using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

await Main();

static async Task Main()
{
    const string apiKey = "14f01ddef5bea8fec7e30a6352958af5";
    string? cityName;

    Console.WriteLine("Dados Meteorologico");

    Console.Write("Digite o nome da sua cidade: ");
    cityName = Convert.ToString(Console.ReadLine());

    string apiURL = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}";

    Console.Clear();

    using(HttpClient client = new HttpClient())
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(apiURL);
        
            if(response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                WeatherData? weather = JsonConvert.DeserializeObject<WeatherData>(responseBody);
          
                //Console.WriteLine("Os dados meteorologicos de sua cidade: ");
                Console.WriteLine("Cidade: " + weather?.Name);
                Console.WriteLine("Temperatura: " + (weather.Main.Temp - 273).ToString("f0") + " C°");
                
            }
            else
            {
                Console.WriteLine("Erro na requisição: " + response.StatusCode);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
    }
}

public class WeatherData
{
    public string Name {get; set;}
    public MainData Main {get; set;}
    
}

public class MainData
{
    public double Temp {get; set;}
}