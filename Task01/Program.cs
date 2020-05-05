using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо отфильтровать полученные коллекцию, оставив только отрицательные или четные числа.
 * Дважды вывести коллекцию, разделив элементы специальным символом.
 * Остальные указания см. непосредственно в коде!
 * 
 * Пример входных данных:
 * 1 2 3 4 5
 * 
 * Пример выходных:
 * 2:4
 * 2*4
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * В случае возникновения иных нештатных ситуаций (например, в случае попытки итерирования по пустой коллекции) 
 * выбрасывайте InvalidOperationException!
 * 
 */

namespace Task01 {
    class Program {
        static void Main(string[] args) {
            RunTesk01();
        }

        public static void RunTesk01() {
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

            // использовать синтаксис запросов!
            IEnumerable<int> arrQuery =
                from number in arr
                where number < 0 || number % 2 == 0
                select number;

            // использовать синтаксис методов!
            IEnumerable<int> arrMethod = arr.Where(num => num < 0 || num % 2 == 0);

            try {
                PrintEnumerableCollection<int>(arrQuery, ":");
                PrintEnumerableCollection<int>(arrMethod, "*");
            }
            catch (IOException ex) {
                Console.WriteLine("IOException");
            }
            catch (InvalidOperationException ex) {
                Console.WriteLine("InvalidOperationException");
                return;
            }
        }

        // Попробуйте осуществить вывод элементов коллекции с учетом разделителя, записав это ОДНИМ ВЫРАЖЕНИЕМ.
        // P.S. Есть два способа, оставьте тот, в котором применяется LINQ...
        public static void PrintEnumerableCollection<T>(IEnumerable<T> collection, string separator) {
            Console.WriteLine(
                collection.Select(t => t.ToString())
                    .Aggregate((elem1, elem2) => $"{elem1}{separator}{elem2}")); 
            // не совсем понятно, почему способа только два и какой из этого множества имеется ввиду.
            //   Альтернативное решение:
            // collection.Select(t => t.ToString())
            //  .Aggregate((elem1, elem2) => $"{elem1}{separator}{elem2}")
            //  .ToList()
            //  .ForEach(Console.Write)
            //  посимвольный вывод, зато всё в одно выражение
        }
    }
}
