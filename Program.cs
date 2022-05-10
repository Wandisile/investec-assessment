using InvestecConsoleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvestecConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var buddies = GetBuddies();
            for (int i = 0; i < buddies.Count; i++)
            {
                Console.WriteLine("--------------");
                for (int j = 0; j < buddies[i].Buddies.Count; j++)
                {
                    Console.WriteLine(buddies[i].Buddies[j]);
                }
             
            }
        }

        public static List<BuddyList> GetBuddies()
        {
            try
            {
                var people = FetchPeopleFilms().Result;
                List<BuddyList> buddies = new List<BuddyList>();
                var currentBudy = people[0];
                for (int i = 0; i < people.Count; i++)
                {
                    var group = Guid.NewGuid();
                    for (int j = 1; j < people.Count; j++)
                    {
                        if (currentBudy.films.Count == people[j].films.Count
                        && currentBudy.name != people[j].name)
                        {
                            var sameFilms = Enumerable.SequenceEqual(currentBudy.films.OrderBy(c => c), 
                                people[i].films.OrderBy(p => p));
                           
                            if (sameFilms)
                            {
                                var existingBuddies = buddies.Find(b => b.group == group);
                                if (existingBuddies != null)
                                {
                                    existingBuddies.Buddies.Add(people[j].name);
                                }
                                else
                                {
                                    buddies.Add(new BuddyList
                                    {
                                        group = group,
                                        Buddies = new List<string> { people[j].name, currentBudy.name }
                                    });
                                }
                            }
                        }
                    }
                    currentBudy = people[i];
                }
                
                return buddies;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        public async static Task<List<PeopleFilms>> FetchPeopleFilms()
        {
            List<PeopleFilms> pf = new();
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://swapi.dev/api/people")
                    .ConfigureAwait(false);
                pf = JsonSerializer.Deserialize<Result>(response).results;

            }
            return pf;
        }
    }
}
