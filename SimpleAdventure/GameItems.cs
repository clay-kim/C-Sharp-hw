using System;

namespace SimpleAdventure
{
    // Useful, pre-created Items, Armor, and Weapons that can be used anywhere in the game
    static class GameItems
    {
        public static readonly Item RabbitsFoot = new Item("Rabbit's Foot");
        public static readonly Item SilverRing = new Item("Silver Ring");

        public static readonly Armor Skin = new Armor("Skin", 0);
        public static readonly Armor ClothArmor = new Armor("Cloth Armor", 1);
        public static readonly Armor LeatherArmor = new Armor("Leather Armor", 4);
        public static readonly Armor ChainMailArmor = new Armor("Chainmail Armor", 8);
        public static readonly Armor GuardsArmor = new Armor("Guard's Armor", 1000);

        public static readonly Weapon Hands = new Weapon("Hands", 1);
        public static readonly Weapon Dagger = new Weapon("Dagger", 3);
        public static readonly Weapon RustyAxe = new Weapon("Rusty Axe", 5);
        public static readonly Weapon LongSword = new Weapon("LongSword", 8);
        public static readonly Weapon GuardsPike = new Weapon("Guard's Pike", 10);
    }
}