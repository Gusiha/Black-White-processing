// Какой массив брать изначально
// Пятиричный массив и перевод в бинарный вид - в одной функции
using static MyLib.Sort;
internal class Program
{
    // Вывести матрицу
    public static void PrintBynary(int[,] arr, int Dim)
    {
        for (int i = 0; i < Dim; i++)
        {
            for (int j = 0; j < Dim; j++)
            {
                Console.Write($"{arr[i, j]} ");
            }
            System.Console.WriteLine();
        }
    }
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

    // Найти размерность
    static int FindDim(int[,] arr)
    {
        return (int)Math.Sqrt(arr.Length);
    }

    // Найти максимальное кол-во квадратов (начальная длина десятичного массива)
    static int FindMaxSquares(int[,] arr)
    {
        int count = 0;

        foreach (var item in arr)
        {
            if (item == 1)
                count++;
        }

        return count;
    }

    static void Title()
    {
        Console.WriteLine("\t\t\tКУРСОВАЯ РАБОТА\n" +
            "по дисциплине «Структуры и алгоритмы компьютерной обработки данных»\n" +
            "на тему «Кратчайшие пути в бесконтурном графе»\n" +
            "Выполнил студент 3 курса ВШКМиС\n" +
            "Милушев Тимур Ильдусович\n");
    }

    static void MenuTitle()
    {
        Console.WriteLine(
            "0. Ввести бинарную матрицу\n" +
            "1. Ввести десятичную строку\n" +
            "2. Обнуление матрицы\n" +
            "3. Обнуление десятичной строки\n" +
            "4. Вывести изображение\n" +
            "5. Вывести десятичную строку\n" +
            "6. Алгоритм [изображение]->[строка]\n" +
            "7. Алгоритм [строка]->[изображение]\n"
            );
    }



    private static void Main(string[] args)
    {
        Title();
        int Dimension = 0;
        int[,] bynary_image = null;
        int MenuButton;
        int[] decimal_image = null;
        int temp = -1;
        int count = 0;

        //int[] decimal_image = new int[FindMaxSquares(bynary_image)];

        while (true)
        {
            MenuTitle();
            MenuButton = Int32.Parse(Console.ReadLine());
            switch (MenuButton)
            {
                case 0:
                    {
                        Console.WriteLine("Введите размерность матрицы(четную)");
                        while (int.TryParse(Console.ReadLine(), out Dimension) == false || Dimension % 2 != 0 || Dimension < 0)
                        {
                            Console.WriteLine("Incorrect value. Input Dimension again: ");
                            Console.WriteLine("Введите размерность матрицы(четную)");
                        }
                        bynary_image = new int[Dimension, Dimension];
                        Console.WriteLine("Введите бинарное изображение");

                        for (int i = 0; i < Dimension; i++)
                        {
                            for (int j = 0; j < Dimension; j++)
                            {

                                while (int.TryParse(Console.ReadLine(), out temp) == false || !(temp == 0 || temp == 1))
                                {
                                    Console.WriteLine("Incorrect symbol. Input index again: ");
                                }
                                bynary_image[i, j] = temp;
                                temp = -1;
                            }

                        }
                        decimal_image = new int[FindMaxSquares(bynary_image)];
                        continue;
                    }

                case 1:
                    {
                        int BlackAreas = 0;
                        Console.WriteLine("Ввести десятичный вид изображения");
                        if (Dimension == 0)
                        {
                            Console.WriteLine("Введите размерность будущего изображение");
                            Dimension = int.Parse(Console.ReadLine());
                            Console.WriteLine("Введите кол-во черных зон");
                            BlackAreas = int.Parse(Console.ReadLine());
                        }
                        decimal_image = new int[BlackAreas];

                        for (int i = 0; i < BlackAreas; i++)
                        {
                            decimal_image[i] = int.Parse(Console.ReadLine());
                        }
                        continue;
                    }
                case 2:
                    {
                        if (bynary_image == null)
                        {
                            Console.WriteLine("Матрица не задана");
                            continue;
                        }
                        Array.Clear(bynary_image);
                        continue;
                    }

                case 3:
                    {
                        if (decimal_image == null)
                        {
                            Console.WriteLine("Строка не задана");
                            continue;
                        }
                        Array.Clear(decimal_image);
                        continue;
                    }
                case 4:
                    {
                        if (bynary_image != null)
                        {
                            PrintBynary(bynary_image, Dimension);
                        }
                        else
                        {
                            Console.WriteLine("Матрица не задана");
                        }
                        continue;
                    }
                case 5:
                    {
                        if (decimal_image != null)
                        {
                            Print(decimal_image);
                        }
                        else
                        {
                            Console.WriteLine("Строка не задана");
                        }
                        continue;
                    }
                case 6:
                    {
                        if (bynary_image == null)
                        {
                            Console.WriteLine("Матрица не задана");
                            continue;
                        }
                        ToDecimalString(0, 0, Dimension);
                        QuickSort(decimal_image, 0, decimal_image.Length - 1);
                        Array.Reverse(decimal_image);

                        int NewSize = Array.IndexOf(decimal_image, 0);
                        
                        // Нужно ли сжатие
                        if (NewSize != -1)
                        {
                            Array.Resize(ref decimal_image, NewSize);
                        }

                        From5To10(decimal_image);
                        continue;
                    }

                case 7:
                    {
                        if (decimal_image == null)
                        {
                            Console.WriteLine("Строка не задана");
                            continue;
                        }
                        ToBynaryMatrix();
                        continue;
                    }


                default:
                    break;
            }
            break;
        }

        //int[,] bynary_image =
        //{
        //    {0, 0, 0, 0, 0, 0, 0, 0},
        //    {0, 0, 1, 1, 1, 0, 0, 0},
        //    {0 ,1, 1, 0, 1, 1, 1, 1},
        //    {1 ,1, 0, 0, 1, 1, 1, 1},
        //    {0 ,0, 1, 1, 0, 1, 0, 1},
        //    {0 ,0, 1, 1, 0, 0, 1, 1},
        //    {0 ,0, 1, 1, 1, 0, 1, 0},
        //    {0 ,0, 1, 1, 1, 0, 0, 0}
        //};



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
                FindBlackSquare(0, 0, Dimension, decimal_image[i]);
            }
            From5To10(decimal_image);

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
                        bynary_image[k, r] = 1;
                    }
                }
            }
            else
            {
                k = Way % 5 - 1;
                r = Dimension / 2;
                FindBlackSquare(i + r * (k / 2), j + r * (k % 2), r, Way / 10);
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
