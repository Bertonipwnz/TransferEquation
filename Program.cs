using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Программа для решения уравнения переноса
/// </summary>
namespace CondPerenosa
{
    class Program
    {
        static void Main(string[] args)
        {
            int t = 1; //t
            double x = 0; //x
            string stroka; //переменная для считывания строк
            double coefA = 1; //коэф А
            int fx = 0; //f(x)
            int choiceUserMethod = 0;//выбор метода
            int NSetka = 0;//сетка N
            double h = 0;//H
            double TAU = 0;//T
            Console.WriteLine("Уравнение переноса имеет вид: d*U/d*t + (A * (d * U)/(d * x)) = 0, где A>0, t>=0, -1<=x<=2");
            Console.WriteLine("Начальные данные: u(0,x) = f(x)");
            Console.WriteLine("Краевое условие: u(t,-1)= 0");
            Console.WriteLine("Где f(x) = {1, -0.5<=x<=0.5 \n" +
                "\t   {0,-1<=x<=-0.5, 0.5<=x<=2 \n" +
                "\t   {A = 1 ");
            gg:
            Console.Write("Введите x: ");//Запрос на ввод
            stroka = Console.ReadLine(); //считывание
            x = Convert.ToDouble(stroka);//конвертация
            //начальные данные
            if(x>=-0.5 && x<=0.5)
            {
                fx = 1;
            }
            else if((x<-0.5 && x>=-1) || (x > 0.5 && x <= 2))
            {
                fx = 0;
            }
            else
            {
                Console.WriteLine("Error");
                goto gg;
            }
            Console.Write("Введите размер сетки: ");//Запрос на ввод
            stroka = Console.ReadLine();            //считывание
            NSetka = Convert.ToInt32(stroka);       //конвертация
            Console.Write("Введите шаг по пространству: ");//Запрос на ввод
            stroka = Console.ReadLine();                   //считывание
            h = Convert.ToDouble(stroka);                  //конвертация
            Console.Write("Введите шаг по времени: ");//Запрос на ввод
            stroka = Console.ReadLine();              //считывание
            TAU = Convert.ToDouble(stroka);           //конвертация
            Console.Write("Введите A: ");//Запрос на ввод
            stroka = Console.ReadLine();              //считывание
            coefA = Convert.ToDouble(stroka);           //конвертация
            float[,] massivPer = new float[NSetka+5, NSetka+5]; //переменная для массива
            for(int i = 0; i<NSetka; i++)
            {
                massivPer[0, i] = fx;
                massivPer[i, 1] = 0;
            }
            Console.WriteLine("Выберите схему решения: 1 - явная схема против-потока,\n 2 - неявная схема против-потока, \n 3 - схема Лакса-Вендрофа");
            stroka = Console.ReadLine();
            choiceUserMethod = Convert.ToInt32(stroka);
            switch(choiceUserMethod)
            {
                case 1:
                    {
                        for (int n = 1; n < NSetka; n++)
                        {
                            for (int i = 1; i < NSetka; i++)
                            {
                                //решение метода
                                massivPer[i, n] = (float)(massivPer[i, n + 1] + coefA * TAU * ((massivPer[i,n] - massivPer[i-1,n])/h));  
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int n = 1; n < NSetka; n++)
                        {
                            for (int i = 1; i < NSetka; i++)
                            {
                                //решение метода
                                massivPer[i, n] = (float)(massivPer[i, n + 1] + coefA * TAU * ((massivPer[i, n + 1] - massivPer[i - 1, n + 1]) / h));
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        for (int n = 1; n < NSetka; n++)
                        {
                            for (int i = 1; i < NSetka; i++)
                            {
                                double predictor = (massivPer[i, n] +massivPer[i-1,n])/2;
                                double corrector = massivPer[i, n] + (coefA * (massivPer[i, n + 1] - massivPer[i, n]) / TAU * h);
                                //решение метода
                                massivPer[i, n] = (float)(massivPer[i-1,n] -  (coefA*(corrector - predictor)/TAU*h));
                            }
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Такого варианта нету");
                        break;
                    }
            }
            for (int j = NSetka-1; j != 0; j--)
            {
                Console.Write("|" + " ");

                for (int i = 1; i < NSetka; i++)
                {
                    Console.Write(massivPer[i, j] + "\t" + "|");
                }
                Console.WriteLine();
                Console.Write(new string('-', NSetka * 5));
                Console.WriteLine();
            }
            Console.Write("|" + " ");

            for (int i = 0; i < NSetka; i++)
            {
                Console.Write("{0}", massivPer[i, 0] + "\t" + "|");
            }
            Console.WriteLine();
            Console.Write(new string('-', NSetka * 5));
            Console.ReadKey(true);
        }
    }
}
