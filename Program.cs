using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ieee754Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //----------------------------------------------------------------------------------------------
            Console.WriteLine("IEEE-754 Konvertáló");
            Console.WriteLine("Készítette: Herbák Marcell - Neptun kód: QAGSVA - 2022");
            Console.WriteLine("\nEz a program képes egy decimális, dupla pontosságú lebegőpontos számot ábrázolni az IEEE754 szabvány szerint!");

            Console.Write("\nNyomjon meg egy gombot a folytatáshoz!");

            Console.ReadKey();
            //----------------------------------------------------------------------------------------------
            Console.Clear();
            Console.WriteLine("Használat: [előjel]decimál[,törtrész] | Minta: -192,865");
            Console.WriteLine("Kérem, ne használjon (-1;1) intervallumon belüli értéket!\nVegye figyelembe, hogy az egész rész a 32 bites (decimál <= 2,147,483,647) határon beül maradjon!");
            Console.Write("\nKérem írjon be egy számot: ");
            string inputNum = Console.ReadLine();
            bool notNum = false;
            bool inter = false;
            double decNum = 0;
            int inputTest = 0;
            do
            {
                if (notNum && inter)
                {
                    Console.Write("Nem megfelelő érték! Kérem adjon meg új számot: ");
                    inputNum = Console.ReadLine();
                }
                else if (notNum)
                {
                    Console.Write("Nem megfelelő bevitel! Kérem adjon meg új számot: ");
                    inputNum = Console.ReadLine();
                }

                notNum = false;
                inter = false;

                string[] tempInput = inputNum.Split(',');

                if (tempInput[0] == "")
                {
                    notNum = true;
                    continue;
                }

                if (!int.TryParse(tempInput[0], out inputTest))
                {
                    notNum = true;
                    continue;
                }

                if (!double.TryParse(inputNum, out decNum))
                {
                    notNum = true;
                    continue;
                }

                if (Math.Abs(Convert.ToDouble(inputNum)) < 1) // Jelenleg nem tudom leprogramozni, ezért csak nem engedem be a programba :(
                {
                    notNum = true;
                    inter = true;
                }
            } while (notNum);
            //----------------------------------------------------------------------------------------------
            int eBit = 0;
            if (decNum < 0)
            {
                eBit = 1;
            }
            //----------------------------------------------------------------------------------------------
            string[] decString = decNum.ToString().Split(',');

            if (decString[0] == "")
            {
                decString[0] = "0";
            }

            string decInt = "";
            string decFract = "";
            if (eBit == 0)
            {
                for (int i = 0; i < decString[0].Length; i++)
                {
                    decInt += decString[0][i];
                }
                if (decString.Length > 1)
                {
                    decFract = decString[1];
                }
            }
            else if (eBit == 1)
            {
                for (int i = 1; i < decString[0].Length; i++)
                {
                    decInt += decString[0][i];
                }
                if (decString.Length > 1)
                {
                    decFract = decString[1];
                }
            }
            //----------------------------------------------------------------------------------------------
            string decIntInBin = Convert.ToString(Convert.ToInt32(decInt), 2);
            //----------------------------------------------------------------------------------------------
            string exponent = "";
            int d = decIntInBin.ToString().Length - 1;
            int expDec = d + 1023;
            if (d == 0)
            {
                exponent = "0" + Convert.ToString(expDec, 2);
            }
            else
            {
                exponent = Convert.ToString(expDec, 2);
            }
            //----------------------------------------------------------------------------------------------
            string fraction = "";
            for (int i = 1; i < decIntInBin.Length; i++)
            {
                fraction += decIntInBin[i];
            }
            //----------------------------------------------------------------------------------------------
            double decFractD = Convert.ToDouble("0," + decFract);
            for (int i = fraction.Length; i < 28; i++)
            {
                decFractD = decFractD * 2;
                if (decFractD >= 1)
                {
                    fraction += "1";
                    decFractD = decFractD - 1;
                }
                else
                {
                    fraction += "0";
                }
            }
            // Mivel nem fér el egy stringbe, ezért ketté szedtem!
            string fraction2 = "";
            for (int i = fraction.Length; i < 52; i++)
            {
                decFractD = decFractD * 2;
                if (decFractD >= 1)
                {
                    fraction2 += "1";
                    decFractD = decFractD - 1;
                }
                else
                {
                    fraction2 += "0";
                }
            }
            //----------------------------------------------------------------------------------------------
            Console.WriteLine("A(z) {0} IEEE-754 szabvány szerint leírva: ", inputNum);
            Console.WriteLine("{0} {1} {2}{3}", eBit, exponent,fraction,fraction2);
            Console.WriteLine("ahol: ");
            //----------------------------------------------------------------------------------------------
            Console.WriteLine("\n" + eBit + " - előjel bit");
            Console.WriteLine(exponent + " - karakterisztika");
            Console.WriteLine(fraction + fraction2 + " - mantissza");
            //----------------------------------------------------------------------------------------------
            string hex = "";
            string ieee754 = eBit + exponent + fraction + fraction2;
            int fractIndex = 0;
            string binTemp = "";

            while (fractIndex < ieee754.Length - 1)
            {
                binTemp = ieee754[fractIndex].ToString() + ieee754[fractIndex + 1].ToString() + ieee754[fractIndex + 2].ToString() + ieee754[fractIndex + 3].ToString();
                switch (binTemp)
                {
                    case "0000":
                        hex = hex + "0";
                        break;
                    case "0001":
                        hex = hex + "1";
                        break;
                    case "0010":
                        hex = hex + "2";
                        break;
                    case "0011":
                        hex = hex + "3";
                        break;
                    case "0100":
                        hex = hex + "4";
                        break;
                    case "0101":
                        hex = hex + "5";
                        break;
                    case "0110":
                        hex = hex + "6";
                        break;
                    case "0111":
                        hex = hex + "7";
                        break;
                    case "1000":
                        hex = hex + "8";
                        break;
                    case "1001":
                        hex = hex + "9";
                        break;
                    case "1010":
                        hex = hex + "A";
                        break;
                    case "1011":
                        hex = hex + "B";
                        break;
                    case "1100":
                        hex = hex + "C";
                        break;
                    case "1101":
                        hex = hex + "D";
                        break;
                    case "1110":
                        hex = hex + "E";
                        break;
                    case "1111":
                        hex = hex + "F";
                        break;
                    default:
                        Console.WriteLine("Hiba!"); // Ha idelépsz, akkor nem lehet ez!
                        break;
                }
                fractIndex = fractIndex + 4;
            }
            Console.WriteLine("\nN(16): {0}", hex);
            //----------------------------------------------------------------------------------------------
            Console.WriteLine("\nA program egy gomb lenyomására leáll!");
            Console.ReadKey();
        }
    }
}
