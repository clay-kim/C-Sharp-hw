using System;
using static System.Console;
using System.Collections.Generic;

namespace SimpleAdventure
{
    static class GameActions
    {
        public static void DrinkRiverWater(Player player)
        {
            player.Heal(4);
            Console.WriteLine("You drink some water from the river and feel refreshed.");
            
        }

        public static void TalkToGuard(Player player)
        {
            //Item SilverRing = new Item("Silver Ring");
            if(player.Has(GameItems.SilverRing))
            {
                Console.WriteLine("Guard: I see you have defeated the goblin that has plagued us for years.  You may enter our town, Professor the Confused!");
                player.MoveTo(Locale.Town);
            }
            else 
                Console.WriteLine("Guard: Hello there stranger.  Sorry, but I cannot let a stranger enter the town without proof that you are trustworthy.");

        }

        public static void FindForestItems(Player player)
        {
            player.Equip(GameItems.ChainMailArmor);
            player.Equip(GameItems.LongSword);

        }

        public static void MonsterAttacks(Player p)
        {
            GameActions.Fight(p, true);
        }

        public static void PlayerAttacks(Player p)
        {
            GameActions.Fight(p, false);
        }

        private static void Fight(Player player, bool monsterStarts = false)
        {
            Actor opponent = Program.theWorld[player.Locale].Resident;
            bool stillFighting = true;
            
            if (monsterStarts)
                {
                    opponent.Attack(player);
                    WriteLine($"The {opponent.Name} attacks you with their {opponent.Weapon}! You now have {player.Health} health points.");
                    if (player.Health <= 0)
                    {
                        stillFighting = false;
                        WriteLine("\nOh no! You are mortally wounded!  You are dead...");
                    }
                }
            while (stillFighting)
            {

                player.Attack(opponent);
                Console.WriteLine($"\nYou attack the {opponent.Name} with your [{player.Weapon}]! They now have {opponent.Health} health points.");
                
                if(opponent.Health == 0)
                {   
                    List<Item> item = ((Monster)opponent).TakeLoot();
                    Console.WriteLine("\nYOU ARE VICTORIOUS!!!!");

                    foreach(Item i in item)
                        {
                            player.AddItemToBag(i);
                            Console.WriteLine($"\nYou find a [{i}] on the monster's body and you add it to your bag.");
                        }
                    
                    stillFighting = false;
                }

                    else if (stillFighting)
                    {
                        opponent.Attack(player);
                        Console.WriteLine($"The {opponent.Name} attacks you with their [{opponent.Weapon}]! You now have {player.Health} health points.");

                        if(player.Health == 0)
                            stillFighting = false;

                        else if (stillFighting)
                        {
                            Console.WriteLine("\nDo you wish to continue fighting (y/n)?");
                             string s = Console.ReadLine();
                             if(s == "n")
                             stillFighting = false;
            
                        }
                    }
             
            }
        }
    }
}