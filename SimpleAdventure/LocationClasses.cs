using System;
using System.Collections.Generic;
using LWTech.CSD228.TextMenus;

namespace SimpleAdventure
{
    enum Direction { North, South, East, West }

    enum Locale
    {
        Nowhere,
        Town,
        TownGates,
        Crossroads,
        Woods,
        River,
        Bridge
    }

    class Location
    {
        public Locale Id { get; private set; }
        public string Description { get; private set; }
        public Dictionary<Direction, Locale> Pathways { get; private set; }

        public Actor Resident {get; private set;}
        public Dictionary<String, TextMenuItem<Player>> Menuitems {get; private set;}
        public Action<Player> PreAction { get; private set; }

        public Location(Locale id, string description, Actor resident = null)
        {
            if (description == null)
                throw new ArgumentNullException("description cannot be null");
            if (description.Length == 0)
                throw new ArgumentException("description cannot be empty");

            this.Id = id;
            this.Description = description;
            this.Pathways = new Dictionary<Direction, Locale>();
            this.Resident = resident;
            this.Menuitems = new Dictionary<String, TextMenuItem<Player>>();
            this.PreAction = null;

        }

        public void AddResident(Actor resident)
        {
            Resident = resident;
        }

        public void AddPathway(Direction direction, Locale locale)
        {
            Pathways.Add(direction, locale);
        }

        public void AddMenuItem(String key, TextMenuItem<Player> menuitems)
        {
            if(menuitems == null)
                throw new ArgumentNullException("Cannot take null argument");
            Menuitems.Add(key, menuitems);
        }

        public void RemoveMenuItem(String key)
        {
            Menuitems.Remove(key);
        }

        public void AddPreAction(Action<Player> preaction)
        {
            PreAction = preaction;
        }

        public void RunPreAction(Player player)
        {
            if(PreAction != null)
            PreAction(player);
        }

        public TextMenu<Player> GetMenu()
        {
            var menu = new TextMenu<Player>();
            foreach (Direction direction in Pathways.Keys)
            {
                menu.AddItem(new TextMenuItem<Player>($"Go {direction}",
                                    (p)=>{ p.MoveTo(Pathways[direction]); }));
            }

            foreach (String string1 in Menuitems.Keys)
            {
                menu.AddItem(Menuitems[string1]);
            }
        
            return menu;
        }

        public override string ToString()
        {
            if(Resident != null)
            {
                String str = Resident.Name;
               
                return $"Resident is nearby: {str}\n{Description}";
            }
        
            return $"{Description}";
        }
    }
}
