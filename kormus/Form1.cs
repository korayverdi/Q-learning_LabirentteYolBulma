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

namespace kormus
{
    public partial class Form1 : Form
    {
        public static int satirGlobal = 0;
        public int iterasyonGlobal = 0;
        public int iterasyonCheck = 0;
        public int girisGlobal = 0;
        public int cikisGlobal = 0;
        public int cikisCheck = 0;
        public static int pathb =0;
        Class1 cls;
        public Form1()
        {
            InitializeComponent();  
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Txt dosyası  |*.txt";
            file.Title = "Aç";
            file.ShowDialog();
            string DosyaYolu = file.FileName;
            //Okuma işlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(DosyaYolu, FileMode.Open, FileAccess.Read);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosyanın açılacağını,
            //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
            StreamReader sw = new StreamReader(fs);

            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.

            string dene = sw.ReadLine();

            int satirSay = 0;

            while (dene != null)
            {
                dene = sw.ReadLine();
                satirSay++;
            }
            satirGlobal = satirSay;
            pathb = satirSay;
            cls = new Class1();
            int giris = Convert.ToInt16(textBox1.Text);
            int cikis = Convert.ToInt16(textBox2.Text);
            int iterasyon = Convert.ToInt16(textBox3.Text);
            iterasyonGlobal = iterasyon;
            girisGlobal = giris;
            cikisGlobal = cikis;
            string[,] komsuMat = new string[satirSay, satirSay];
            int[] pathMat = new int[satirSay];
            int i = 0, j = 0;
            string deneme;
            for (int k = 0; k < satirSay; k++)
            {
                for (int l = 0; l < satirSay; l++)
                {
                    komsuMat[k, l] = "200";
                }
            }
            sw.Close();
            fs.Close();
            FileStream fst = new FileStream(DosyaYolu, FileMode.Open, FileAccess.Read);
            StreamReader swt = new StreamReader(fst);
            string satir = swt.ReadLine();
            while (satir != null)
            {
                deneme = satir;

                string[] words = satir.Split(',');
                foreach (string word in words)
                {
                    komsuMat[i, j] = word;
                    j++;
                }

                j = 0;
                i++;
                satir = swt.ReadLine();
            }           

            for (int k = 0; k < satirSay; k++)
            {
                for (int l = 0; l < satirSay; l++)
                {
                    cls.RMat[k, l] = "-1";
                }
            }

            int tmp = 0;
            string temp;
            for (int k = 0; k < satirSay; k++)
            {
                for (int l = 0; l < satirSay; l++)
                {
                    temp = Convert.ToString(cikis);
                    tmp = Convert.ToInt16(komsuMat[k, l]);

                    if (komsuMat[k, l] == temp && tmp != 200)
                    {
                        cls.RMat[k, tmp] = "100";
                    }
                    else if (komsuMat[k, l] != "200" && tmp != 200)
                    {
                        cls.RMat[k, tmp] = "0";
                    }
                    cls.RMat[cikis, cikis] = "100";
                }
            }

            System.Drawing.Graphics grafiknesne;
            grafiknesne = this.CreateGraphics();

            Pen siyah = new Pen(System.Drawing.Color.Black, 5);
            Pen arkaplan = new Pen(System.Drawing.Color.WhiteSmoke, 5);
            grafiknesne.Clear(this.BackColor);
            grafiknesne.DrawRectangle(siyah, 260, 10, 300, 300);

            double cizgisay = Math.Sqrt(satirSay) - 1;
            double karesay = Math.Sqrt(satirSay);
            int karesayisi = Convert.ToInt16(karesay);
            int kenar =  300 / karesayisi;
            int girisX = girisGlobal;
            int girisY = girisGlobal;
            int cikisX = cikisGlobal;
            int cikisY = cikisGlobal;
            for (int k = 0; k < cizgisay; k++)
            {
                for (int l = 0; l < cizgisay; l++)
                {
                    grafiknesne.DrawLine(siyah, 260 + kenar * (l + 1), 10, 260 + kenar * (l + 1), 310);
                    grafiknesne.DrawLine(siyah, 260, 10 + kenar * (k + 1), 560, 10 + kenar * (k + 1));   
                }
            }

            if (girisGlobal >= 0 && girisGlobal < karesay)
            {
                //girisi cizer
                grafiknesne.DrawLine(arkaplan, 260 + kenar * giris, 10, 260 + kenar * (giris + 1), 10);
            }
            if (cikisGlobal >= satirSay-karesay && cikisGlobal < satirSay)
            {
                // cikis cizer
                grafiknesne.DrawLine(arkaplan, 260 + kenar * (cikis % karesayisi), 310, 260 + kenar * ((cikis % karesayisi) + 1), 310);
            }

            for (int k = 0; k < satirGlobal; k++)
            {
                for (int l = 0; l < satirGlobal; l++)
                {
                    int kX = k / Convert.ToInt16(karesay);
                    int kY = k % Convert.ToInt16(karesay);
                    int lX = l / Convert.ToInt16(karesay);
                    int lY = l % Convert.ToInt16(karesay);

                        
                    if ((cls.RMat[k,l] == "0" || cls.RMat[k, l] == "100" ))
                    {
                        if (k < l) {
                            if (Math.Abs(k - l) == 1 )
                            {
                                //dikey cizer
                                grafiknesne.DrawLine(arkaplan, 260 + kenar * (lY), 10 + kenar * (lX), 260 + kenar * (lY), 10 + kenar * (lX + 1));
                            }
                            else if (Math.Abs(k - l) == Convert.ToInt16(karesay))
                            {
                                // yatay cizer
                                grafiknesne.DrawLine(arkaplan, 260 + kenar * (lY), 10 + kenar * (lX), 260 + kenar * (lY + 1), 10 + kenar * (lX));
                            }
                        }
                    }
                }
            }
            
            
            /*for (int k = 0; k < cizgisay; k++)
            {
                for (int l = 0; l < cizgisay; l++)
                {
                    if(RMat[k,l] == "-1")
                    {
                        grafiknesne.DrawLine(siyah, 260 + (300 / Convert.ToInt16(karesay)) * (l + 1), 10, 260 + (300 / Convert.ToInt16(karesay)) * (l + 1), 310);
                    }
                }
            }*/

            for (int k = 0; k < satirSay; k++)
            {
                for (int l = 0; l < satirSay; l++)
                {
                    cls.QMat[k, l] = 0;
                }
            }
            
            iterasyonDondur();
            pathHesapla(girisGlobal);
            dosya_yaz();

            pathCiz();




            /*
            for (int k = 0; k < satirSay; k++)
            {
                int enbuyuk = 0;
                for (int l = 0; l < satirSay; l++)
                {
                    if(cls.QMat[k, l] > enbuyuk)
                    {
                        enbuyuk = Convert.ToInt16(cls.QMat[k, l]);
                    }
                }
            }
            */





            /*
             int pathSatir = Convert.ToInt16(Math.Sqrt(satirGlobal));
            for (int k = 0; k < pathSatir - 1; k++)
            {
                Console.WriteLine(cls.pathMatris[k]);  
            }
            */
            //
            //
            //
            //
            //
            //

            //
            //

            //
            //
            //Satır satır okuma işlemini gerçekleştirdik ve ekrana yazdırdık
            //Son satır okunduktan sonra okuma işlemini bitirdik
            swt.Close();
            fst.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.
            Console.WriteLine("Bitti");
        }
        public void pathCiz()
        {
            System.Drawing.Graphics grafiknesne1;
            grafiknesne1 = this.CreateGraphics();

     
            Pen path = new Pen(System.Drawing.Color.Red, 5);
            
            double cizgisay = Math.Sqrt(satirGlobal) - 1;
            double karesay = Math.Sqrt(satirGlobal);
            int karesayisi = Convert.ToInt16(karesay);
            int kenar = 300 / karesayisi;

            int yol = 0, kaynak = 0;
            int kaynakX = 0, kaynakY = 0, yolY = 0, yolX = 0;
            int satir = Convert.ToInt16(Math.Sqrt(satirGlobal));

            
            
            int i = 0,j=0;

            while (cls.pathMatris[i]!=cikisGlobal)
            {
                kaynak = cls.pathMatris[i];
                 j = i + 1;
                yol = cls.pathMatris[j];
                kaynakY = kaynak / satir;
                kaynakX = kaynak % satir;
                yolY = yol / satir;
                yolX = yol % satir;
                if (kaynak==girisGlobal)
                {
                    grafiknesne1.DrawLine(path, 260 + kenar * (kaynakX) + kenar / 2, 10 + kenar * (kaynakY), 260 + kenar * (kaynakX) + kenar / 2, 10 + kenar * (kaynakY) + kenar / 2);

                }
                if (yol==cikisGlobal)
                {
                    grafiknesne1.DrawLine(path, 260 + kenar * (yolX) + kenar / 2, 10 + kenar * (yolY) + kenar / 2, 260 + kenar * (yolX) + kenar / 2, 10 + kenar * (yolY+1));

                }


                string donus;
                if (yol < kaynak)
                {
                    if (kaynakY == yolY)
                    {
                        donus = "sol";
                        grafiknesne1.DrawLine(path, 260 + kenar * (yolX) + kenar / 2, 10 + kenar * (yolY) + kenar / 2, 260 + kenar * (kaynakX) + kenar / 2, 10 + kenar * (kaynakY) + kenar / 2);

                        //return *donus;
                        i++;
                    }
                    else if (kaynakX == yolX)
                    {
                        donus = "ust";
                        grafiknesne1.DrawLine(path, 260 + kenar * (yolX) + kenar / 2, 10 + kenar * (yolY) + kenar / 2, 260 + kenar * (kaynakX) + kenar / 2, 10 + kenar * (kaynakY) + kenar / 2);
                        //return *donus;
                        i++;
                    }
                }
                else
                {
                    if (kaynakY == yolY)
                    {
                        donus = "sag";
                        grafiknesne1.DrawLine(path, 260 + kenar * (kaynakX) + kenar / 2, 10 + kenar * (kaynakY) + kenar / 2, 260 + kenar * (yolX) + kenar / 2, 10 + kenar * (yolY) + kenar / 2);
                        //return *donus;
                        i++;
                    }
                    else if (kaynakX == yolX)
                    {
                        grafiknesne1.DrawLine(path, 260 + kenar * (kaynakX) + kenar / 2, 10 + kenar * (kaynakY) + kenar / 2, 260 + kenar * (yolX) + kenar / 2, 10 + kenar * (yolY) + kenar / 2);
                        donus = "alt";
                        i++;
                        //return *donus;
                    }

                }
               
            }
        }

