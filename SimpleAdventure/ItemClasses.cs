using System;

namespace SimpleAdventure
{
    class Item
    {
        public string Name { get; private set; }

        public Item(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name cannot be null");
            if (name.Length == 0)
                throw new ArgumentException("name cannot be empty");

            this.Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    class Armor : Item
    {
        public int MaxProtection { get; private set; }

        public Armor(string name, int maxProtection) : base(name)
        {
            this.MaxProtection = maxProtection;
        }

        public override string ToString()
        {
            return $"{Name}({MaxProtection})";
        }
    }

    class Weapon : Item
    {
        public int MaxDamage { get; private set; }

        public Weapon(string name, int maxDamage) : base(name)
        {
            this.MaxDamage = maxDamage;
        }

        public override string ToString()
        {
            return $"{Name}({MaxDamage})";
        }
    }
}