using System;
using System.Collections.Generic;

namespace SimpleAdventure
{
    class Actor
    {
        public string Name { get;  set; }
        public Locale Locale { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public Armor Armor { get; protected set; }
        public Weapon Weapon { get; protected set; }

        public Actor(string name, Armor armor, Weapon weapon, int health = 20, Locale locale = Locale.Nowhere)
        {
            if (name == null)
                throw new ArgumentNullException("name cannot be null");
            if (name.Length == 0)
                throw new ArgumentException("name cannot be empty");
            if (health < 0)
                throw new ArgumentOutOfRangeException("health cannot be negative");

            this.Name = name;
            this.Locale = locale;
            this.Health = health;
            this.MaxHealth = health;
            this.Armor = armor;
            this.Weapon = weapon;
        }

        public void MoveTo(Locale locale)
        {
            Locale = locale;
        }

        public bool At(Locale locale)
        {
            return Locale == locale;
        }

        public void Heal(int health)
        {
            if (health < 0)
                throw new ArgumentOutOfRangeException("health cannot be negative");

            Health += health;
            if (Health > MaxHealth)
                Health = MaxHealth;
        }

        public void Hurt(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException("damage cannot be negative");

            Health -= damage;
            if (Health < 0)
                Health = 0;
        }

        public void Attack(Actor defender)
        {
            var rand = new Random();
            int damage = rand.Next(Weapon.MaxDamage + 1);
            defender.Defend(damage);    

        }
        public void Defend(int damage)
        {
            var rand = new Random();
            int reduction = damage - (rand.Next(1, this.Armor.MaxProtection + 1));
            if (reduction < 1 )
                reduction = 1;
                
            this.Hurt(reduction);
        }


        public override string ToString()
        {
            return $"{Name}@{Locale}\tHealth: {Health}";
        }
    }

    class Player : Actor
    {
        public List<Item> Bag { get; private set; }

        public Player(string name, Armor armor, Weapon weapon, int health = 20, Locale locale = Locale.Nowhere) : base (name, armor, weapon, health, locale)
        {
            this.Bag = new List<Item>();
        }

        public void Equip(Armor armor)
        {
            if (armor == null)
                throw new ArgumentNullException("armor cannot be null");
            Armor = armor;
        }

        public void Equip(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException("weapon cannot be null");
            Weapon = weapon;
        }

        public void AddItemToBag(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("item cannot be null");
            Bag.Add(item);
        }

        public Item RemoveItemFromBag(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("item cannot be null");
            if (Bag.Remove(item))
                return item;
            return null;
        }

        public bool Has(Item item)
        {
            foreach (Item i in Bag)
            {
                if (i == item)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            string bag = "";
            string comma = "";
            foreach (Item item in Bag)
            {
                bag += $"{comma}{item}";
                comma = ", ";
            }
            return $"{Name} the dreamer\tHealth:{Health}\nArmor: {Armor}\tWeapon: {Weapon}\nBag: [{bag}]";
        }
    }

    class Monster : Actor
    {
        public List<Item> Loot { get; private set; }

        public Monster(string name, Armor armor, Weapon weapon, Locale locale = Locale.Nowhere, int health = 20) : base (name,armor, weapon, health, locale)
        {
            this.Loot = new List<Item>(); 
        }

        public void Equip(Armor armor)
        {
            if (armor == null)
                throw new ArgumentNullException("armor cannot be null");
            Armor = armor;
        }

        public void Equip(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException("weapon cannot be null");
            Weapon = weapon;
        }

        public void AddLoot(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("loot item cannot be null");

            Loot.Add(item);
        }

        public List<Item> TakeLoot()
        {
            if(Health == 0)
            return Loot;
            else
            return null;
        }

        public override string ToString()
        {
            return $"{Name}@{Locale}\tHealth:{Health}\nArmor: {Armor}\tWeapon: {Weapon}";
        }
    }
}