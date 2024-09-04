using System;

namespace NeaLibrary.DataStructures
{
    public class BinarySearch
    {
        static public int binarySearch<T>(T[] arr, T target) where T : IComparable
        {
            int l = arr.Length;

            int i = l / 2;
            try
            {
                if (target.CompareTo(arr[i]) == 0) { return i; }
                else if (target.CompareTo(arr[i]) < 0)
                {
                    //go left
                    Console.WriteLine($"{target} < {arr[i]}  going left");
                    T[] newArr = new T[i];
                    for (int n = 0; n < i; n++)
                    {
                        newArr[n] = arr[n];
                        Console.Write($"{newArr[n]} ");
                    }
                    Console.WriteLine();
                    return binarySearch(newArr, target);
                }
                else
                {
                    //go right
                    Console.WriteLine($"{target} > {arr[i]}  going right");
                    T[] newArr = new T[l - i - 1];
                    for (int n = l - 1; n > i; n--)
                    {
                        newArr[n - i - 1] = arr[n];
                        Console.Write($"{newArr[n - i - 1]} ");
                    }
                    Console.WriteLine();
                    int r = binarySearch(newArr, target);
                    return r >= 0 ? i + r + 1 : -1;
                }


            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }

        public static int binarySearchProcedural<T>(T[] arr, T target) where T : IComparable
        {
            int left = 0;
            int right = arr.Length - 1;
            int i;
            bool found = false;
            do
            {
                i = (left + right) / 2;  //integer division so middle left if odd
                if (target.CompareTo(arr[i]) == 0) { found = true; return i; }
                else if (left == right)
                {
                    return -1;
                }
                else if (target.CompareTo(arr[i]) < 0)
                {
                    //target<arr[i]
                    right = i - 1;
                }
                else
                {
                    //target>arr[i]
                    left = i + 1;
                }

            } while (!found);
            return i;
        }
    }
}