using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class JsonController : Controller
    {
        private const string URL = "https://5fa04c51e21bab0016dfd080.mockapi.io/wth/Post";
        private const string URL2 = "https://5fa04c51e21bab0016dfd080.mockapi.io/wth/Person";
        private static string PostIdtag; private static string PersonIdtag;

        public IActionResult Index()
        {

            var PostId = new Datamonster() { PostId = PostIdtag};

            var PersonId = new Datamonster() { PersonId = PersonIdtag };

            return View(PostId, PersonId);
        }





        public static void jsonStringToCSV(string jsonContent)
        {
            var dataTable = (DataTable)JsonConvert.DeserializeObject(jsonContent, (typeof(DataTable)));

            //Datatable to CSV
            var lines = new List<string>();
            string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            File.WriteAllLines(@"D:/Export.csv", lines);
        }






        public static async Task JputhaPost()
        {



            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format. Hol up, ai ehema header accept karanne?
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    response.EnsureSuccessStatusCode();

                    //eliye gahuwoth doesnot exist in the current context wenawa. ekai methana gahuwe.
                    if (response.Content is object)
                    {
                        // var result = response.Content.ReadAsAsync();
                        var responseContent = response.Content.ReadAsStringAsync().Result;


                        Console.WriteLine("Raw data heystack");

                        //check
                        Console.WriteLine(responseContent);

                        //csv thrower
                        jsonStringToCSV(responseContent);

                        PostIdtag = responseContent;

                        // var dataObj = JObject.Parse(responseContent);
                        // Console.WriteLine("data------------{0}", JObject.Parse(responseContent)["results"]);
                        Console.WriteLine("..................................................................");
                        // Console.WriteLine(dataObj);

                        //splitting
                        var cow = responseContent.Split("},{");
                        //count of stuff! every stuff
                        int cowlen = cow.Length;

                        //data row count
                        Console.WriteLine("*Row count from this resource: {0} ", cowlen);

                        //printing splitted data rows
                        for (int i = 0; i < cowlen; i++)
                        {
                            Console.WriteLine(cow[i]);
                        }



                     

                    };
                };
            }
        }






        public static async Task JputhaPerson()
        {



            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL2);

                // Add an Accept header for JSON format. Hol up, ai ehema header accept karanne?
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    response.EnsureSuccessStatusCode();

                    //eliye gahuwoth doesnot exist in the current context wenawa. ekai methana gahuwe.
                    if (response.Content is object)
                    {
                        // var result = response.Content.ReadAsAsync();
                        var responseContent = response.Content.ReadAsStringAsync().Result;


                        Console.WriteLine("Raw data heystack");

                        Console.WriteLine(responseContent);

                        PersonIdtag = responseContent;

                        // var dataObj = JObject.Parse(responseContent);
                        // Console.WriteLine("data------------{0}", JObject.Parse(responseContent)["results"]);
                        Console.WriteLine("..................................................................");
                        // Console.WriteLine(dataObj);

                        //splitting
                        var cow = responseContent.Split("},{");
                        //count of stuff! every stuff
                        int cowlen = cow.Length;

                        //data row count
                        Console.WriteLine("*Row count from this resource: {0} ", cowlen);

                        //printing splitted data rows
                        for (int i = 0; i < cowlen; i++)
                        {
                            Console.WriteLine(cow[i]);
                        }

                    };
                };
            }
        }
    }
}