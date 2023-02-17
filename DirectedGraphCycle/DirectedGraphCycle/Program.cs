using System;

namespace Cycles
{
    class Program
    {
        static bool Check(int[] used)
        {
            for (int i = 0; i < used.Length; i++)
            {
                if (used[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void GetNewArr(int[,] arr, int[,] newArr, int top)
        {
            int i1 = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                int j1 = 0;
                if (i != top)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (j != top)
                        {
                            newArr[i1, j1] = arr[i, j];
                            j1++;
                        }
                    }
                    i1++;
                }
            }
        }

        static bool DepthFirstSearch(int[,] newArr, int[] used, int startTop)
        {
            used[startTop] = 1;
            for (int i = 0; i < newArr.GetLength(0); i++)
            {
                if (newArr[startTop, i] == 1)
                {
                    if (used[i] == 1 && newArr[startTop, i] == 1)
                    {
                        return true;
                    }
                    if (used[i] == 0)
                    {
                        if (DepthFirstSearch(newArr, used, i))
                        {
                            used[i] = 2;
                            return true;
                        }
                        else
                        {
                            used[i] = 2;
                        }
                    }
                }
            }
            if (Check(used))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < used.Length; i++)
                {
                    if (used[i] == 0)
                    {
                        if (DepthFirstSearch(newArr, used, i))
                        {
                            used[i] = 2;
                            return true;
                        }
                        else
                        {
                            used[i] = 2;
                        }
                    }
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            string[] inputData = File.ReadAllLines("file.txt");
            int n = Convert.ToInt32(inputData[0]);
            int[,] arr = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                int[] temp = inputData[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
                for (int j = 0; j < n; j++)
                {
                    arr[i, j] = temp[j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
            int[,] newArr = new int[n - 1, n - 1];
            int[] used = new int[n - 1];
            for (int i = 0; i < n; i++)
            {
                Array.Clear(used);
                GetNewArr(arr, newArr, i);
                if (!DepthFirstSearch(newArr, used, 0))
                {
                    Console.WriteLine($"Можно удалить вершину {i}, чтобы в полученном орграфе не было циклов");
                    return;
                }

            }
            Console.WriteLine("Нельзя из заданного орграфа удалить вершину, чтобы в полученном орграфе не было циклов");
        }
    }
}