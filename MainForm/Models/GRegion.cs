using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm.Models
{
    public class GRegion
    {
        string name;
        
        string typeregion;
        string capital;
        string country;
        
        double citizens;
        
        string materic;
        public GRegion() { }
        public GRegion(string namex, string countryx, string matericx, double citizensx = 0, string typex = "-", string capitalx = "-")
        {
            name = namex;
            citizens = citizensx;
            materic = matericx;
            country = countryx;
            capital = capitalx;
            typeregion = typex;
        }
        /*public string GetTypeg
        {
            get { return "регион"; }
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

        public string TypeRegion
        {
            get { return typeregion; }
            set
            {
                if (value != String.Empty && (value.ToLower() == "экономический" || value.ToLower() == "географический")) typeregion = value.ToLower();
                else typeregion="-";
            }
        }
        public string Capital
        {
            get { return capital; }
            set
            {
                if (value != String.Empty) capital = value;
                else capital="-";
            }
        }
        
    }
}
