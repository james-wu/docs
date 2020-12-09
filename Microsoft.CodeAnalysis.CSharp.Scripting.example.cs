using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ws2.UtilityTool.Models;
using Ws2.UtilityTool.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ws2.UtilityTool.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {


            var path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var serializer = new JsonSerializer();

            var data = JObject.Parse(
                System.IO.File.ReadAllText($"{path}\\Data\\document.json"));
            var dictionary =
                (IDictionary<string, object>) serializer.Deserialize(new JTokenReader(data),
                    typeof(IDictionary<string, object>));

            var dictionary2 = (Dictionary<string, string>)serializer.Deserialize(new JTokenReader(data),
                typeof(Dictionary<string, string>));


            var ruleObj = JObject.Parse(
                System.IO.File.ReadAllText($"{path}\\Data\\rules.json"));

            var rule = (Rootobject) serializer.Deserialize(new JTokenReader(ruleObj), typeof(Rootobject));

            

            var condition = "Context[\"FundedEFTS\"]";

            try
            {
                var options = ScriptOptions.Default.WithReferences(
                    typeof(Dictionary<string, object>).GetTypeInfo().Assembly
                );
                var task = await CSharpScript.EvaluateAsync(
                    condition,
                    options,
                    new Global
                    {
                        Context = dictionary
                    });
            } catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            //void CardStats findCard(Func<int, CardStats, bool> pred)
            //{
            //    return cardPool.Where(kv => pred(kv.Key, kv.Value))
            //        .Select(kv => kv.Value)
            //        .FirstOrDefault();
            //}
            //accValues.Where(kvp => kvp.Key == "SomeString").Select(kvp => kvp.Value).FirstOrDefault();
            // 
            //var options2 = ScriptOptions.Default.AddReferences(typeof(IDictionary<string, object>).Assembly);
            //var filter = "";

            //Func<string, IDictionary<string, object>, bool> filterExpression = await CSharpScript.
            //    EvaluateAsync<Func<string, IDictionary<string, object>, bool>>
            //        (filter, options2);

            //var value = 
            //    context.Where(kvp => kvp.Key == "FundedEFTS").Select(kvp => kvp.Value);

            //var predicate = "Context[\"FundedEFTS\"] > 0";
            //Func<Dictionary<string, object>, bool> pre =
            //    context.Where(m => m.Key == "FundedEFTS").Select(kvp => kvp.Value) > 0;y7777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777g
            dynamic temp = new ExpandoObject();
            temp.member1 = "aha";
            temp.member2 = 1234;
            var json = JsonConvert.SerializeObject(dictionary, Newtonsoft.Json.Formatting.Indented);
            try
            {

                //var myTest = JsonConvert.DeserializeObject(json);
                var obj = JObject.Parse(json);

                //foreach (var x in obj) {
                //    string name = x.Key;
                //    JToken value = x.Value;
                //}

                //var name = obj.Properties().Where(kvp => kvp.Name == "FundedEFTS").Select(kvp => kvp.Value);

                //foreach (PropertyInfo prop in myTest.GetType().GetProperties()) {
                //    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                //    Debug.WriteLine(prop.GetValue(myTest, null).ToString());

                //}



                //    var discountFilter = "doc => doc.FundedEFTS > 0";
                //var options3 = ScriptOptions.Default.AddReferences(myTest.GetType().Assembly);

                //Func<object, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<object, bool>>(discountFilter, options3);

                ////var discountedAlbums = albums.Where(discountFilterExpression);

                //    var result = discountFilterExpression(context);


            } catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //var result = await EvaluateDictAsync(context);

            //await EvaluateKeyValuePairAsync(dictionary);

            await EvaluateDictionaryAsync(dictionary);
            await EvaluateAsync(dictionary2);

            return new string[] {"value1", "value2"};
        }

        private static async Task EvaluateUserAsync()
        {
            var user = new User()
            {
                Id = 1,
                Age = 25
            };
            var filter = "doc => doc.Age < 24";
            var options3 = ScriptOptions.Default.AddReferences(typeof(User).Assembly);
            Func<User, bool> discountFilterExpression = await CSharpScript
                .EvaluateAsync<Func<User, bool>>(filter, options3);
            var result = discountFilterExpression(user);
        }

        public static bool HasContent(object value)
        {
            return value == null;
        }

        private async Task<bool> EvaluateDictAsync(IDictionary<string, object> dictionary)
        {
            Func<KeyValuePair<string, object>, bool> predicate = kvp => kvp.Key == "FundedEFTS"
                                                                        && Convert.ToDecimal(kvp.Value) > 0;
            var output = dictionary
                .FirstOrDefault(predicate);

            //return output.Value != null && output.Key != null;
            try
            {
                var condition =
                    "kvp => (kvp.Key == \"FundedEFTS\" && Ws2.UtilityTool.Controllers.ValuesController.HasContent(kvp.Value))";
                var options = ScriptOptions.Default.AddReferences(typeof(KeyValuePair<string, object>).Assembly)
                    .AddReferences(typeof(ValuesController).Assembly);
                Func<KeyValuePair<string, object>, bool> expression = await CSharpScript
                    .EvaluateAsync<Func<KeyValuePair<string, object>, bool>>(condition, options);
                var result = dictionary.FirstOrDefault(expression);

                return result.Value != null && result.Key != null;

            } catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            //var q1 = testDict.Where(p => p.Value == "Apple");

        }

        public static bool HasContent2(string key, object value, string cln)
        {
            return true;
        }

        private async Task EvaluateKeyValuePairAsync(IDictionary<string, object> dictionary)
        {
            foreach (KeyValuePair<string, object> valuePair in dictionary)
            {
                try
                {
                    var context = "kvp => Ws2.UtilityTool.Controllers.ValuesController.HasContent2(kvp.Key, kvp.Value, \"FundedEFTS\")";
                    var options = ScriptOptions.Default
                        .AddReferences(typeof(ValuesController).Assembly)
                        .AddReferences(typeof(KeyValuePair<string, object>).Assembly);
                    Func<KeyValuePair<string, object>, bool> expression = await CSharpScript
                        .EvaluateAsync<Func<KeyValuePair<string, object>, bool>>(context, options);


                    var result = expression(valuePair);

                    Debug.WriteLine($"{valuePair.Key} -> {valuePair.Value} => {result}");


                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static bool HasContent3(IDictionary<string, object> dictionary, string cln) {
            return dictionary[cln] != null;
        }

        private async Task EvaluateDictionaryAsync(IDictionary<string, object> dictionary)
        {
            try {
                var code = "dict => Ws2.UtilityTool.Controllers.ValuesController.HasContent3(dict, \"FundedEFTS\")";
                var options = ScriptOptions.Default
                    .AddReferences(typeof(ValuesController).Assembly)
                    .AddReferences(typeof(IDictionary<string, object>).Assembly);
                Func<IDictionary<string, object>, bool> expression = await CSharpScript
                    .EvaluateAsync<Func<IDictionary<string, object>, bool>>(code, options);


                var result = expression(dictionary);

            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task EvaluateAsync(Dictionary<string, string> document) {
            try {
                var code = "dict => Ws2.UtilityTool.Controllers.ValuesController.IsNull(dict, \"ProvisionOptions.YearToDate\") || Ws2.UtilityTool.Controllers.ValuesController.AsDateTime(dict, \"LearnerStartDate\", \"dd/MM/yyyy\") < Ws2.UtilityTool.Controllers.ValuesController.AsDateTime(dict, \"ProvisionOptions.YearToDate\", \"dd/MM/yyyy\") ";
                var options = ScriptOptions.Default
                    .AddReferences(typeof(ValuesController).Assembly)
                    .AddReferences(typeof(Dictionary<string, string>).Assembly);
                Func<Dictionary<string, string>, bool> expression = await CSharpScript
                    .EvaluateAsync<Func<Dictionary<string, string>, bool>>(code, options);

                var result = expression(document);

            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public static bool IsNull(Dictionary<string, string> dictionary, string key)
        {
            return dictionary[key] == null;
        }

        public static bool HasContent(Dictionary<string, string> document, string key) =>
            document.ContainsKey(key) && document[key]?.Length > 0;

        public static TType As<TType>(Dictionary<string, string> document, string key,
            TType defaultValue = default(TType)) =>
            HasContent(document, key) ? (TType)Convert.ChangeType(document[key], typeof(TType)) : defaultValue;

        public static DateTime AsDateTime(Dictionary<string, string> document, string key, string format) =>
            DateTime.ParseExact(document[key], format, null);

        public static DateTimeOffset AsDateTimeOffset(Dictionary<string, string> document, string key, string format) =>
            DateTimeOffset.ParseExact(document[key], format, null);


    }

   

    public class Global {
        public IDictionary<string, object> Context { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public int Age { get; set; }
    }

    //class Program {
    //    static void Main() {
    //        string condition = "dict[\"Key1\"]";
    //        var task = CSharpScript.EvaluateAsync(
    //            condition,
    //            ScriptOptions.Default.WithReferences(
    //                typeof(Dictionary<string, string>).GetTypeInfo().Assembly
    //            ),
    //            new Global {
    //                dict = new Dictionary<string, string> { ["Key1"] = "Value1" }
    //            });
    //        task.Wait();
    //        Console.WriteLine(task.Result);
    //    }
    //}
}
