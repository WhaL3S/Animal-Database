using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3
{
    class Dog : Animal, IEquatable<Animal>, IComparable<Animal>
    {
        private const int VaccinationDuration = 1; 
        public DateTime LastVaccination { get; set; }
        public bool Aggresive { get; set; } 
        public Dog() : base() { }

        public Dog(int id, string name, string breed, DateTime birthDate, Gender gender,
        DateTime LastVaccination, bool aggresive) : base(id, name, breed, birthDate, gender)
        {
            this.LastVaccination = LastVaccination;
            this.Aggresive = aggresive;
        }

        public Dog(string data) : base(data)
        {
            SetData(data);
        }

        public override void SetData(string line)
        {
            base.SetData(line); 
            string[] values = line.Split(';');
            LastVaccination = DateTime.Parse(values[6]);
            Aggresive = bool.Parse(values[7]);
        }

        public override string ToString()
        {
            return string.Format("| {0,-9} {1} | {2,11:yyyy-MM-dd} | {3,-10} |", "Dog",
            base.ToString(), this.LastVaccination, Aggresive);
        }
        
        public override bool Equals(Animal other)
        {
            return base.Equals(other);
        }
       
        public override int CompareTo(Animal other)
        {
            Dog d = other as Dog;
            int cAggresive = this.Aggresive.CompareTo(d.Aggresive);
            int cName = String.Compare(base.Name, d.Name,
            StringComparison.CurrentCulture);
            if ((cAggresive < 0) || (cAggresive == 0 && (cName > 0)))
                return 1;
            else if ((cAggresive > 0) || (cAggresive == 0 && (cName < 0)))
                return -1;
            return 0;
        }

        public override bool RequiresVaccination
        {
            get
            {
                if (LastVaccination.Equals(DateTime.MinValue))
                {
                    return true;
                }
                return LastVaccination.AddYears(VaccinationDuration)
                .CompareTo(DateTime.Now) < 0;
            }
        }

        public override bool NeedSpecialCare()
        {
            int AgeLimitMale = 10;
            int AgeLimitFeMale = 12;
            if (Aggresive)
            {
                if (base.Gender == Gender.Male)
                {
                    AgeLimitMale--;
                    if (Age > AgeLimitMale)
                        return true;
                }
                else if (base.Gender == Gender.Female)
                {
                    AgeLimitFeMale--;
                    if (Age > AgeLimitFeMale)
                        return true;
                }
            }
            else
            {
                if (base.Gender == Gender.Male)
                {
                    if (Age > AgeLimitMale)
                        return true;
                }
                else if (base.Gender == Gender.Female)
                {
                    if (Age > AgeLimitFeMale)
                        return true;
                }
            }
            return false;
        }

        public override string ToCSVString()
        {
            return string.Format("{0,5};{1};{2,12:yyyy-MM-dd};{3,-9};", "Dog",
            base.ToCSVString(), this.LastVaccination, Aggresive);
        }
    }
}
