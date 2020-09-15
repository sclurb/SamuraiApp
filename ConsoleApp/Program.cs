using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Xml.Serialization;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            //GetSamurais("Before Add:");
            //InsertMultipleSamurais();
            //AddSamurai();
            //GetSamurais("After Add:");
            //Console.Write("Press any key...");
            //QueryFilters();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //RetrieveAndDeleteSamurai();
            //RetrieveAndUpdateSamurai();
            //InsertBattle();
            //QueryAndUpdateBattle_Disconnected();
            //InsertNewSamuraiWithQuote();
            //InsertNewSamuraiWithManyQuotes();
            //AddQuoteToExistingSamuraiWhileTracking();
            //int samuraiId = 2;
            //AddQuoteToExistingSamuraiNotTracked(samuraiId);
            //AddQuoteToExistingSamuraiNotTracked(samuraiId);
            //AddQuoteToExistingSamuraiNotTracked_Easy(samuraiId);
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //ProjectSamuraiWithQuotes();
            //ExplicitLoadQuotes();
            //FilteringWithRelatedData();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();
            //JoinBattleAndSamurai();
            //EnlistSamuraiIntoBattle();
            // RemoveJoinBetweenSamuraiAndBattleSimple();
            //GetSamuraiWithBattles();
            //AddNewSamuraiWithHorse();
            //AddNewHorseToSamuraiUsingId();
            //AddNewHorseToSamuraiObject();
            //AddNewHorseToDisConnectedSamuraiObject();
            //ReplaceHorse();
            //GetSamuraiWithHorse();
            GetHorseWithSamurai();

            Console.ReadKey();

        }

        private static void QueryFilters()
        {
            var name = "Doggy";
            //var samurais = _context.Samurais.Where(s => s.Name == name).ToList();
            var samurais = _context.Samurais.Find(2);
            var last = _context.Samurais.OrderBy(s => s.Id).LastOrDefault(s => s.Name == name);
            Console.WriteLine("Here is the result: " + last.Name);
        }

        private static void GetSamuraiSimpler()
        {
            var samurais = _context.Samurais.ToList();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Melissa" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Doggy" };
            var samurai2 = new Samurai { Name = "Pinky" };
            _context.Samurais.AddRange(samurai, samurai2);
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.Find(2);
            samurai.Name = "Bobby Donovan";
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(3).ToList();
            samurais.ForEach(s => s.Name += "Forward");
            _context.SaveChanges();
            Console.ReadKey();
        }

        private static void RetrieveAndDeleteSamurai()
        {
            var samurai = _context.Samurais.Find(3);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle Of Hard Knocks",
                StartDate = new DateTime(2020, 05, 01),
                EndDate = new DateTime(2020, 09, 15)
            });
            _context.SaveChanges();
            
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void InsertNewSamuraiWithQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "I've come to save you"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewSamuraiWithManyQuotes()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "Watch out ofr my sharp sword"},
                    new Quote {Text = "I told you to watch out for my sharp sword.  Oh well..."}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddQuoteToExistingSamuraiWhileTracking()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you are happy I saved you!"
            });
            _context.SaveChanges();
        }

        private static void AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
                samurai.Quotes.Add(new Quote
                {
                    Text = "I am Bobby Donovan"
                });
            using (var newContext = new SamuraiContext())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        }

        private static void AddQuoteToExistingSamuraiNotTracked_Easy(int samuraiId)
        {
            var quote = new Quote
            {
                Text = "Now that I have saved you, will you feed me dinner again?",
                SamuraiId = samuraiId
            };
            using (var newContext = new SamuraiContext())
            {
                newContext.Quotes.Add(quote);
                newContext.SaveChanges();
            }
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes1 = _context.Samurais.Include(s => s.Quotes).ToList();
            var samuraiWithQuotes = _context.Samurais.Where(s => s.Name.Contains("Bobby Donovan"))
                                                                .Include(s => s.Quotes).ToList();
            Console.ReadKey();
        }

        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name });
            Console.ReadKey();
        }

        private static void ProjectSamuraiWithQuotes()
        {
            //var somePropertiesWithQuotes = _context.Samurais
            //    .Select(s => new { s.Id, s.Name, s.Quotes.Count,
            //    HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) })
            //    .ToList();

            var samuraiWithHappyQuotes = _context.Samurais
                .Select(s => new {
                    Samurai = s,
                    HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy"))
                })
                .ToList();
            var firstsamurai = samuraiWithHappyQuotes[0].Samurai.Name += "The Happiest";
        }

        private static void ExplicitLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Bobby"));
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.Horse).Load();
        }

        private static void FilteringWithRelatedData()
        {
            var samurai = _context.Samurais
                .Where(s => s.Quotes.Any(Queryable => Queryable.Text.Contains("happy")))
                .ToList();
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            samurai.Quotes[0].Text = "Did you hear that?";
            _context.SaveChanges();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            var quote = samurai.Quotes[0];
            quote.Text = "Did you hear that Again?";
            using (var newContext = new SamuraiContext())
            {
                //newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        private static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle { SamuraiId = 2, BattleId = 1 };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }

        private static void EnlistSamuraiIntoBattle()
        {
            var battle = _context.Battles.Find(2);
            battle.SamuraiBattles.Add(new SamuraiBattle { SamuraiId = 2 });
            _context.SaveChanges();
        }

        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 2 };
            _context.Remove(join);
            _context.SaveChanges();
        }

        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattle = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(samurai => samurai.Id == 2);

            var samuraiWithBattlesCleaner = _context.Samurais.Where(s => s.Id == 2)
                .Select(s => new
                {
                    Samurai = s,
                    Battles = s.SamuraiBattles.Select(sb => sb.Battle)
                })
                .FirstOrDefault();
        }

        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Jina Ujichika" };
            samurai.Horse = new Horse { Name = "Silver" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddNewHorseToSamuraiUsingId()
        {
            var horse = new Horse { Name = "Scout", SamuraiId = 2};
            _context.Add(horse);
            _context.SaveChanges();
        }

        private static void AddNewHorseToSamuraiObject()
        {
            var samurai = _context.Samurais.Find(8);
            samurai.Horse = new Horse { Name = "Black Beauty" };
            _context.SaveChanges();
        }

        private static void AddNewHorseToDisConnectedSamuraiObject()
        {
            var samurai = _context.Samurais.AsNoTracking().FirstOrDefault(s => s.Id == 6);
            samurai.Horse = new Horse { Name = "Mr. Ed" }; 
            using (var newContext = new SamuraiContext())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }

        private static void ReplaceHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horse).FirstOrDefault(s => s.Id == 8);
            samurai.Horse = new Horse { Name = "Trigger" };
            _context.SaveChanges();
        }

        private static void GetSamuraiWithHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horse).ToList();
        }

        private static void GetHorseWithSamurai()
        {
            var horseWithoutSamurai = _context.Set<Horse>().Find(3);

            var horseWithSamurai = _context.Samurais.Include(s => s.Horse).FirstOrDefault(s => s.Horse.Id == 2);

            var horsesWithSamurais = _context.Samurais
                .Where(s => s.Horse != null)
                .Select(s => new { Horse = s.Horse, Samurai = s })
                .ToList();
        }

        private static void GetSamuraiWithClan()
        {
            var samurai = _context.Samurais.Include(s => s.Clan).FirstOrDefault();
        }

        private static void GetClanWithSamurais()
        {
            //var clan = _context.Clans.Include(c => c.????)
            var clan = _context.Clans.Find(3);
            var samuraisForClan = _context.Samurais.Where(s => s.Clan.Id == 3).ToList();
        }
    }
}
