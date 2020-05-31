using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainForm.Models;
using System.Text.RegularExpressions;
using MainForm.Views;
namespace MainForm.Models
{
    public partial class Add_ChangeForm : Form
    {
        Catalog catalog;
        Town changet;
        GRegion changer;
        Country changec;
        int type;
        bool[] permtokens;
        bool maintoken;
        Dictionary<string, int> array;
        
        public Add_ChangeForm(Catalog temp)
        {
            InitializeComponent();
            catalog = temp;
            array = new Dictionary<string, int>();
            array.Add("Евразия",0);
            array.Add("Африка",1);
            array.Add("Северная Америка",2);
            array.Add("Южная Америка",3);
            array.Add("Австралия",4);
            array.Add("Антарктида",5);
            this.Text = "Добавление элемента";
            button1.Text = "Добавить";
            button1.DialogResult = DialogResult.None;
            button2.DialogResult = DialogResult.Cancel;
            StartChanging();
            permtokens =new bool[]{ true,true,true,true};
            maintoken = true;
        } 

        public Add_ChangeForm(Catalog temp, Town t, GRegion r, Country c)
        {
            InitializeComponent();
            catalog = temp;
            button1.Text = "Изменить";
            this.Text = "Изменение элемента";
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
            changec = c;
            changet = t;
            changer = r;
            permtokens = new bool[] { true, true, true, true };
            maintoken = true;
            type = (c != null) ? 2 : (r != null) ? 1 : 0;
            Prepare();
        }

        public void StartChanging()
        {

            label1.Visible = true;
            label2.Visible = false;
            label3.Visible = true;
            label5.Visible = false;
            label6.Visible = false;
            label4.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            label11.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            comboBox1.Visible = true;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            button3.Visible = false;
        }

        public void Prepare()
        {
            comboBox1.SelectedIndex = type;
            comboBox1.Enabled = false;
            
            switch (type)
            {
                case 0:
                    {
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label5.Visible = true;
                        label6.Visible = true;
                        label4.Visible = false;
                        label7.Visible = false;
                        label8.Visible = false;
                        label10.Visible = true;
                        label9.Visible = true;
                        label11.Visible = true;
                        textBox1.Visible = true;
                        textBox1.Text = changet.Name;
                        textBox2.Visible = false;
                        textBox3.Visible = true;
                        textBox3.Text = changet.Country;
                        textBox3.Enabled = false;
                        textBox4.Visible = true;
                        textBox4.Text = changet.Geopos;
                        comboBox1.Visible = true;
                        comboBox2.Visible = false;
                        comboBox3.Visible = false;
                        comboBox4.Visible = true;
                        comboBox4.Text = changet.Materic;
                        comboBox4.Enabled = false;
                        numericUpDown1.Visible = true;
                        numericUpDown1.Value = (decimal)changet.Area;
                        numericUpDown2.Visible = true;
                        numericUpDown2.Value = (decimal)changet.Citizens;
                        break;
                    }
                case 1:
                    {
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label5.Visible = true;
                        label6.Visible = false;
                        label4.Visible = true;
                        label7.Visible = true;
                        label9.Visible = true;
                        label8.Visible = false;
                        label10.Visible = false;
                        label11.Visible = false;
                        textBox1.Visible = true;
                        textBox1.Text = changer.Name;
                        textBox2.Visible = true;
                        textBox2.Text = changer.Capital;
                        textBox3.Visible = true;
                        textBox3.Text = changer.Country;
                        textBox3.Enabled = false;
                        textBox4.Visible = false;
                        comboBox1.Visible = true;

                        comboBox2.Visible = true;
                        comboBox2.Text = changer.TypeRegion;
                        comboBox3.Visible = false;
                        comboBox4.Visible = true;
                        comboBox4.Text = changer.Materic;
                        comboBox4.Enabled = false;
                        numericUpDown1.Visible = false;
                        numericUpDown2.Visible = true;
                        numericUpDown2.Value = (decimal)changer.Citizens;
                        break;
                    }
                case 2:
                    {
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label5.Visible = false;
                        label6.Visible = false;
                        label4.Visible = true;
                        label7.Visible = false;
                        label8.Visible = true;
                        label9.Visible = true;
                        label10.Visible = true;
                        label11.Visible = true;
                        textBox1.Visible = true;
                        textBox1.Text = changec.Name;
                        textBox2.Visible = true;
                        textBox2.Text = changec.Capital;
                        textBox3.Visible = false;

                        textBox4.Visible = false;
                        comboBox1.Visible = true;
                        comboBox2.Visible = false;
                        comboBox3.Visible = true;
                        comboBox3.Text = changec.Politic;
                        comboBox4.Visible = true;
                        comboBox4.Text = changec.Materic;
                        comboBox4.Enabled = false;
                        numericUpDown1.Visible = true;
                        numericUpDown2.Visible = true;
                        numericUpDown2.Value = (decimal)changec.Citizens;
                        break;
                    }
            }
        }

