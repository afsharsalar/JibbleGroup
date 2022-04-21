using System;
using System.Linq;
using System.Threading.Tasks;
using JibbleGroup.Service;
using Microsoft.Extensions.DependencyInjection;

namespace JibbleGroup.ConsoleApp
{
    internal class Program
    {
        static int tableWidth = 73;

        static async Task Main(string[] args)
        {
            var serviceProvider = ServiceProvider();

            var peopleService = serviceProvider.GetService<IPeopleService>();
            if (peopleService == null)
                throw new ArgumentNullException(nameof(peopleService));

            Console.WriteLine("Welcome to my task on Jibble group");

            while (true)
            {
                Console.WriteLine("[1] List people");
                Console.WriteLine("[2] Searching/Filtering people");
                Console.WriteLine("[3] A specific Person");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("");
                Console.Write("Please enter a valid choice: ");

                var input = Console.ReadLine();
                Console.Clear();
                try
                {
                    if (input == "1")
                    {
                        await PeopleList(peopleService, "");
                    }
                    else if (input == "2")
                    {
                        Console.Write("Please enter a filter (sample FirstName eq 'Scott' ):");
                        var filter = Console.ReadLine();
                        await PeopleList(peopleService, filter);


                    }
                    else if (input == "3")
                    {
                        await SpecificPerson(peopleService);
                    }else if (input == "0")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("please enter valid number");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
            



            


        }

        #region SpecificPerson
        /// <summary>
        /// Display specific person on Console
        /// </summary>
        /// <param name="peopleService"></param>
        /// <returns></returns>
        private static async Task SpecificPerson(IPeopleService peopleService)
        {
            Console.Write("Please enter a person username :");
            var userName = Console.ReadLine();

            var person = await peopleService.GetOneAsync(userName);
            Console.Clear();
            if (person == null)
            {
                Console.WriteLine($"{userName} not found!");
                return;
            }
            Console.WriteLine($"FirstName: {person.FirstName}");
            Console.WriteLine($"LastName: {person.LastName}");
            Console.WriteLine($"MiddleName: {person.MiddleName}");
            Console.WriteLine($"Gender: {person.Gender}");
            Console.WriteLine($"Age: {person.Age}");
            Console.WriteLine($"Emails: {string.Join('-', person.Emails)}");
            Console.WriteLine($"FavoriteFeature: {person.FavoriteFeature}");
            Console.WriteLine($"Features: {string.Join('-', person.Features)}");
            Console.WriteLine(
                $"AddressInfo: {string.Join('-', person.AddressInfo.Select(p => $"{p.Address}-{p.City.Name}-{p.City.CountryRegion}"))}");
            var homeAddress = person.HomeAddress != null
                ? $"{person.HomeAddress.Address}-{person.HomeAddress.City.Name}-{person.HomeAddress.City.CountryRegion}"
                : "-";
            Console.WriteLine($"HomeAddress: {homeAddress}");
            Console.WriteLine($"Budget: {person.Budget}");
            Console.WriteLine($"BossOffice: {person.BossOffice}");
            Console.WriteLine($"Cost: {person.Cost}");
        }
        #endregion

        #region PeopleList
        /// <summary>
        /// Display people full list or with filter
        /// </summary>
        /// <param name="peopleService"></param>
        /// <param name="filter">FirstName eq 'Scott'</param>
        /// <returns></returns>
        private static async Task PeopleList(IPeopleService peopleService, string filter)
        {
            var data = await peopleService.GetAsync(filter);
            Console.Clear();

            if (!data.Any())
                PrintRow("There is no data to display");
            else
            {
                PrintLine();
                PrintRow("UserName", "FirstName", "LastName", "Gender");
                PrintLine();
                foreach (var people in data)
                {
                    PrintRow(people.UserName, people.FirstName, people.LastName, people.Gender.ToString());
                }

                PrintLine();
            }

        }
        #endregion

        #region DisplayTable
        private static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }
        private static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        #endregion

        #region Dependency Injection
        /// <summary>
        /// User service collection for Dependency Injection(DI)
        /// </summary>
        /// <returns></returns>
        private static ServiceProvider ServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IPeopleService, PeopleService>()
                .AddHttpClient()
                .BuildServiceProvider();
            return serviceProvider;
        }
        #endregion

    }
}
