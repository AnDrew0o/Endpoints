using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace endpoints
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/headers", async context =>
                {
                    var headers = context.Request.Headers;
                    string result = "";
                    foreach(var header in headers)
                    {  
                        result += header.Key + ":\n";
                    }
                    await context.Response.WriteAsync(result);
                });

                endpoints.MapGet("/plural", async context =>
                {
                    int number = Int32.Parse(context.Request.Query["number"]);
                    string[] forms = context.Request.Query["forms"].ToString().Split(",");

                    string form = Pluralisation.GetForm(number, forms[0], forms[1], forms[2]);
                    //string form = Pluralisation.GetForm(number, "forms[0]", "forms[1]", "forms[2]");

                    await context.Response.WriteAsync(number + " " + form);
                });

                endpoints.MapPost("/frequency", async context =>
                {
                    string input = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    var dict = WordFrequency.GetDictionary(input);
                    int uniqueWords = dict.Count;
                    KeyValuePair<string, int> mostFrequentWord = dict.First();
                    foreach (var pair in dict)
                    {
                        if (pair.Value > mostFrequentWord.Value)
                            mostFrequentWord = pair;
                    }
                    context.Response.ContentType = "application/json";
                    context.Response.Headers.Add("uniqueWords", dict.Count.ToString());
                    context.Response.Headers.Add("mostFrequentWord", mostFrequentWord.Key);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(dict));
                });
            });
        }
    }
}
