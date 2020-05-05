using System;
using System.IO;
using System.Linq;

/*
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * На основе полученных чисел получить новое по формуле: 5 + a[0] - a[1] + a[2] - a[3] + ...
 * Это необходимо сделать двумя способами:
 * 1) с помощью встроенного LINQ метода Aggregate
 * 2) с помощью своего метода MyAggregate, сигнатура которого дана в классе MyClass
 * Вывести полученные результаты на экран (естесственно, они должны быть одинаковыми)
 * 
 * Пример входных данных:
 * 1 2 3 4 5
 * 
 * Пример выходных:
 * 8
 * 8
 * 
 * Пояснение:
 * 5 + 1 - 2 + 3 - 4 + 5 = 8
 * 
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 */

namespace Task04
{
    class Program {
        static void Main(string[] args) {
            RunTesk04();
        }

        public static void RunTesk04() {
            int[] arr;
            try {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = Console.ReadLine()
                    .Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                if (arr.Length == 0)
                    throw new InvalidOperationException();
            }
            catch (FormatException ex) {
                Console.WriteLine("FormatException");
                return;
            }
            catch (IOException ex) {
                Console.WriteLine("IOException");
                return;
            }
            catch (InvalidOperationException ex) {
                Console.WriteLine("InvalidOperationException");
                return;
            }
            catch (OverflowException ex) {
                Console.WriteLine("OverflowException");
                return;
            }

            // использовать синтаксис методов! SQL-подобные запросы не писать!

            try {
                checked {
                    int arrAggregate = 5 + arr.Select((num, index) => index % 2 == 0 ? num : -num)
                                           .Aggregate(((num1, num2) => num1 + num2));
                    int arrMyAggregate = MyClass.MyAggregate(arr);
                    
                    // выводим результат
                    Console.WriteLine(arrAggregate);
                    Console.WriteLine(arrMyAggregate);
                }
            }
            catch (OverflowException e) {
                Console.WriteLine("OverflowException");
                return;
            }


        }
    }

    static class MyClass
    {
        /// <summary>
        ///    Складывает числа по формуле: 5 + a[0] - a[1] + a[2] - a[3] + ...
        /// </summary>
        /// <param name="arr">Массив чисел</param>
        public static int MyAggregate(int[] arr) {
            int res = 5;
            for (int i = 0; i < arr.Length; i++) {
                if (i % 2 == 0) {
                    res += arr[i];
                } else {
                    res -= arr[i];
                }
            }

            return res;
        }
    }
}
