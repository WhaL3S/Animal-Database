using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3
{
    abstract class Animal : IEquatable<Animal>, IComparable<Animal>
    {
        public int ID { get; set; } 
        public string Name { get; set; } 
        public string Breed { get; set; } 
        public DateTime BirthDate { get; set; } 
        public Gender Gender { get; set; } 
        public Animal() { }
        public abstract bool RequiresVaccination { get; }
        public abstract bool NeedSpecialCare();

        public Animal(int id, string name, string breed, DateTime birthDate, Gender gender)
        {
            this.ID = id;
            this.Name = name;
            this.Breed = breed;
            this.BirthDate = birthDate;
            this.Gender = gender;
        }

        public Animal(string data)
        {
            SetData(data);
        }

        public virtual void SetData(string line)
        {
            string[] values = line.Split(';');
            ID = int.Parse(values[1]);
            Name = values[2];
            Breed = values[3];
            BirthDate = DateTime.Parse(values[4]);
            Gender g;
            Enum.TryParse(values[5], out g); 
            Gender = g;
        }

        public override string ToString()
        {
            return String.Format("| {0,8} | {1,-10} | {2,-12} | {3,-12:yyyy-MM-dd} | {4,-8}",
            ID,
           Name,
           Breed,
           BirthDate,
           Gender);
        }

        private int age;

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                age = today.Year - this.BirthDate.Year;
                if (this.BirthDate.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
            set
            {
                age = value;
            }
        }

        public virtual int CompareTo(Animal other)
        {
            int result = this.Breed.CompareTo(other.Breed);
            if (result == 0)
            {
                return this.Gender.CompareTo(other.Gender);
            }
            return result;
        }

        public virtual bool Equals(Animal other)
        {
            return this.ID == other.ID;
        }

        public virtual string ToCSVString()
        {
            return String.Format("{0,8};{1,-10};{2,-12};{3,-12:yyyy-MM-dd};{4,-8}", ID, Name, Breed, BirthDate, Gender);
        }
    }
}