        public void dosya_yaz()
        {
            StreamWriter rw = new StreamWriter("C:\\Users\\kryvr\\Desktop\\Yazlab2_1kormus\\kormus\\kormus\\outR.txt");
            rw.WriteLine("R Matrisi:");
            for (int k = 0; k < satirGlobal; k++)
            {
                for (int l = 0; l < satirGlobal; l++)
                {
                    rw.Write(cls.RMat[k, l]);
                    rw.Write(" ");
                }
                rw.WriteLine(" ");
            }
            rw.Close();

            StreamWriter qw = new StreamWriter("C:\\Users\\kryvr\\Desktop\\Yazlab2_1kormus\\kormus\\kormus\\outQ.txt");
            qw.WriteLine("Q Matrisi:");
            for (int k = 0; k < satirGlobal; k++)
            {
                for (int l = 0; l < satirGlobal; l++)
                {
                    qw.Write(cls.QMat[k, l]);
                    qw.Write(" ");
                }
                qw.WriteLine(" ");
            }
            qw.Close();

            StreamWriter pathw = new StreamWriter("C:\\Users\\kryvr\\Desktop\\Yazlab2_1kormus\\kormus\\kormus\\outPath.txt");
            pathw.WriteLine("Path:");
           
                for (int k = 0; k < pathb; k++)
                {
                    pathw.Write(cls.pathMatris[k]);
                    pathw.Write(" ");
                }
            
            pathw.Close();

        }

