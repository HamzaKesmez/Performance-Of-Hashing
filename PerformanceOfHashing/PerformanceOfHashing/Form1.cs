using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerformanceOfHashing
{
    public struct veri
    {
       public int key;
       public int link;
        public veri(int a)
        {
            key = a;
            link = -1;
        }
    }


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random random = new Random();
        public static bool BlYukari = true;
        public static bool BeYukari = true;
        public static int cakismaSayisi = 0;
        int[] randDizi = new int[10000];
        private long GetMemoryUsageInBytes()
        {
            Process currentProcess = Process.GetCurrentProcess();
            return currentProcess.WorkingSet64;
        }
        private void randomDeger()
        {
            for (int i = 0; i < 10000; i++)
            {
                randDizi[i] = random.Next(100, 10000);
            }
        }

        public void LischEkle(int keys, veri[] dizi)
        {
            int index=keys%dizi.Length;
            if (dizi[index].key==0)
            {
                dizi[index].key = keys;
            }
            else
            {   
                int dataplace=keys%dizi.Length;
                cakismaSayisi++;
                int a = -1;
                for(int i=0;i<dizi.Length; i++)
                {
                    if (dizi[dataplace].key == 0)
                    {
                        break;
                    }
                    dataplace++;
                    dataplace=dataplace%dizi.Length;

                }
                while (dizi[index].link != -1)
                {
                    index = dizi[index].link;
                }
                dizi[index].link = dataplace;
                dizi[dataplace].key = keys;

            }
        }
        public void EischEkle(int keys, veri[] dizi)
        {
            int index = keys % dizi.Length;
            if (dizi[index].key == 0)
            {
                dizi[index].key = keys;
            }
            else
            {
                int dataplace = keys / dizi.Length;
                cakismaSayisi++;

                for (int i = 0; i < dizi.Length; i++)
                {
                    if (dizi[dataplace].key == 0)
                    {
                        break;
                    }
                    dataplace++;
                    dataplace= dataplace%dizi.Length;
                }
                while (dizi[index].link != -1)
                {
                    index = dizi[index].link;
                }

                dizi[dataplace].key = keys;
                dizi[dataplace].link = dizi[index].link;
                dizi[index].link = dataplace;

            }
        }
        public void LichEkle(int keys, veri[] dizi)
        {
            int index = keys % (int)((double)dizi.Length * 0.8);
            if (dizi[index].key == 0)
            {
                dizi[index].key = keys;
            }
            else
            {
                int dataplace = (dizi.Length-1);
                cakismaSayisi++;
                while(dataplace!=0 || dizi[dataplace].link== -1)
                {
                    if (dizi[dataplace].key == 0)
                    {
                        break;
                    }
                    dataplace--;
                }
                while (dizi[index].link != -1)
                {
                    index = dizi[index].link;
                }
                dizi[index].link = dataplace;
                dizi[dataplace].key = keys;
            }
        }
        public void EichEkle(int keys, veri[] dizi)
        {
            int index = keys % (int)((double)dizi.Length * 0.8);
            if (dizi[index].key == 0)
            {
                dizi[index].key = keys;
            }
            else
            {
                cakismaSayisi++;
                int a = -1;
                for (int i = (dizi.Length - 1); i >= 0; i--)
                {
                    if (dizi[i].key == 0)
                    {
                        a = i;
                    }
                }
                dizi[a].key = keys;
                dizi[a].link = dizi[index].link;
                dizi[index].link = a;
                
            }
        }
        public void RlischEkle(int keys, veri[] dizi)
        {
            List<int>BosIndex = new List<int>();

            int index = keys % dizi.Length;
            if (dizi[index].key == 0)
            {
                dizi[index].key = keys;
            }
            else
            {   
                cakismaSayisi++;
                for (int i = 0; i < dizi.Length; i++)
                {
                    if (dizi[i].key == 0)
                    {
                       BosIndex.Add(i);
                    }
                }

                int a=BosIndex[random.Next(0,BosIndex.Count)];

                while (dizi[index].link != -1)// zincirin sonuna gelmek için
                {
                    index = dizi[index].link;
                }
                
                dizi[index].link = a;
                dizi[a].key=keys;

            }
        }
        public void ReischEkle(int keys, veri[] dizi)
        {
            List<int> BosIndex = new List<int>();

            int index = keys % dizi.Length;
            if (dizi[index].key == 0)
            {
                dizi[index].key = keys;
            }
            else
            {
                cakismaSayisi++;
                for (int i = 0; i < dizi.Length; i++)
                {
                    if (dizi[i].key == 0)
                    {
                        BosIndex.Add(i);
                    }
                }

                int a = BosIndex[random.Next(0, BosIndex.Count)];

                dizi[a].key= keys; /// zincirin başına ekleme yaptık 
                dizi[a].link = dizi[index].link;
                dizi[index].link= a;

            }
        }
        public void BlischEkle(int keys, veri[] dizi)
        {
            int index = keys % dizi.Length;
            if (dizi[index].key == 0)
            {
                dizi[index].key = keys;
            }
            else
            {   
                cakismaSayisi++;
                int a = -1;
                if (BlYukari)
                {
                    for (int i = 0; i < dizi.Length; i++)
                    {
                        if (dizi[i].key == 0)
                        {
                            a = i;
                            break;
                        }
                    }
                    BlYukari = !BlYukari;
                }
                else
                {
                    for (int i = (dizi.Length-1); i >= 0; i--)
                    {
                        if (dizi[i].key == 0)
                        {
                            a = i;
                            break;
                        }
                    }
                    BlYukari = !BlYukari;
                }
                while (dizi[index].link != -1)// zincirin sonuna gelmek için
                {
                    index = dizi[index].link;
                }
                dizi[index].link = a;
                dizi[a].key = keys;
            }
        }
        public void BeischEkle(int keys, veri[] dizi)
        {
            int index = keys % dizi.Length;
            if (dizi[index].key == 0)
            {
                dizi[index].key = keys;
            }
            else
            {
                cakismaSayisi++;
                int a = -1;
                if (BeYukari)
                {
                    for (int i = 0; i < dizi.Length; i++)
                    {
                        if (dizi[i].key == 0)
                        {
                            a = i;
                            break;
                        }
                    }
                    BeYukari = !BeYukari;
                }
                else
                {
                    for (int i = (dizi.Length-1); i >= 0; i--)
                    {
                        if (dizi[i].key == 0)
                        {
                            a = i;
                            break;
                        }
                    }
                    BeYukari = !BeYukari;
                }

                dizi[a].key = keys;
                dizi[a].link = dizi[index].link;
                dizi[index].link = a;

            }
        }

        string AlgoritmaAdlarınıAl(int index)
        {
            switch (index)
            {
                case 0:
                    return "Lisch";
                case 1:
                    return "Eisch";
                case 2:
                    return "Lich";
                case 3:
                    return "Eich";
                case 4:
                    return "Rlisch";
                case 5:
                    return "Reisch";
                case 6:
                    return "Blisch";
                case 7:
                    return "Beisch";
                default:
                    return "Unknown";
            }
        }
        private void basla_Click(object sender, EventArgs e)
        {
            

            randomDeger();
            veri[] dizi = new veri[10000];
            for(int i = 0; i < dizi.Length; i++)
            {
                dizi[i] = new veri();
                dizi[i].link = -1;
                dizi[i].key = 0;
            }
            Stopwatch stopwatch = new Stopwatch();

            if (comboBox1.SelectedItem.ToString() == "Süre")
            {
                chart1.Series["Algoritmalar"].Points.Clear();
                chart1.Titles.Clear();

                stopwatch.Start();
                for(int i = 0;i<10000;i++)
                {
                    LischEkle(randDizi[i], dizi);
                }
               
                stopwatch.Stop();
                double lischElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }

                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    EischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                double eischElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }

                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    LichEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                double lichElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }

                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    EichEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                double eichElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }

                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    RlischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                double rlischElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }

                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    ReischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                double reischElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }

                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    BlischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                double blischElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }

                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    BeischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                double beischElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }

                stopwatch.Reset();

  


                this.chart1.Titles.Add("Algoritmaların Çalışma Hızı");
                this.chart1.Series["Algoritmalar"].Points.AddXY("Lisch", lischElapsedTime);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Eisch", eischElapsedTime);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Lich", lichElapsedTime);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Eich", eichElapsedTime);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Rlisch", rlischElapsedTime);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Reisch", reischElapsedTime);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Blisch", blischElapsedTime);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Beisch", beischElapsedTime);
            }

            else if (comboBox1.SelectedItem.ToString() == "Bellek Mik.")
            {
                chart1.Series["Algoritmalar"].Points.Clear();
                chart1.Titles.Clear();

                List<double> runtimes = new List<double>();
                List<long> memoryUsages = new List<long>();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    LischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                runtimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                memoryUsages.Add(GetMemoryUsageInBytes());
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    EischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                runtimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                memoryUsages.Add(GetMemoryUsageInBytes());
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    LichEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                runtimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                memoryUsages.Add(GetMemoryUsageInBytes());
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    EichEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                runtimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                memoryUsages.Add(GetMemoryUsageInBytes());
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    RlischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                runtimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                memoryUsages.Add(GetMemoryUsageInBytes());
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    ReischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                runtimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                memoryUsages.Add(GetMemoryUsageInBytes());
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    BlischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                runtimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                memoryUsages.Add(GetMemoryUsageInBytes());
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    BeischEkle(randDizi[i], dizi);
                }
                stopwatch.Stop();
                runtimes.Add(stopwatch.Elapsed.TotalMilliseconds);
                memoryUsages.Add(GetMemoryUsageInBytes());
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }
                stopwatch.Reset();
                
                chart1.Titles.Add("Algoritmaların Bellek Kullanımı");


                for (int i = 0; i < runtimes.Count; i++)
                {
                    double totalMemory = memoryUsages.Sum();
                    double percentage = (memoryUsages[i] / totalMemory) * 100;
                    string algoritmaAdı = AlgoritmaAdlarınıAl(i);

                    chart1.Series["Algoritmalar"].Points.AddXY(algoritmaAdı, percentage);
                }
            }

            else if (comboBox1.SelectedItem.ToString() == "Adım Sayısı")
            {
                chart1.Series["Algoritmalar"].Points.Clear();
                chart1.Titles.Clear();

                cakismaSayisi = 0;
                for (int i = 0; i < 10000; i++)
                {
                    LischEkle(randDizi[i], dizi);
                }
                int lischCakismaSayisi = cakismaSayisi;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }


                cakismaSayisi = 0;
                for (int i = 0; i < 10000; i++)
                {
                    EischEkle(randDizi[i], dizi);
                }
                int eischCakismaSayisi = cakismaSayisi;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }


                cakismaSayisi = 0;
                for (int i = 0; i < 10000; i++)
                {
                    LichEkle(randDizi[i], dizi);
                }
                int lichCakismaSayisi = cakismaSayisi;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }


                cakismaSayisi = 0;
                for (int i = 0; i < 10000; i++)
                {
                    EichEkle(randDizi[i], dizi);
                }
                int eichCakismaSayisi = cakismaSayisi;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }


                cakismaSayisi = 0;
                for (int i = 0; i < 10000; i++)
                {
                    RlischEkle(randDizi[i], dizi);
                }
                int rlischCakismaSayisi = cakismaSayisi;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }


                cakismaSayisi = 0;

                for (int i = 0; i < 10000; i++)
                {
                    ReischEkle(randDizi[i], dizi);
                }
                int reischCakismaSayisi = cakismaSayisi;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }


                cakismaSayisi = 0;
                for (int i = 0; i < 10000; i++)
                {
                    BlischEkle(randDizi[i], dizi);
                }
                int blischCakismaSayisi = cakismaSayisi;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }


                cakismaSayisi = 0;
                for (int i = 0; i < 10000; i++)
                {
                    BeischEkle(randDizi[i], dizi);
                }
                int beischCakismaSayisi = cakismaSayisi;
                for (int i = 0; i < dizi.Length; i++)
                {
                    dizi[i].key = 0;
                    dizi[i].link = -1;
                }


                this.chart1.Titles.Add("Algoritmaların Adım Sayısı");
                this.chart1.Series["Algoritmalar"].Points.AddXY("Lisch", lischCakismaSayisi);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Eisch", eischCakismaSayisi);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Lich", lichCakismaSayisi);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Eich", eichCakismaSayisi);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Rlisch", rlischCakismaSayisi);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Reisch", reischCakismaSayisi);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Blisch", blischCakismaSayisi);
                this.chart1.Series["Algoritmalar"].Points.AddXY("Beisch", beischCakismaSayisi);
               

            }
        }

        private void sil_Click(object sender, EventArgs e)
        {      
            chart1.Series["Algoritmalar"].Points.Clear(); 
            chart1.Titles.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
