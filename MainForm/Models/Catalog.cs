using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace MainForm.Models
{
     public class Catalog
    {
        List<Town> towns;
        List<GRegion> regions;
        List<Country> countrys;
        string path;
        int numoftowns;
        int numofregions;
        int numofcountrys;

        public Catalog()
        {
            towns = null;
            regions = null;
            countrys = null;
            path = null;
            numoftowns = 0;
            numofregions = 0;
            numofcountrys = 0;
        }

        public Catalog(List<Town> tmp1, List<GRegion> tmp2, List<Country> tmp3)
        {
            towns = tmp1;
            regions = tmp2;
            countrys = tmp3;
            path = null;
            numoftowns = tmp1.Count + 1;
            numofregions = tmp2.Count + 1;
            numofcountrys = tmp3.Count + 1;
        }

        public Catalog(string filename)
        {
            towns = new List<Town>();
            regions= new List<GRegion>() ;
            countrys= new List<Country>() ;
            path = filename;
            ReadFromFile();
            numoftowns =towns.Count;
            numofcountrys = countrys.Count;
            numofregions = regions.Count;
        }

        public Catalog(Catalog other)
        {
            towns = other.towns;
            regions = other.regions;
            countrys = other.countrys;
            path = other.path;
            numoftowns = other.numoftowns;
            numofregions = other.numofregions;
            numofcountrys = other.numofcountrys;
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public List<Town> UseTowns
        {
            get { return towns; }
            set { towns = value; }
        }
        public List<GRegion> UseRegions
        {
            get { return regions; }
            set { regions = value; }
        }
        public List<Country> UseCountrys
        {
            get { return countrys; }
            set { countrys = value; }
        }

        public int NumberTowns
        {
            get { return numoftowns; }
            set
            {
                if (value >= 0) numoftowns = value;
                else throw new Exception("Количесто элементов не может быть меньше 0");
            }
        }
        public int NumberRegions
        {
            get { return numofregions; }
            set
            {
                if (value >= 0) numofregions = value;
                else throw new Exception("Количесто элементов не может быть меньше 0");
            }
        }
        public int NumberCountrys
        {
            get { return numofcountrys; }
            set
            {
                if (value >= 0) numofcountrys = value;
                else throw new Exception("Количесто элементов не может быть меньше 0");
            }
        }
        private void ReadFromFile()
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    GetObject(sr);
                }
            }

        }
        private void GetObject(StreamReader a)
        {
            string[] tmp = a.ReadLine().Split('|');
            /*int pos1 = 0;
            int pos2 = 0;
            int pos3 = 0;*/
            switch (tmp[0].ToLower())
            {
                case "город":
                    {
                        bool isset = false;
                        Town s = new Town(tmp[1],tmp[2],tmp[3],Convert.ToDouble(tmp[4]),tmp[5],Convert.ToDouble(tmp[6]));
                        towns.Add(s);
                        foreach(Country co in countrys)
                        {
                            if(co.Name==tmp[2])
                            {
                                co.Add(s);
                                isset = true;
                                break;
                            }
                        }
                        if (!isset)
                        {
                            Country c = new Country(tmp[2],"-",tmp[3], Convert.ToDouble(tmp[4]));
                            c.Add(s);
                            countrys.Add(c);
                        }
                        break;
                    }
                case "регион":
                    {
                        bool isset = false;
                        GRegion ss = new GRegion(tmp[1], tmp[2], tmp[3], Convert.ToDouble(tmp[4]), tmp[5], tmp[6]);
                        regions.Add(ss);
                        foreach (Country co in countrys)
                        {
                            if (co.Name == tmp[2])
                            {
                                co.Add(ss);
                                isset = true;
                                break;
                            }
                        }
                        if (!isset)
                        {
                            Country c = new Country(tmp[2], "-", tmp[3], Convert.ToDouble(tmp[4]));
                            c.Add(ss);
                            countrys.Add(c);
                        }
                        break;
                    }
                case "страна":
                    {
                        Country sss = new Country(tmp[1], tmp[2], tmp[3], Convert.ToDouble(tmp[4]), tmp[5], Convert.ToDouble(tmp[6]));
                        countrys.Add(sss);
                        
                        break;
                    }
            }
            
            
        }
        public void Write(string str)
        {
           using (TextWriter tw = new StreamWriter(str, false, Encoding.GetEncoding(1251)))
                {
                    for (int i = 0; i < UseCountrys.Count; i++)
                    {
                    tw.WriteLine("Страна" + "|" + UseCountrys[i].Name + "|" + UseCountrys[i].Capital
						+ "|" + UseCountrys[i].Materic + "|" + UseCountrys[i].Citizens + "|"
							+ UseCountrys[i].Politic + "|" + UseCountrys[i].Area);
                    }

                    for (int i = 0; i <UseTowns.Count; i++)
                    {
                        tw.WriteLine("Город" + "|" + UseTowns[i].Name + "|" +  UseTowns[i].Country 
							+ "|" +  UseTowns[i].Materic + "|" +  UseTowns[i].Citizens + "|" 
								+  UseTowns[i].Geopos + "|" +  UseTowns[i].Area);
                    }

                    for (int i = 0; i < UseRegions.Count; i++)
                    {
                        tw.WriteLine("Регион" + "|" +  UseRegions[i].Name + "|" +  UseRegions[i].Country
							+ "|" +  UseRegions[i].Materic + "|" +  UseRegions[i].Citizens + "|"
								+  UseRegions[i].TypeRegion + "|" +  UseRegions[i].Capital);
                    }

                    
                }

            
        }
        public void Write(string str, List<Town> slist)
        {
            using (TextWriter tw = new StreamWriter(str, false, Encoding.GetEncoding(1251)))
            {
                for (int i = 0; i < slist.Count; i++)
                {
                    tw.WriteLine("Город" + "|" + slist[i].Name + "|" + slist[i].Country + "|" + slist[i].Materic + "|" + slist[i].Citizens + "|" + slist[i].Geopos + "|" + slist[i].Area);
                }
                  
            }


        }
        public void Write(string str, List<GRegion> slist)
        {
            using (TextWriter tw = new StreamWriter(str, false, Encoding.GetEncoding(1251)))
            {
                for (int i = 0; i < slist.Count; i++)
                {
                    tw.WriteLine("Регион" + "|" + slist[i].Name + "|" + slist[i].Country + "|" + slist[i].Materic + "|" + slist[i].Citizens + "|" + slist[i].TypeRegion + "|" + slist[i].Capital);
                }

            }


        }
        public void Write(string str, List<Country> slist)
        {
            using (TextWriter tw = new StreamWriter(str, false, Encoding.GetEncoding(1251)))
            {
                for (int i = 0; i < slist.Count; i++)
                {
                    tw.WriteLine("Страна" + "|" + slist[i].Name + "|" + slist[i].Capital + "|" + slist[i].Materic + "|" + slist[i].Citizens + "|" + slist[i].Politic + "|" + slist[i].Area);
                }

            }


        }
        public void FixTowns()
        {
            towns.Clear();
            foreach(Country temp in countrys)
            {
                foreach (Town t in temp.Towns()) towns.Add(t);
            }
        }
        public void FixRegions()
        {
            regions.Clear();
            foreach (Country temp in countrys)
            {
                foreach (GRegion t in temp.GRegions()) regions.Add(t);
            }
        }
    }
}