        public void  iterasyonDondur()
        {
            for(int i=0; i < iterasyonGlobal*2; i++ )
            {
                QFonkHesapla(girisGlobal);

            }
   
        }
        public void QFonkHesapla(int indexY)
        {
            List<int> myList = new List<int>();
            List<int> myList1 = new List<int>();
            bool done = false;
            Random random1 = new Random();
            Random random2 = new Random();

            while (!done)
            {
                for (int i = 0; i < satirGlobal; i++)
                {
                    if (cls.RMat[indexY, i] != "-1")
                    {
                        myList.Add(i);
                    }
                }

                
                int selectedNeigbor = myList[random1.Next(0, myList.Count)];

                for (int i = 0; i < satirGlobal; i++)
                {
                    if (cls.RMat[selectedNeigbor, i] != "-1")
                    {
                        myList1.Add(i);
                    }
                }
                double maxQValue = 0;
                int max = 0;


                if(myList1.Count>=2)
                {
                    for(int i=0;i<myList1.Count-1;i++)
                    {
                        if(cls.QMat[selectedNeigbor, myList1[i]] >cls.QMat[selectedNeigbor, myList1[i+1]])
                        {

                            if(cls.QMat[selectedNeigbor, myList1[i]] >=maxQValue)
                            {
                                maxQValue = cls.QMat[selectedNeigbor, myList1[i]];

                            }
                        }
                        else if (cls.QMat[selectedNeigbor, myList1[i]] < cls.QMat[selectedNeigbor, myList1[i+1]])
                        {
                            if (cls.QMat[selectedNeigbor, myList1[i+1]] > maxQValue)
                            {
                                maxQValue = cls.QMat[selectedNeigbor, myList1[i + 1]];
                            }
                        }
                        else
                        {
                            if (cls.QMat[selectedNeigbor, myList1[i]] > maxQValue)
                            {
                                maxQValue = cls.QMat[selectedNeigbor, myList1[i]];

                            }

                        }
                    }
                }
                else
                {
                    maxQValue = cls.QMat[selectedNeigbor, myList1[0]];
                }


                
                max = Convert.ToInt32(0.8 * maxQValue); 
                cls.QMat[indexY, selectedNeigbor] = Convert.ToInt32(cls.RMat[indexY, selectedNeigbor]) + max; 


                if(indexY == cikisGlobal)
                {
                    myList.Clear();
                    myList1.Clear();
                    done = true;
                }

                indexY = selectedNeigbor;
                myList.Clear();
                myList1.Clear();

            }



        }

        
      
        public void pathHesapla(int bas)
        {
            bas = girisGlobal;
            int sayac = 0, l = 1, son = cikisGlobal, enb = 0;
            cls.pathMatris[0] = bas;
            while (bas != son)
            {
                for (int k = 0; k < satirGlobal; k++)
                {
                    if (enb < cls.QMat[bas, k] && cls.RMat[bas,k] != "-1")
                    {
                        enb = Convert.ToInt16(cls.QMat[bas, k]);
                        sayac = k; 
                    }
                }
                cls.pathMatris[l] = sayac;
                l++;
                bas = sayac;
                enb = 0;
                pathb=l;
            }
            for (int k = 0; k < pathb; k++)
            {
                Console.WriteLine(cls.pathMatris[k]);
            }

        }
    }
}
