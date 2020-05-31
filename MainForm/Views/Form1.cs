using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MainForm.Models;
using System.IO;
using MainForm.Views;

namespace MainForm

{
    public partial class Form1 : Form
    {
        BindingSource bs = new BindingSource();
        Catalog catalog;
        public bool change = false;
        Town t;
        GRegion r;
        Country c;
        string path;
        int kindof;
        List<Town> towns;
        List<GRegion> regions;
        List<Country> countrys;
        public Form1()
        {
            InitializeComponent();
            EnUnableControl(false);
            kindof = 0;
        }
        private void EnUnableControl(bool t)
        {
            searchcomboBox1.Enabled = t;
            if (t == true) searchcomboBox1.SelectedIndex = 0;
            comboBox1.Enabled = t;
            comboBox2.Enabled = t;
            comboBox3.Enabled = t;
            textBox1.Enabled = t;
            textBox2.Enabled = t;
            textBox3.Enabled = t;
        } 
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            path ="";
            try
            {

                //openFileDialog1.FileName = "" + ".txt";
                openFileDialog1.Filter = "TXT файлы,(*.txt)|*.txt";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    path = openFileDialog1.FileName;
                catalog = new Catalog(path);
                EnUnableControl(true);
            }
            catch (Exception)
            {
                MessageBox.Show("Файл не был выбран");
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (catalog == null)
            {
                MessageBox.Show("Сначала выберите файл для считывания");
            }
            else
            {
                catalog.Write(catalog.Path);
                change = false;
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (catalog == null)
            {
                MessageBox.Show("Сначала выберите файл для считывания");
                return;
            }
            saveFileDialog1.FileName = "Geobjects" + ".txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Text = saveFileDialog1.FileName;
                catalog.Write(saveFileDialog1.FileName);
                change = false;
            }
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Write(string path)
        {
            using (TextWriter tw = new StreamWriter(path, false, Encoding.GetEncoding(1251)))
            {
                for (int i = 0; i < catalog.UseTowns.Count; i++)
                {
                    tw.WriteLine("Город" +"|"+catalog.UseTowns[i].Name + "|" + catalog.UseTowns[i].Country + "|" + catalog.UseTowns[i].Materic + "|" + catalog.UseTowns[i].Citizens + "|" + catalog.UseTowns[i].Geopos + "|" + catalog.UseTowns[i].Area);
                }
                
                for (int i = 0; i < catalog.UseRegions.Count; i++)
                {
                    tw.WriteLine("Регион" + "|" + catalog.UseRegions[i].Name + "|" + catalog.UseRegions[i].Country + "|" + catalog.UseRegions[i].Materic + "|" + catalog.UseRegions[i].Citizens + "|" + catalog.UseRegions[i].TypeRegion + "|" + catalog.UseRegions[i].Capital);
                }

                for (int i = 0; i < catalog.UseCountrys.Count; i++)
                {
                    tw.WriteLine("Страна" + "|" + catalog.UseCountrys[i].Name + "|" + catalog.UseCountrys[i].Capital + "|" + catalog.UseCountrys[i].Materic + "|" + catalog.UseCountrys[i].Citizens + "|" + catalog.UseCountrys[i].Politic + "|" + catalog.UseCountrys[i].Area);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (catalog == null)
            {
                MessageBox.Show("Сначала выберите файл для считывания");
                return;
            }
            int geotype = searchcomboBox1.SelectedIndex;
            switch (geotype)
            {
                case 0:
                    {
                        SearchTowns();
                        kindof = 1;
                        break;
                    }
                case 1:
                    {
                        SearchRegions();
                        kindof = 2;
                        break;
                    }
                case 2:
                    {
                        SearchCountrys();
                        kindof = 3;
                        break;
                    }
            }
        }

        private void SearchTowns()
        {
            catalog.FixTowns();
            List<Town> sList = new List<Town>();
            for (int i = 0; i < catalog.UseTowns.Count; i++)
            {
                if (catalog.UseTowns[i].Name.ToLower().IndexOf(textBox1.Text.ToLower()) != -1)
                {
                    if (catalog.UseTowns[i].Country.ToLower().IndexOf(textBox3.Text.ToLower()) != -1)
                    {
                        if(catalog.UseTowns[i].Materic.ToLower().IndexOf(comboBox1.Text.ToLower()) != -1 ||comboBox1.Text=="Все")
                        {
                            //Town ins = catalog.UseTowns[i];
                            //dataGridView1.Rows.Add(ins.Name,ins.Country,ins.Materic,ins.Citizens,ins.Geopos,ins.Area);
                            sList.Add(catalog.UseTowns[i]);
                        }
                    }
                }
            }
            towns = sList;
            bs.DataSource = sList;
            dataGridView1.DataSource = bs;
            dataGridView1.Columns[0].HeaderText = "Название";
            dataGridView1.Columns[1].HeaderText = "Страна";
            dataGridView1.Columns[2].HeaderText = "Принадлежность материку";
            dataGridView1.Columns[3].HeaderText = "Население, тыс. чел.";
            dataGridView1.Columns[4].HeaderText = "Географические координаты";
            dataGridView1.Columns[5].HeaderText = "Площадь, кв.км.";
        }
        private void SearchRegions()
        {
            catalog.FixRegions();
            List<GRegion> sList = new List<GRegion>();
            for (int i = 0; i < catalog.UseRegions.Count; i++)
            {
                if (catalog.UseRegions[i].Name.ToLower().IndexOf(textBox1.Text.ToLower()) != -1)
                {
                    if (catalog.UseRegions[i].Country.ToLower().IndexOf(textBox3.Text.ToLower()) != -1)
                    {
                        if (catalog.UseRegions[i].Materic.ToLower().IndexOf(comboBox1.Text.ToLower()) != -1 || comboBox1.Text == "Все")
                        {
                            if (catalog.UseRegions[i].Capital.ToLower().IndexOf(textBox2.Text.ToLower()) != -1)
                            {
                                if (catalog.UseRegions[i].TypeRegion.ToLower().IndexOf(comboBox3.Text.ToLower()) != -1 || comboBox3.Text == "Любые")
                                {
                                    sList.Add(catalog.UseRegions[i]);
                                }
                            }
                        }
                    }
                }
            }
            regions = sList;
            bs.DataSource = sList;
            dataGridView1.DataSource = bs;
            dataGridView1.Columns[0].HeaderText = "Название";
            dataGridView1.Columns[1].HeaderText = "Страна";
            dataGridView1.Columns[2].HeaderText = "Принадлежность материку";
            dataGridView1.Columns[3].HeaderText = "Население, тыс. чел.";
            dataGridView1.Columns[4].HeaderText = "Тип региона";
            dataGridView1.Columns[5].HeaderText = "Столица";
        }
        private void SearchCountrys()
        {
            
            List<Country> sList = new List<Country>();
            for (int i = 0; i < catalog.UseCountrys.Count; i++)
            {
                if (catalog.UseCountrys[i].Name.ToLower().IndexOf(textBox1.Text.ToLower()) != -1)
                {
                    if (catalog.UseCountrys[i].Politic.ToLower().IndexOf(comboBox2.Text.ToLower()) != -1 || comboBox2.Text == "Любые")
                    {
                        if (catalog.UseCountrys[i].Materic.ToLower().IndexOf(comboBox1.Text.ToLower()) != -1 || comboBox1.Text == "Все")
                        {
                            if (catalog.UseCountrys[i].Capital.ToLower().IndexOf(textBox2.Text.ToLower()) != -1)
                            {
                                sList.Add(catalog.UseCountrys[i]);                                
                            }
                        }
                    }
                }
            }
            countrys = sList;
            bs.DataSource = sList;
            
            dataGridView1.DataSource = bs;
            dataGridView1.Columns[0].HeaderText = "Название";
            dataGridView1.Columns[1].HeaderText = "Столица";
            dataGridView1.Columns[2].HeaderText = "Принадлежность материку";
            dataGridView1.Columns[3].HeaderText = "Население, тыс. чел.";
            dataGridView1.Columns[4].HeaderText = "Форма правления";
            dataGridView1.Columns[5].HeaderText = "Площадь, кв. км.";
        }

        private void searchcomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = searchcomboBox1.SelectedIndex;
            switch (i)
            {
                case 0:
                    {
                        button2.Visible = true;
                        label1.Visible = true;          
                        label2.Visible = true;          
                        label3.Visible = true;
                        label4.Visible = false;
                        label5.Visible = false;
                        label6.Visible = false;
                        label7.Visible = true;
                        textBox2.Visible = false;
                        textBox3.Visible = true;
                        comboBox2.Visible = false;
                        comboBox3.Visible = false;
                        /*dataGridView1.Columns[1].HeaderText = "Страна";
                        dataGridView1.Columns[2].HeaderText = "Принадлежность материку";
                        dataGridView1.Columns[3].HeaderText = "Население, тыс. чел.";
                        dataGridView1.Columns[4].HeaderText = "Географические координаты";
                        dataGridView1.Columns[5].HeaderText = "Площадь";*/
                        break;
                    }

                case 1:
                    {
                        button2.Visible = false;
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label4.Visible = true;
                        label5.Visible = false;
                        label6.Visible = true;
                        label7.Visible = true;
                        textBox2.Visible = true;
                        textBox3.Visible = true;
                        comboBox2.Visible = false;
                        comboBox3.Visible = true;
                        
                        break;
                    }

                case 2:
                    {
                        button2.Visible = false;
                        label1.Visible = true;
                        label2.Visible = true;
                        label3.Visible = true;
                        label4.Visible = true;
                        label5.Visible =true;
                        label6.Visible = false;
                        label7.Visible = false;
                        textBox2.Visible = true;
                        textBox3.Visible = false;
                        comboBox2.Visible = true;
                        comboBox3.Visible = false;
                        /*dataGridView1.Columns[1].HeaderText = "Столица";
                        dataGridView1.Columns[2].HeaderText = "Принадлежность материку";
                        dataGridView1.Columns[3].HeaderText = "Население, тыс. чел.";
                        dataGridView1.Columns[4].HeaderText = "Форма правления";
                        dataGridView1.Columns[5].HeaderText = "Площадь";*/
                        break;
                    }
                

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dataGridView1.AutoGenerateColumns = false;
        }

        private void addGeobject_Click(object sender, EventArgs e)
        {
            
            if (catalog == null)
            {
                MessageBox.Show("Сначала выберите файл для считывания");
                return;
            }
            Add_ChangeForm form2 = new Add_ChangeForm(catalog);
            form2.Text = "Добавление нового географического объекта";
            if (form2.ShowDialog() == DialogResult.OK)
            {
                form2.AddGeo();
                change = true;
            }
            catalog = form2.UseCatalog;
            button1_Click(sender, e);
            if (DialogResult == DialogResult.OK)
            {
                MessageBox.Show("Добавление элемента прошло успешно.");
            }
        }

        private void changeGeobject_Click(object sender, EventArgs e)
        {
            if (catalog == null)
            {
                MessageBox.Show("Сначала выберите файл для считывания");
                return;
            }
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Cells[1]==null)
            {
                MessageBox.Show("Не выбран элемент для изменения.");
                return;
            }
            int i = searchcomboBox1.SelectedIndex;
            
            switch (i)
            {
                case 0:
                    {
                        t = new Town();
                        t.Name = dataGridView1.SelectedCells[0].Value.ToString();
                        t.Country = dataGridView1.SelectedCells[1].Value.ToString();
                        t.Materic = dataGridView1.SelectedCells[2].Value.ToString();
                        t.Citizens = Convert.ToDouble(dataGridView1.SelectedCells[3].Value.ToString());
                        t.Geopos = dataGridView1.SelectedCells[4].Value.ToString(); 
                        t.Area = Convert.ToDouble(dataGridView1.SelectedCells[5].Value.ToString());
                        r = null;
                        c = null;
                        break;
                    }
                case 1:
                    {
                        r = new GRegion();
                        r.Name = dataGridView1.SelectedCells[0].Value.ToString();
                        r.Country = dataGridView1.SelectedCells[1].Value.ToString();
                        r.Materic = dataGridView1.SelectedCells[2].Value.ToString();
                        r.Citizens = Convert.ToDouble(dataGridView1.SelectedCells[3].Value.ToString());
                        r.TypeRegion = dataGridView1.SelectedCells[4].Value.ToString();
                        r.Capital = dataGridView1.SelectedCells[5].Value.ToString();
                        t = null;
                        c = null;
                        break;
                    }
                case 2:
                    {
                        c = new Country();
                        c.Name = dataGridView1.SelectedCells[0].Value.ToString();
                        c.Capital = dataGridView1.SelectedCells[1].Value.ToString();
                        c.Materic = dataGridView1.SelectedCells[2].Value.ToString();
                        c.Citizens = Convert.ToDouble(dataGridView1.SelectedCells[3].Value.ToString());
                        c.Politic = dataGridView1.SelectedCells[4].Value.ToString();
                        c.Area = Convert.ToDouble(dataGridView1.SelectedCells[5].Value.ToString());
                        r = null;
                        t = null;
                        break;
                    }
            }
            Add_ChangeForm form2 = new Add_ChangeForm(catalog,t,r,c);
            form2.Text = "Изменение географического объекта";
            if (form2.ShowDialog() == DialogResult.OK)
            {
                form2.ChangeGeo();
                change = true;
            }
            catalog = form2.UseCatalog;
            button1_Click(sender, e);
            if (DialogResult == DialogResult.OK)
            {
                MessageBox.Show("Изменение элемента прошло успешно.");
            }
        }

        private void deleteGeobject_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null )
            {
                MessageBox.Show("Не выбран элемент для удаления.");
                return;
            }
            try
            {
                DialogResult d = MessageBox.Show("Вы уверены, что хотите удалить " + dataGridView1.SelectedCells[0].Value.ToString() + "?", "Выход", MessageBoxButtons.YesNo);
                if (d == DialogResult.No)
                    return;
                else
                {
                    string name = dataGridView1.SelectedCells[0].Value.ToString();
                    string coun = dataGridView1.SelectedCells[1].Value.ToString();
                    switch (searchcomboBox1.SelectedIndex)
                    {
                        case 0:
                            {

                                for (int i = 0; i < catalog.UseTowns.Count; i++)
                                {
                                    if (catalog.UseTowns[i].Name == name || catalog.UseTowns[i].Country == coun)
                                    {
                                        catalog.UseTowns.RemoveAt(i);
                                        foreach(Country cout in catalog.UseCountrys)
                                        {
                                            if (cout.Name == coun)
                                            {
                                                for(int p=0;p<cout.Towns().Count;p++)
                                                {
                                                    if (cout.Towns()[i].Name == name) cout.Towns().RemoveAt(i);
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        break;
                                    }
                                }
                                dataGridView1.DataSource = catalog.UseTowns;
                                button1_Click(sender, e);
                                MessageBox.Show("Удаление элемента прошло успешно.");
                                break;
                            }
                        case 1:
                            {

                                for (int i = 0; i < catalog.UseRegions.Count; i++)
                                {
                                    if (catalog.UseRegions[i].Name == name || catalog.UseRegions[i].Country == coun)
                                    {
                                        catalog.UseRegions.RemoveAt(i);
                                        foreach (Country cout in catalog.UseCountrys)
                                        {
                                            if (cout.Name == coun)
                                            {
                                                for (int p = 0; p < cout.GRegions().Count; p++)
                                                {
                                                    if (cout.GRegions()[i].Name == name) cout.GRegions().RemoveAt(i);
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        break;
                                    }
                                }
                                dataGridView1.DataSource = catalog.UseRegions;
                                button1_Click(sender, e);
                                MessageBox.Show("Удаление элемента прошло успешно.");
                                break;
                            }
                        case 2:
                            {

                                for (int i = 0; i < catalog.UseCountrys.Count; i++)
                                {
                                    if (catalog.UseCountrys[i].Name == name || catalog.UseCountrys[i].Capital == coun)
                                    {
                                        catalog.UseCountrys.RemoveAt(i);
                                        break;
                                    }
                                }
                                dataGridView1.DataSource = catalog.UseCountrys;
                                button1_Click(sender, e);
                                MessageBox.Show("Удаление элемента прошло успешно.");
                                break;
                            }

                    }
                    change = true;

                }
            }
            catch(Exception)
            {
                MessageBox.Show("Не выбран элемент для удаления.");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.SelectedCells[4] == null)
            {
                MessageBox.Show("Не выбран элемент для отображения.");
                return;
            }
            string pos = dataGridView1.SelectedCells[4].Value.ToString();
            if (pos == "-")
            {
                MessageBox.Show("Координаты данного города не указаны");
                return;
            }
            Form2 f = new Form2(pos);
            f.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (change == true)
                {
                    DialogResult d = MessageBox.Show("Сохранить изменения в файле?", "Выход", MessageBoxButtons.YesNoCancel);
                    if (d == DialogResult.Cancel)
                        e.Cancel = true;
                    if (d == DialogResult.Yes)
                        сохранитьToolStripMenuItem_Click(sender, e);
                }
            else
                {
                    DialogResult d = MessageBox.Show("Закрыть программу?", "Выход", MessageBoxButtons.OKCancel);
                    if (d == DialogResult.OK)
                    {
                        e.Cancel = false;
                    }
                    else
                        e.Cancel = true;
                }

            
        }

        private void сводкаПоМатерикамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (catalog == null)
            {
                MessageBox.Show("Сначала выберите файл для считывания");
                return;
            }
            double[] mater =new double[] { 0, 0, 0, 0, 0 ,0};
            
            for (int i = 0; i < catalog.UseCountrys.Count; i++)
            {
                switch (catalog.UseCountrys[i].Materic.ToLower())
                {
                    case "евразия":
                        {
                            mater[0] += catalog.UseCountrys[i].Citizens;
                            break;
                        }
                    case "африка":
                        {
                            mater[1] += catalog.UseCountrys[i].Citizens;
                            break;
                        }
                    case "северная америка":
                        {
                            mater[2] += catalog.UseCountrys[i].Citizens;
                            break;
                        }
                    case "южная америка":
                        {
                            mater[3] += catalog.UseCountrys[i].Citizens;
                            break;
                        }
                    case "австралия":
                        {
                            mater[4] += catalog.UseCountrys[i].Citizens;
                            break;
                        }
                }
            }
            string message = "Евразия - " + mater[0] + " чел.\n"+ "Африка - " + mater[1] + " чел.\n"+
                "Северная Америка - " + mater[2] + " чел.\n"+ "Южная Америка - " + mater[3] + " чел.\n"+
                "Австралия - " + mater[4] + " чел.\n"+"Антарктида - "+ mater[5]+ " чел.\n";
            MessageBox.Show(message, "Сводка", MessageBoxButtons.OK);
        }

        private void HelpForm(object sender, HelpEventArgs hlpevent)
        {
            using (var helpform=new HelpForm())
            {
                helpform.ShowDialog();
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var helpform = new HelpForm())
            {
                helpform.ShowDialog();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (catalog == null)
            {
                MessageBox.Show("Сначала выберите файл для считывания");
                return;
            }
            if (kindof == 0) return;
            saveFileDialog1.FileName = "Geobjects" + ".txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Text = saveFileDialog1.FileName;
                switch (kindof){
                    case 1:
                        {
                            catalog.Write(saveFileDialog1.FileName,towns);
                            break;
                        }
                    case 2:
                        {
                            catalog.Write(saveFileDialog1.FileName, regions);
                            break;
                        }
                    case 3:
                        {
                            catalog.Write(saveFileDialog1.FileName, countrys);
                            break;
                        }
                }
                
            }
        }
    }
}
