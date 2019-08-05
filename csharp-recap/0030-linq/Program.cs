using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    #region Helper classes
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Training
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public IEnumerable<Person> Attendees { get; set; }
    }

    class TrainingInfo
    {
        public string Title { get; set; }
        public int NumberOfAttendees { get; set; }
    }
    #endregion

    class Program
    {
        static void Main()
        {
            var demoData = GenerateDemoData();

            // #########################################################################################################
            // LEARN MORE about LINQ at https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
            // #########################################################################################################

            // Which trainings does Mr. Stropek attend?
            Print(from training in demoData
                  from attendee in training.Attendees
                  where attendee.LastName == "Stropek"
                  select training.Title);

            // Result of a method
            Print(from training in demoData select training.Attendees.First());

            // Result of a property
            Print(from training in demoData select training.Duration);

            // Anonymous type and aggregation function
            Print(from training in demoData
                  select new { training.Title, NumberOfAttendees = training.Attendees.Count() });

            // Instance of existing type
            Print(from training in demoData
                  select new TrainingInfo { Title = training.Title, NumberOfAttendees = training.Attendees.Count() });

            // Simple grouping
            Print(from training in demoData
                  group training by training.Duration);

            // Complex grouping expression
            Print(from training in demoData
                  group training by training.Title.Contains("C#") ? "C# related training" : "Other training" into trainingGroup
                  select new { trainingGroup.Key, NumberOfTrainings = trainingGroup.Count() });

            // Set operation
            Print((
                from training in demoData
                from attendee in training.Attendees
                select new { attendee.FirstName, attendee.LastName }
              ).Concat(
                from trainer in GetTrainers()
                select new { trainer.FirstName, trainer.LastName }
              ).Distinct());

            // Aggregation functions
            Console.WriteLine("Our trainers are: ");
            Console.Write(GetTrainers().Aggregate("", (agg, t) => $"{agg}* {t.FirstName} {t.LastName}\n"));

            // Method syntax
            var strings = new[]
            {
                "This is string number one",
                "This might be a second string"
            };
            Print(strings
                .SelectMany(s => s.Split(' '))
                .Distinct()
                .Select(s => s.ToLower())
                .OrderBy(s => s));
        }

        #region Helper methods
        static void Print<T>(T item)
        {
            Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
        }

        static IEnumerable<Training> GenerateDemoData()
        {
            return new[]
            {
                new Training {
                    Title = "C# Training",
                    Duration = TimeSpan.FromDays(2),
                    Attendees = new [] {
                        new Person {  FirstName = "Rainer", LastName = "Stropek" },
                        new Person {  FirstName = "Max", LastName = "Muster" }
                    }
                },
                new Training {
                    Title = "C# Deep Dive",
                    Duration = TimeSpan.FromDays(2),
                    Attendees = new [] {
                        new Person {  FirstName = "Ada", LastName = "Lovelace" },
                        new Person {  FirstName = "Anders", LastName = "Hejlsberg" }
                    }
                },
                new Training {
                    Title = ".NET Training",
                    Duration = TimeSpan.FromDays(4),
                    Attendees = new [] {
                        new Person {  FirstName = "Tom", LastName = "Turbo" },
                        new Person {  FirstName = "Max", LastName = "Muster" }
                    }
                }
            };
        }

        static IEnumerable<Person> GetTrainers()
        {
            return new[]
            {
                new Person {  FirstName = "Rainer", LastName = "Stropek" },
                new Person {  FirstName = "Bruce", LastName = "Wayne" }
            };
        }
        #endregion
    }
}
