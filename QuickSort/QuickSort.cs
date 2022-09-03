namespace MyLib
{
    public class Sort
    {
        public static int[] QuickSort(int[] array, int startIndex, int finishIndex)
        {
            if (startIndex >= finishIndex)
            {
                return array;
            }

            int pivotIndex = GetPivotIndex(array, startIndex, finishIndex);

            // рекурсия с левым и правым подмассивами
            QuickSort(array, startIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, finishIndex);

            return array;
        }

        private static int GetPivotIndex(int[] array, int startIndex, int finishIndex)
        {
            int pivot = startIndex - 1;

            for (int i = startIndex; i <= finishIndex; i++)
            {
                if (array[i] < array[finishIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            // перестановка опорного элемента(pivot)
            pivot++;
            Swap(ref array[pivot], ref array[finishIndex]);

            return pivot;
        }

        private static void Swap(ref int left, ref int right)
        {
            (right, left) = (left, right);
        }

    }
}
