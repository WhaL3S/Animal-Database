using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3
{
    class Cat : Animal, IEquatable<Animal>, IComparable<Animal>
    {
        private const int VaccinationDurationMonths = 6; 
        public DateTime LastVaccination { get; set; } 

        public Cat() : base() { }

        public Cat(int id, string name, string breed, DateTime birthDate, Gender gender,
       DateTime LastVaccination) : base(id, name, breed, birthDate, gender)
        {
            this.LastVaccination = LastVaccination;
        }
 
        public Cat(string data) : base(data)
        {
            SetData(data);
        }

        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(';');
            LastVaccination = DateTime.Parse(values[6]);
        }

        public override string ToString()
        {
            return string.Format("| {0,-9} {1,-10} | {2,11:yyyy-MM-dd} | {3,12}", "Cat",
            base.ToString(), this.LastVaccination, "|");
        }

        public override bool Equals(Animal other)
        {
            return base.Equals(other);
        }

        public override int CompareTo(Animal other)
        {
            Cat c = other as Cat;
            int cName = String.Compare(base.Name, c.Name,
            StringComparison.CurrentCulture);
            int cDate = this.LastVaccination.CompareTo(c.LastVaccination);
            if ((cDate < 0) || (cDate == 0 && (cName > 0)))
                return 1;
            else if ((cDate > 0) || (cDate == 0 && (cName < 0)))
                return -1;
            return 0;
        }

        public override bool RequiresVaccination
        {
            get
            {
                if (this.LastVaccination.Equals(DateTime.MinValue))
                {
                    return true;
                }
                return LastVaccination.AddMonths(VaccinationDurationMonths)
                .CompareTo(DateTime.Now) < 0;
            }
        }

        public override bool NeedSpecialCare()
        {
            if (base.Gender == Gender.Male)
            {
                if ((Age > 11) && (Age <= 14))
                    return true;
            }
            else if (base.Gender == Gender.Female)
            {
                if ((Age >= 10) && (Age <= 15))
                    return true;
            }
            return false;
        }

        public override string ToCSVString()
        {
            return string.Format("{0,5};{1,-10};{2,12:yyyy-MM-dd};{3,11}", "Cat",
            base.ToCSVString(), this.LastVaccination, ";");
        }

    }
}
