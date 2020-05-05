using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо оставить только те элементы коллекции, которые предшествуют нулю, или все, если нуля нет.
 * Дважды вывести среднее арифметическое квадратов элементов новой последовательности.
 * Вывести элементы коллекции через пробел.
 * Остальные указания см. непосредственно в коде.
 * 
 * Пример входных данных:
 * 1 2 0 4 5
 * 
 * Пример выходных:
 * 2,500
 * 2,500
 * 1 2
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
 */
namespace Task02 {
    class Program {
        static void Main(string[] args) {
            RunTesk02();
        }

        public static void RunTesk02() {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

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


            var filteredCollection = arr.TakeWhile(num => num != 0).ToList();

            try {
                checked {
                    var sqrFilteredCollection = filteredCollection.Select(x => x * x).ToList();

                // использовать статическую форму вызова метода подсчета среднего
                double averageUsingStaticForm = Enumerable.Average(sqrFilteredCollection);
                // использовать объектную форму вызова метода подсчета среднего
                double averageUsingInstanceForm = sqrFilteredCollection.Average();

                Console.WriteLine($"{averageUsingStaticForm:f3}".Replace('.', ','));
                Console.WriteLine($"{averageUsingInstanceForm:f3}".Replace('.', ','));

                // вывести элементы коллекции в одну строку 
                Console.WriteLine(
                    filteredCollection.Select(t => t.ToString())
                        .Aggregate((elem1, elem2) => $"{elem1} {elem2}"));
                }

            }
            catch (OverflowException) {
                Console.WriteLine("OverflowException");
            }
            catch (InvalidOperationException) {
                Console.WriteLine("InvalidOperationException");
            }
        }

    }
}
