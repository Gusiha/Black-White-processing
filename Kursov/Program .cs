// Какой массив брать изначально
// Пятиричный массив и перевод в бинарный вид - в одной функции
using static MyLib.Sort;
internal class Program
{
    // Проверка на "серость"
    static bool Check(int Dimension, int[,] arr, int x, int y)
    {
        int flag = arr[x, y];
        for (int i = x; i < x + Dimension; i++)
        {
            for (int j = y; j < y + Dimension; j++)
            {
                if (flag != arr[i, j])
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    // Из десятичной системы в @base-систему счисления
    static int ConvertFromBase(int num, int @base)
    {
        return num.ToString().Reverse()
        .Select((c, index) => (int)Char.GetNumericValue(c) * (int)Math.Pow(@base, index))
        .Sum();
    }

    static int[,] bynary_image =
        {  
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 1, 1, 1, 0, 0, 0},
            {0 ,1, 1, 0, 1, 1, 1, 1},
            {1 ,1, 0, 0, 1, 1, 1, 1},
            {0 ,0, 1, 1, 0, 1, 0, 1},
            {0 ,0, 1, 1, 0, 0, 1, 1},
            {0 ,0, 1, 1, 1, 0, 1, 0},
            {0 ,0, 1, 1, 1, 0, 0, 0}
        };
    
    static int Dim = (int)Math.Sqrt(bynary_image.Length);
    static int FindMaxSquares(int[,] arr)
    {
        int count = 0;

        foreach (var item in bynary_image)
        {
            if (item == 1)
                count++;
        }

        return count;
    }

    private static void Main(string[] args)
    {

        int[] decimal_image = new int[FindMaxSquares(bynary_image)];
        int count = 0;
        PrintBynary();
        ToDecimalString(0, 0, Dim);
        QuickSort(decimal_image, 0, decimal_image.Length-1);
        Array.Reverse(decimal_image);
        Array.Resize(ref decimal_image, Array.IndexOf(decimal_image, 0));
        Print(decimal_image);
        From5To10(decimal_image);
        Print(decimal_image);
        ToBynaryMatrix();
        PrintBynary();
        

        void PrintBynary()
        {
            for (int i = 0; i < Math.Sqrt(bynary_image.Length); i++)
                {
                for (int j = 0; j < Math.Sqrt(bynary_image.Length); j++)
                    {
                    Console.Write($"{bynary_image[i,j]} ");
                }
                System.Console.WriteLine();
            }
        }
       
        void ToClearArray(int[] arr)
        {
            Array.Clear(arr, 0, Array.LastIndexOf(arr, 0));
        }
        
        bool ToDecimalString(int i, int j, int Dimension, string Way = "")

        {
            int cur_dim = 0;

            // выход из рекурсии
            if (Check(Dimension, bynary_image, i, j))
            {
                cur_dim = Dimension / 2;
                ToDecimalString(i, j, cur_dim, "1" + Way);
                ToDecimalString(i, j + cur_dim, cur_dim, "2" + Way);
                ToDecimalString(i + cur_dim, j, cur_dim, "3" + Way);
                ToDecimalString(i + cur_dim, j + cur_dim, cur_dim, "4" + Way);
            }

            else
            {
                if (bynary_image[i, j] == 1)
                {
                    count++;
                    decimal_image[count - 1] = Convert.ToInt32(Way);
                    return true;
                }

                else
                    return false;
            }
            return true;
        }

        void ToBynaryMatrix()
        {
            Array.Clear(bynary_image);
            From10To5(decimal_image);
            for (int i = 0; i < decimal_image.Length; i++)
            {
                FindBlackSquare(0, 0, 8, decimal_image[i]);
            }

        }

        void FindBlackSquare(int i, int j, int Dimension, int Way)
        {
            int k, r = 0;

            if (Way == 0)
            {
                for (k = i; k < i + Dimension; k++)
                {
                    for (r = j; r < j + Dimension; r++)
                    {
                        bynary_image[k,r] = 1;
                    }
                }
            }
            else
            {
                k = Way % 5 - 1;
                r = Dimension / 2;
                FindBlackSquare(i+r*(k / 2), j+r*(k % 2), r, Way / 10);
            }
        }

        void From5To10(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = ConvertFromBase(arr[i], 5);
            }
        }

        void From10To5(int[] arr)
        {
            string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                while (arr[i] != 0)
                {
                    result += arr[i] % 5;
                    arr[i] /= 5;
                }

                result = new string(result.Reverse().ToArray());
                arr[i] = Convert.ToInt32(result);
                result = "";
            }
        }

        void Print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.WriteLine();
        }
    }


}
