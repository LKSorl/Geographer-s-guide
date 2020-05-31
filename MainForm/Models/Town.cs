using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm.Models
{
    public class Town
    {
        
        string name;
        string geoposition;
        
        
        string country;
        
        double area;
        double citizens;
        
        string materic;
        public Town()
        {

        }
        public Town(string namex, string countryx, string matericx, double citizensx=0, string geopositionx="-", double areax=0)
        {
            
            name = namex;
            citizens = citizensx;
            materic = matericx;
            country = countryx;
            geoposition = geopositionx;
            if (areax == 0) area = 0;
            else area = Convert.ToDouble(areax);
        }
        /*public int ID
        {
            get { return id; }
        }*/
        public string Name
        {
            get { return name; }
            set
            {
                 name = value;
                
            }
        }
        public string Country
        {
            get { return country; }
            set
            {
                if (value != String.Empty) country = value;
                else country = "-";
                //else throw new Exception("Название страны города имеет недопустимое значение.");
            }
        }
        
        public string Materic
        {
            get { return materic; }
            set
            {
                if (value != String.Empty && (value.ToLower() == "австралия" || value.ToLower() == "северная америка" || value.ToLower() == "южная америка" || value.ToLower() == "евразия" || value.ToLower() == "африка" || value.ToLower()=="антарктида")) materic = value;
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
                    double p = Convert.ToDouble(value);
                    if (p < 0) throw new Exception("Население не может быть отрицательным");
                    else citizens = p;
                }
                catch (FormatException)
                {
                    throw new Exception("Численность населения имеет недопустимое значение");
                }
            }
        }
        
        
        public string Geopos
        {
            get { return geoposition; }
            set
            {
                if (value != String.Empty) geoposition = value;
                else geoposition = "-";
            }
        }
        public double Area
        {
            get { return area; }
            set
            {
                try
                {
                    
                    double p = Convert.ToDouble(value);
                    if (p < 0) throw new Exception("Площадь не может быть отрицательной");
                    else area = p;
                }
                catch (FormatException)
                {
                    throw new Exception("Площадь имеет недопустимое значение");
                }
                
            }
        }
    }
}
