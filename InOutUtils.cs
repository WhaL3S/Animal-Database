using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace P3
{
    static class InOutUtils
    {
        public static Register Read(string fileName)
        {
            Register register = new Register();
            using (StreamReader reader = new StreamReader(fileName,
            Encoding.GetEncoding(1257)))
            {
                string animalDB = reader.ReadLine().Trim();
                register.DatabaseName = animalDB;
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    string type = values[0];
                    switch (type)
                    {
                        case "D":
                            Dog dog = new Dog(line);
                            if (!register.Contains(dog))
                                register.Add(dog);
                            break;
                        case "C":
                            Cat cat = new Cat(line);
                            if (!register.Contains(cat))
                                register.Add(cat);
                            break;
                        case "G":
                            GuineaPig GuineaPig = new GuineaPig(line);
                            if (!register.Contains(GuineaPig))
                                register.Add(GuineaPig);
                            break;
                        default:
                            break;
                    }
                }
            }
            return register;
        }

        public const string CCSV = @"SpecialCare.csv";

        public static void WriteToCSV(string file, bool supplyHeaders, Register register)
        {
            using (StreamWriter writer = new StreamWriter(file, false))
            {
                if (supplyHeaders)
                {
                    string headerFormat = string.Format("{0};", register.DatabaseName);
                    writer.WriteLine(headerFormat);
                }
                string tableHeader = string.Format
                ("{0,5};{1, 6};{2,-10};{3,-12};{4,-8};{5,-8};{6};{7};", "Type", "ID", "Name", "Breed", "Birth Date", "Gender", "Vaccination", "Aggressive");
                writer.WriteLine(tableHeader);
                for (int i = 0; i < register.Count(); i++)
                {
                    Animal a = register.Get(i);
                    writer.WriteLine(a.ToCSVString());
                }
            }
        }
    }
}
