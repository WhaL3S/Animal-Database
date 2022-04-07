using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3
{
    class GuineaPig : Animal, IEquatable<Animal>, IComparable<Animal>
    {
        public DateTime LastVaccination { get; set; }

        public GuineaPig() : base() { }

        public GuineaPig(int id, string name, string breed, DateTime birthDate, Gender gender,
       DateTime LastVaccination) : base(id, name, breed, birthDate, gender)
        {
            this.LastVaccination = LastVaccination;
        }

        public GuineaPig(string data) : base(data)
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
            return string.Format("| {0,-9} {1,-10} | {2,11:yyyy-MM-dd} | {3,12}", "GuineaPig",
            base.ToString(), this.LastVaccination, "|");
        }

        public override bool Equals(Animal other)
        {
            return base.Equals(other);
        }

        public override bool RequiresVaccination
        {
            get
            {
                if (this.LastVaccination.Equals(DateTime.MinValue))
                {
                    return true;
                }
                return false;
            }
        }

        public override bool NeedSpecialCare()
        {
            if ((Age > 5) && (Age < 9))
                    return true;
            return false;
        }

        public override string ToCSVString()
        {
            return string.Format("{0,5};{1,-10};{2,12:yyyy-MM-dd};{3,11}", "GuineaPig",
            base.ToCSVString(), this.LastVaccination, ";");
        }

    }
}