        public bool GeoposMatch(string s)
        {
            string[] arr = s.Split(' ');
            if (arr.Length != 4) return false;
            try
            {
                string[] arr1 = arr[0].Split('.');
                double y = Convert.ToDouble(arr1[0]);
                y += Convert.ToDouble(arr1[1]) * 0.0166;
                //y = (y / 18) * 43;
                //if (arr[1][0] == 'с') y = -y;
                arr1 = arr[2].Split('.');

                double x = Convert.ToDouble(arr1[0]);
                x += Convert.ToDouble(arr1[1]) * 0.0166;
                if (y > 90 || y < 0 || x > 180 || x < 0) return false;
                if (arr[1] != "с.ш." && arr[1] != "ю.ш.") return false;
                if (arr[3] != "з.д." && arr[3] != "в.д.") return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ChangeGeo()
        {
            //string pattern1 = @"^[А-Я]{1}[а-яА-ЯЁ -]{1,25}$";
            //string pattern2 = @"^[-+]?([0-8]?\d\.[0-5]?\d|90\.(\.0{1,2})?)\s[-+]?(180(\.0{1,2})|((1[0-7]\d)|([0-9]?\d))(\.\d{1,2})?)$";
            int i = comboBox1.SelectedIndex;
            int ind = 0;
            switch (i)
            {
                case 0:
                    {   
                        for(int r = 0; r < catalog.UseTowns.Count; r++)
                        {
                            if(catalog.UseTowns[r].Name==changet.Name)
                            {
                                ind = r;
                                break;
                            }
                        }
                        //Town town = new Town();
                        catalog.UseTowns[ind].Name = textBox1.Text;
                        
                        catalog.UseTowns[ind].Country = textBox3.Text;
                        
                        
                        catalog.UseTowns[ind].Geopos = textBox4.Text;
                        
                        catalog.UseTowns[ind].Citizens = (int)numericUpDown2.Value;
                        catalog.UseTowns[ind].Area = (int)numericUpDown1.Value;
                        
                        catalog.UseTowns[ind].Materic = comboBox4.Text;
                        //catalog.UseTowns.Add(town);
                        break;
                    }
                case 1:
                    {
                        for (int r = 0; r < catalog.UseRegions.Count; r++)
                        {
                            if (catalog.UseRegions[r].Name == changer.Name)
                            {
                                ind = r;
                                break;
                            }
                        }
                        //GRegion region = new GRegion();
                         catalog.UseRegions[ind].Name = textBox1.Text;
                        
                         catalog.UseRegions[ind].Country = textBox3.Text;
                        
                        catalog.UseRegions[ind].Capital = textBox2.Text;
                        
                        catalog.UseRegions[ind].Citizens = (int)numericUpDown2.Value;
                        catalog.UseRegions[ind].Materic = comboBox4.Text;
                        
                        catalog.UseRegions[ind].TypeRegion = comboBox2.Text;
                        //catalog.UseRegions.Add(region);
                        break;
                    }
                case 2:
                    {
                        for (int r = 0; r < catalog.UseCountrys.Count; r++)
                        {
                            if (catalog.UseCountrys[r].Name == changec.Name)
                            {
                                ind = r;
                                break;
                            }
                        }
                        
                         catalog.UseCountrys[ind].Name = textBox1.Text;
                        
                         catalog.UseCountrys[ind].Capital = textBox2.Text;
                        
                        catalog.UseCountrys[ind].Politic = comboBox3.Text;
                        catalog.UseCountrys[ind].Citizens = (int)numericUpDown2.Value;
                        catalog.UseCountrys[ind].Area = (int)numericUpDown1.Value;
                        catalog.UseCountrys[ind].Materic = comboBox4.Text;
                        
                        break;
                    }

            }
            
        }

        public void AddGeo()
        {
            
            //comboBox1_SelectedIndexChanged(sender, e);
            //string pattern1 = @"^[А-Я]{1}[а-яА-ЯЁ -]{2,20}$";
            //string pattern2 = @"^[0-180]\.[0-60] [ю|с]\.ш\. [0-180]\.[0-60] [в|з]\.д\.$";
            int i = comboBox1.SelectedIndex;
            switch (i)
            {
                case 0:
                    {
                        Town town = new Town();
                        town.Name = textBox1.Text;
                        
                         town.Country = textBox3.Text;
                        
                        
                        town.Geopos = textBox4.Text;
                        
                        town.Citizens = (int)numericUpDown2.Value;
                        town.Area = (int)numericUpDown1.Value;
                        
                        town.Materic = comboBox4.Text;
                        catalog.UseTowns.Add(town);
                        bool isset = false;
                        foreach (Country co in catalog.UseCountrys)
                        {
                            if (co.Name == town.Country)
                            {
                                co.Add(town);
                                isset = true;
                                break;
                            }
                        }
                        if (!isset)
                        {
                            Country c = new Country(town.Country, "-", town.Materic, Convert.ToDouble(town.Citizens));
                            c.Add(town);
                            catalog.UseCountrys.Add(c);
                        }
                        break;
                    }
                case 1:
                    {
                        GRegion region = new GRegion();
                         region.Name = textBox1.Text;
                        
                         region.Country = textBox3.Text;
                        
                        region.Capital = textBox2.Text;
                        
                        region.Citizens = (int)numericUpDown2.Value;
                        region.Materic = comboBox4.Text;
                        
                        region.TypeRegion = comboBox2.Text;
                        catalog.UseRegions.Add(region);
                        bool isset = false;
                        foreach (Country co in catalog.UseCountrys)
                        {
                            if (co.Name == region.Country)
                            {
                                co.Add(region);
                                isset = true;
                                break;
                            }
                        }
                        if (!isset)
                        {
                            Country c = new Country(region.Country, "-", region.Materic, Convert.ToDouble(region.Citizens));
                            c.Add(region);
                            catalog.UseCountrys.Add(c);
                        }
                        break;
                    }
                case 2:
                    {
                        Country country = new Country();
                        country.Name = textBox1.Text;
                        
                         country.Capital = textBox2.Text;
                        
                        country.Politic = comboBox3.Text;
                        country.Citizens = (int)numericUpDown2.Value;
                        country.Area = (int)numericUpDown1.Value;
                        country.Materic = comboBox4.Text;
                        
                        catalog.UseCountrys.Add(country);
                        break;
                    }

            }

        }

        public Catalog UseCatalog
        {
            set
            {
                catalog = value;
            }
            get
            {
                return catalog;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.DialogResult == DialogResult.None)
                if (!maintoken) MessageBox.Show("Указанные поля заполнены неверно");

                
                else MessageBox.Show("Необходимо заполнить обязательные поля.");
                
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            switch (i)
            {
                case 0:
                    {
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label5.Visible = true;
                        label6.Visible = true;
                        label4.Visible = false;
                        label7.Visible = false;
                        label8.Visible = false;
                        label10.Visible = true;
                        label9.Visible = true;
                        label11.Visible = true;
                        textBox1.Visible = true;
                        textBox2.Visible = false;
                        textBox3.Visible = true;
                        textBox4.Visible = true;
                        comboBox1.Visible = true;
                        comboBox2.Visible = false;
                        comboBox3.Visible = false;
                        comboBox4.Visible = true;
                        
                        numericUpDown1.Visible = true;
                        numericUpDown2.Visible = true;
                        button3.Visible = true;
                        break;
                    }
                case 1:
                    {
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label5.Visible = true;
                        label6.Visible = false;
                        label4.Visible = true;
                        label7.Visible = true;
                        label9.Visible = true;
                        label8.Visible = false;
                        label10.Visible = false;
                        label11.Visible = true;
                        textBox1.Visible = true;
                        textBox2.Visible = true;
                        textBox3.Visible = true;
                        textBox4.Visible = false;
                        comboBox1.Visible = true;
                        comboBox2.Visible = true;
                        comboBox3.Visible = false;
                        comboBox4.Visible = true;
                        numericUpDown1.Visible = false;
                        numericUpDown2.Visible = true;
                        button3.Visible = false;
                        break;
                    }
                case 2:
                    {
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label5.Visible = false;
                        label6.Visible = false;
                        label4.Visible = true;
                        label7.Visible = false;
                        label8.Visible = true;
                        label9.Visible = true;
                        label10.Visible = true;
                        label11.Visible = true;
                        textBox1.Visible = true;
                        textBox2.Visible = true;
                        textBox3.Visible = false;
                        textBox4.Visible = false;
                        comboBox1.Visible = true;
                        comboBox2.Visible = false;
                        comboBox3.Visible = true;
                        comboBox4.Visible = true;
                        numericUpDown1.Visible = true;
                        numericUpDown2.Visible = true;
                        button3.Visible = false;
                        break;
                    }
            }
        } 

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string pattern1 = @"^[А-Я]{1}[а-яё]{1,12}((\s|\-|\')[А-Яа-яё]{1}[а-яё]{1,12}){0,3}$";
            if (Regex.IsMatch(textBox1.Text, pattern1))
            {
                //textBox1.BackColor = SystemColors.Window;
                permtokens[0] = true;
            }
            if (permtokens[0] == true && permtokens[1] == true && permtokens[2] == true && permtokens[3] == true)
            {
                maintoken = true;
                //button1.DialogResult = DialogResult.OK;
            }
            if (maintoken && comboBox1.Text != "" && comboBox4.Text != "" && numericUpDown2.Value != 0)
            {
                button1.DialogResult = DialogResult.OK;

            }
            else button1.DialogResult = DialogResult.None;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (maintoken && comboBox1.Text != "" && comboBox4.Text != "" && numericUpDown2.Value != 0)
            {
                button1.DialogResult = DialogResult.OK;

            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (maintoken && comboBox1.Text != "" && comboBox4.Text != "" && numericUpDown2.Value != 0)
            {
                button1.DialogResult = DialogResult.OK;

            }
            else button1.DialogResult = DialogResult.None;
        }
        
        private void textBox1_Leave(object sender, EventArgs e)
        {
            string pattern1 = @"^[А-Я]{1}[а-яё]{1,12}((\s|\-|\')[А-Яа-яё]{1}[а-яё]{1,12}){0,3}$";
            if (Regex.IsMatch(textBox1.Text, pattern1))
            {
                textBox1.BackColor = SystemColors.Window;
                permtokens[0] = true;
                if (permtokens[0] == true && permtokens[1] == true && permtokens[2] == true && permtokens[3] == true)
                {
                    maintoken = true;
                    //button1.DialogResult = DialogResult.OK;
                }
                else
                {
                    button1.DialogResult = DialogResult.None;
                    maintoken = false;
                }
            }
            else
            {
                permtokens[0] = false;
                maintoken = false;
                textBox1.BackColor = Color.IndianRed;
                button1.DialogResult = DialogResult.None;
            }
            if (maintoken && comboBox1.Text != "" && comboBox4.Text != "" && numericUpDown2.Value != 0)
            {
                button1.DialogResult = DialogResult.OK;

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            string pattern1 = @"^[А-Я]{1}[а-яё]{1,12}((\s|\-|\')[А-Яа-я]{1}[а-яё]{1,12}){0,3}$";
            if (textBox2.Text=="" || Regex.IsMatch(textBox2.Text, pattern1))
            {
                textBox2.BackColor = SystemColors.Window;
                permtokens[1] = true;
                if (permtokens[0] == true && permtokens[1] == true && permtokens[2] == true && permtokens[3] == true)
                {
                    maintoken = true;
                    //button1.DialogResult = DialogResult.OK;
                }
                else
                {
                    button1.DialogResult = DialogResult.None;
                    maintoken = false;
                }
            }
            else
            {
                permtokens[1] = false;
                maintoken = false;
                textBox2.BackColor = Color.IndianRed;
                button1.DialogResult = DialogResult.None;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            string pattern1 = @"^([А-Я]{1}[а-яё]{1,12}((\s|\-|\')[А-Яа-я]{1}[а-яё]{1,12}){0,3}|[А-Я]{2,5})$";
            if (textBox3.Text == "" || Regex.IsMatch(textBox3.Text, pattern1))
            {
                bool flag = false;
                foreach (Country temp in catalog.UseCountrys)
                {
                    if (textBox3.Text == temp.Name)
                    {
                        comboBox4.SelectedIndex = array[temp.Materic];
                        flag = true;
                        comboBox4.Enabled = false;
                        break;
                    }
                }
                if (!flag)
                {
                    comboBox4.Enabled = true;
                }
                textBox3.BackColor = SystemColors.Window;
                permtokens[2] = true;
                if (permtokens[0] == true && permtokens[1] == true && permtokens[2] == true && permtokens[3] == true)
                {
                    maintoken = true;
                    //button1.DialogResult = DialogResult.OK;
                }
                else
                {
                    button1.DialogResult = DialogResult.None;
                    maintoken = false;
                }
            }
            else
            {
                permtokens[2] = false;
                maintoken = false;
                textBox3.BackColor = Color.IndianRed;
                button1.DialogResult = DialogResult.None;
            }
            if (maintoken && comboBox1.Text != "" && comboBox4.Text != "" && numericUpDown2.Value != 0)
            {
                button1.DialogResult = DialogResult.OK;

            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || GeoposMatch(textBox4.Text))
            {
                textBox4.BackColor = SystemColors.Window;
                permtokens[3] = true;
                if (permtokens[0] == true && permtokens[1] == true && permtokens[2] == true && permtokens[3] == true)
                {
                    maintoken = true;
                    //button1.DialogResult = DialogResult.OK;
                }
                else
                {
                    button1.DialogResult = DialogResult.None;
                    maintoken = false;
                }
            }
            else
            {
                permtokens[3] = false;
                maintoken = false;
                textBox4.BackColor = Color.IndianRed;
                button1.DialogResult = DialogResult.None;
            }
            if (maintoken && comboBox1.Text != "" && comboBox4.Text != "" && numericUpDown2.Value != 0)
            {
                button1.DialogResult = DialogResult.OK;

            }
        }

        private void Add_ChangeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult d = MessageBox.Show("Вы уверены?", "Выход", MessageBoxButtons.OKCancel);
            if (d == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Form2 f = new Form2();
            f.ShowDialog();
            string s = f.getpos();
            textBox4.Text = s;
        }
    }
    
}
