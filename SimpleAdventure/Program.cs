using System;
using static System.Console;
using System.Collections.Generic;
using LWTech.CSD228.TextMenus;
using System.Collections;

namespace SimpleAdventure
{
    class Program
    {
       public static Dictionary<Locale, Location> theWorld = new Dictionary<Locale, Location>(); // 1) move the declaration of "theWorld" up above the Main()

      

        static void Main(string[] args)
        {
            // arraylist for random suffix (optional)
             var list = new ArrayList(){
                 "the Dreamer",
                 "the Wonderer",
                 "the Greatest killer",
                 "the Butcher",
                 "the Priest"
             };

             var rand = new Random();
             String suffix = (string)list[rand.Next(5)];

             Console.Write($"Enter your name please: \n");
                string s = Console.ReadLine();
                try
                {
                    Console.WriteLine($"Alright then, {s} {suffix}, let's get started!");
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Invalid entry.  Please enter Name correctly");
                }


            bool done = false;
            
            Player ourHero = new Player(s, GameItems.LeatherArmor, GameItems.Dagger, 20, Locale.TownGates);
            ourHero.Equip(GameItems.LeatherArmor);
            ourHero.Equip(GameItems.Dagger);
            ourHero.AddItemToBag(new Item("Rabbit's Foot"));
            ourHero.MoveTo(Locale.TownGates);

            // Location: Town Gates
            Actor resident = new Actor("Town Guard", GameItems.GuardsArmor, GameItems.GuardsPike, 1000, Locale.TownGates);

            Location location = new Location(Locale.TownGates, "You are at the gates of a town. A guard is standing in front of you.");
            location.AddPathway(Direction.North, Locale.Crossroads);
            theWorld.Add(Locale.TownGates, location);
            location.AddResident(resident);
            location.AddMenuItem("1", new TextMenuItem<Player>("Attack the Town Guard", (p)=> {
                GameActions.PlayerAttacks(p);
                if(resident.Health == 0)location.RemoveMenuItem("1");}));
            location.AddMenuItem("2", new TextMenuItem<Player>("Talk to the Guard", (p)=> {GameActions.TalkToGuard(p);}));
            
            
            // Location: Crossroads
            location = new Location(Locale.Crossroads, "You are at a lonely 4-way crossroads. You cannot see what lies in each direction.");
            location.AddPathway(Direction.North, Locale.River);
            location.AddPathway(Direction.West, Locale.Woods);
            location.AddPathway(Direction.South, Locale.TownGates);
            location.AddPathway(Direction.East, Locale.Bridge);
            theWorld.Add(Locale.Crossroads, location);

            // Location: Woods
            location = new Location(Locale.Woods, "You are in a dark forboding forest. Fallen trees block your way.");
            location.AddMenuItem("1", new TextMenuItem<Player>("Search the woods nearby", (p)=> {
                GameActions.FindForestItems(p);
                location.RemoveMenuItem("1");
                Console.WriteLine("You discovered a Long Sword and some Chain Mail hidden behind a tree!");}));
            
            location.AddPathway(Direction.East, Locale.Crossroads);
            theWorld.Add(Locale.Woods, location);
            
            // Location: River
            location = new Location(Locale.River, "You are at a swift-flowing, broad river that cannot be crossed.");
            location.AddMenuItem("1", new TextMenuItem<Player>("Take a drink of water", (p)=> {GameActions.DrinkRiverWater(p);}));
            location.AddPathway(Direction.South, Locale.Crossroads);
            theWorld.Add(Locale.River, location);

            // Location: Bridge
            Monster Goblin = new Monster("Goblin", GameItems.LeatherArmor, GameItems.RustyAxe, Locale.Bridge, 20);
            Goblin.AddLoot(GameItems.SilverRing);
            location = new Location(Locale.Bridge, "You come up to a bridge that appears to have been barricaded.");
            location.AddPathway(Direction.West, Locale.Crossroads);
            location.AddResident(Goblin);
            theWorld.Add(Locale.Bridge, location);

            location.AddPreAction((p)=> { 
                
                WriteLine("Look out!  A nasty goblin charges at you from under the bridge and attacks!");
                GameActions.MonsterAttacks(p);
                if(Goblin.Health == 0)
                {
                    location.RemoveMenuItem("1");
                }
                });

            while (!done)
            {   
                location = theWorld[ourHero.Locale];
        
                // Display the Player's current stats and location
                WriteLine("\n-----------------------------------------------------");
                WriteLine(ourHero);
                WriteLine("-----------------------------------------------------");
                WriteLine(location.ToString());
                location.RunPreAction(ourHero);
                if(ourHero.Health == 0)
                { 
                    Console.WriteLine("You got killed by monster: Game Over");
                    done = true;
                }
                TextMenu<Player> menu = location.GetMenu();

                int i = menu.GetMenuChoiceFromUser() - 1;
                WriteLine();
                menu.Run(i, ourHero);

                if(ourHero.Health == 0 || ourHero.At(Locale.Town))
                { 
                    Console.WriteLine("\n========== Game Over ==========");
                    done = true;
                }
            }
        }
    }
}