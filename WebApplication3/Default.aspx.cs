using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create a client instance
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress("http://www.webservicex.com/globalweather.asmx");

            GlobalWeatherSoapClient client = new GlobalWeatherSoapClient(binding, endpoint);

            var globalWeather = new GlobalWeatherSoapClient();

            var client2 = new Calculator.CalculatorSoapClient();
            int result2 = client2.Add(5, 3);
            Console.WriteLine($"Add(5, 3) = {result2}");


            try
            {
                var result1 = globalWeather.GetWeather("London", "UK");
                Console.WriteLine(result1);
            }
            catch (System.ServiceModel.ProtocolException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Handle the error, e.g., display a user-friendly message
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }


            

            var channelFactory = new ChannelFactory<WebApplication3.GlobalWeatherSoap>(binding);
            var channel = channelFactory.CreateChannel(endpoint);
            var result = channel.GetCitiesByCountry(new GetCitiesByCountryRequest { Body = new GetCitiesByCountryRequestBody { CountryName = "UK" } });

            // Call the GetWeather operation
            string cityName = "New York";
            string countryName = "United States";
            string weather = client.GetWeather(cityName, countryName);
            Console.WriteLine($"Weather in {cityName}, {countryName}: {weather}");

            // Call the GetCitiesByCountry operation
            string cities = client.GetCitiesByCountry(countryName);
            Console.WriteLine($"Cities in {countryName}: {cities}");

        }
    }
}