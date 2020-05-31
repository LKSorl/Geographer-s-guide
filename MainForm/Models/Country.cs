using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm.Models
{
    public class Country
    {
        string name;
        string capital;
        string politic;
        double area;
        double citizens;
        string materic;
        List<GRegion> regions;
        List<Town> towns;
        public Country() { }
        public Country(string namex, string capitalx , string matericx, double citizensx = 0, string politicx = "-", double areax=0)
        {
            towns = new List<Town>();
            regions = new List<GRegion>();
            name = namex;
            citizens = citizensx;
            materic = matericx;
            politic = politicx;
            capital = capitalx;
            
            area = areax;
        }
        /*public string GetTypeg
        {
            get { return "страна"; }
        }*/
        public string Name
        {
            get { return name; }
            set
            {
                if (value != String.Empty) name = value;
                else throw new Exception("Название страны имеет недопустимое значение.");
            }
        }
        public string Capital
        {
            get { return capital; }
            set
            {
                if (value != String.Empty) capital = value;
                else capital = "-";

            }
        }
        public string Materic
        {
            get { return materic; }
            set
            {
                if (value != String.Empty && (value.ToLower() == "австралия" || value.ToLower() == "северная америка" || value.ToLower() == "южная америка" || value.ToLower() == "евразия" || value.ToLower() == "африка" || value.ToLower() == "антарктида")) materic = value;
                else throw new Exception("Материк имеет недопустимое название.");
            }
        }
        public double Citizens
        {
            get { return citizens; }
            set
            {
                try
                {
                    
                    if (value < 0) throw new Exception("Население не может быть отрицательным");
                    else citizens = value;
                }
                catch (FormatException)
                {
                    throw new Exception("Численность населения имеет недопустимое значение");
                }
            }
        }

        public string Politic
        {
            get { return politic; }
            set
            {
                if (value != String.Empty && (value.ToLower() == "конституционная монархия" || value.ToLower() == "абсолютная монархия" || value.ToLower() == "республика")) politic = value.ToLower();
                else politic = "-";
            }
        }
        
        public double Area
        {
            get { return area; }
            set
            {
                try
                {
                    
                    if (value < 0) throw new Exception("Площадь не может быть отрицательной");
                    else area = value;
                }
                catch (FormatException)
                {
                    throw new Exception("Площадь имеет недопустимое значение");
                }
            }
        }
        public List<GRegion> GRegions()
        {
            return regions;
        }
        public List<Town> Towns()
        {
            return towns;
        }
        public void Add(GRegion a)
        {
            regions.Add(a);
            citizens += a.Citizens;
        }
        public void Add(Town a)
        {
            towns.Add(a);
            citizens += a.Citizens;
        }
    }
}
