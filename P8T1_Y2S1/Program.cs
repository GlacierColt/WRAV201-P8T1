using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;

namespace P8T1_Y2S1
{
    internal class Program
    {
        public String TFILE = "Items.txt";
       // public FoodItem[] list = new FoodItem[17];
       public FoodList list = new FoodList();
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            list.ReadFile(TFILE);
            list.InsertionSortDec();
            list.DisplayAll();
            Console.ReadLine();
        }


        
    }

    public class FoodItem
    {
        public String Code, Name, Category;
        public int NrInStock;
        public double CostPrice;

        public FoodItem(String Code, String Name, String Category, int NrInStock, double CostPrice)
        {
            this.Code = Code;
            this.Name = Name;
            this.Category = Category;
            this.NrInStock = NrInStock;
            this.CostPrice = CostPrice;
        }

        public virtual double GetSellingPrice()
        {
            return CostPrice + CostPrice * 0.38;
        }

        public int GetNrInStock()
        {
            return NrInStock;
        } 

        public virtual void Display()
        {
            Console.WriteLine("  {0} {1} {2} {3} {4}", Code,Name, Category, NrInStock, GetSellingPrice());
        }

        

    }

    public class OrganicFoodItem : FoodItem
    {
        bool borganic;

        public OrganicFoodItem(String Code, String Name, String Category, int NrInStock, double CostPrice, String sorganic) : base(Code, Name, Category, NrInStock, CostPrice)
        {
            if (sorganic == "Organic")
            {
                borganic = true;
            }
            else
            {
                borganic = false;
            }
        }

        public bool IsOrganic()
        {
            return borganic;
        }
        public override double GetSellingPrice()
        {
            return CostPrice + CostPrice * 0.67;
        }

        public override void Display()
        {
            Console.WriteLine("* {0} {1} {2} {3} {4}", Code, Name, Category, NrInStock, GetSellingPrice());
        }
    }

    class FoodList
    {
        ArrayList list;
        public FoodList()
        {
            list = new ArrayList();
            
        }

        public void ReadFile(string TFILE)
        {
            StreamReader read = new StreamReader(TFILE);
            ReadFileRecursively(read, 0);
            read.Close();
        }

        public void ReadFileRecursively(StreamReader read, int icount)
        {
            string sline = read.ReadLine();
            if (sline != null)
            {
                string[] fields = sline.Split(',');
                if (fields.Length == 6)
                {
                    list.Add(new OrganicFoodItem(fields[0], fields[1], fields[2], int.Parse(fields[3]), double.Parse(fields[4]), fields[5]));
                }
                else
                {
                    list.Add(new FoodItem(fields[0], fields[1], fields[2], int.Parse(fields[3]), double.Parse(fields[4])));
                }
                icount++;
                ReadFileRecursively(read, icount);
            }
            else
            {
                return;
            }
        }

        public void DisplayAll()
        {
            for (int i = 0; i < list.Count; i++)
            {
                FoodItem temp = (FoodItem)list[i];
                temp.Display();
            }
        }

        public void InsertionSortDec()
        {
            for (int pass = 1; pass < list.Count; pass++)
            {
                FoodItem newItem = (FoodItem)list[pass];
                int curPos = pass-1;
                FoodItem currentItem = (FoodItem)list[curPos];
                while ((curPos != -1) && (newItem.GetSellingPrice() > currentItem.GetSellingPrice()))
                {
                    curPos--;
                    if (curPos != -1)
                    {
                        currentItem = (FoodItem)list[curPos];
                    }
                }
                list.RemoveAt(pass);
                list.Insert(curPos  + 1, newItem);

            }
        }

        
       
    }



}
