using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3
{
    class Register
    {
        public string DatabaseName { get; set; } 
        private List<Animal> Animals; 

        public Register()
        {
            DatabaseName = "";
            Animals = new List<Animal>();
        }
        
        public Register(Register register)
        {
            Animals = new List<Animal>();
            for (int i = 0; i < register.Count(); i++)
            {
                Animal a = register.Get(i);
                this.Animals.Add(a);
            }
        }
        
        public int Count() { return this.Animals.Count(); }
        public void Add(Animal animal) { this.Animals.Add(animal); }
        public Animal Get(int i) { return this.Animals[i]; }
        public void Put(int i, Animal animal) { this.Animals[i] = animal; }
        public bool Contains(Animal animal) { return Animals.Contains(animal); }

        public void Sort()
        {
            this.Animals.Sort();
        }

        public static Register operator +(Register a, Register b)
        {
            Register c = new Register();
            for (int i = 0; i < a.Count(); i++)
                c.Add(a.Get(i));
            for (int i = 0; i < b.Count(); i++)
                c.Add(b.Get(i));
            return c;
        }

        public static bool TwoOldestAnimals(Register register, out Animal Oldest1, out Animal Oldest2)
        {
            Oldest1 = register.Get(0);
            Oldest1.Age = -1;
            Oldest2 = register.Get(0);
            Oldest2.Age = -1;
            if (register.Count() < 2) return false;
            if (register.Get(0).Age > register.Get(1).Age)
            {
                Oldest1 = register.Get(0); Oldest2 = register.Get(1);
            }
            else
            {
                Oldest1 = register.Get(1); Oldest2 = register.Get(0);
            }
            for (int i = 2; i < register.Count(); i++)
            {
                if (register.Get(i).Age > Oldest1.Age)
                {
                    Oldest2 = Oldest1;
                    Oldest1 = register.Get(i);
                }
                else if (register.Get(i).Age > Oldest2.Age)
                    Oldest2 = register.Get(i);
            }
            return true;
        }

        public static Register RequireVaccination(Register register)
        {
            Register RVaccination = new Register();
            for (int i = 0; i < register.Count(); i++)
            {
                Animal animal = register.Get(i);
                if (animal.RequiresVaccination)
                {
                    RVaccination.Add(animal);
                }
            }
            return RVaccination;
        }

        public static int CountAggresiveDogs(Register register)
        {
            int howMany = 0;
            for (int i = 0; i < register.Count(); i++)
            {
                Animal a = register.Get(i);
                if (a is Dog && (a as Dog).Aggresive)
                    howMany++;
            }
            return howMany;
        }

        public static void NeedSpecialCareOfType(Register register, Type AType, ref Register sRegister)
        {
            for (int i = 0; i < register.Count(); i++)
            {
                Animal animal = register.Get(i);
                if (animal.GetType() == AType)
                {
                    if (animal.NeedSpecialCare())
                        sRegister.Add(animal);
                }
            }
        }
    }
}
