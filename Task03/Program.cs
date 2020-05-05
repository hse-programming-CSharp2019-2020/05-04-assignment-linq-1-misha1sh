using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*Все действия по обработке данных выполнять с использованием LINQ
 * 
 * Объявите перечисление Manufacturer, состоящее из элементов
 * Dell (код производителя - 0), Asus (1), Apple (2), Microsoft (3).
 * 
 * Обратите внимание на класс ComputerInfo, он содержит поле типа Manufacturer
 * 
 * На вход подается число N.
 * На следующих N строках через пробел записана информация о компьютере: 
 * фамилия владельца, код производителя (от 0 до 3) и год выпуска (в диапазоне 1970-2020).
 * Затем с помощью средств LINQ двумя разными способами (как запрос или через методы)
 * отсортируйте коллекцию следующим образом:
 * 1. Первоочередно объекты ComputerInfo сортируются по фамилии владельца в убывающем порядке
 * 2. Для объектов, у которых фамилии владельцев сопадают, 
 * сортировка идет по названию компании производителя (НЕ по коду) в возрастающем порядке.
 * 3. Если совпадают и фамилия, и имя производителя, то сортировать по году выпуска в порядке убывания.
 * 
 * Выведите элементы каждой коллекции на экран в формате:
 * <Фамилия_владельца>: <Имя_производителя> [<Год_производства>]
 * 
 * Пример ввода:
 * 3
 * Ivanov 1970 0
 * Ivanov 1971 0
 * Ivanov 1970 1
 * 
 * Пример вывода:
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * 
 *  * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * При некорректных входных данных (не связанных с созданием объекта) выбрасывайте FormatException
 * При невозможности создать объект класса ComputerInfo выбрасывайте ArgumentException!
 */
namespace Task03 {
    class Program {
        static void Main(string[] args) {
            int N;
            List<ComputerInfo> computerInfoList = new List<ComputerInfo>();
            try {
                // считываем число компьютеров
                N = int.Parse(Console.ReadLine());
                if (N <= 0)
                    throw new FormatException("N должно быть положительно");
                for (int i = 0; i < N; i++) {
                    var spl = Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (spl.Length < 3)
                        throw new ArgumentException("Мало аргументов");
                    
                    string name = spl[0];
                    
                    int year = int.Parse(spl[1]);
                    
                    int code = int.Parse(spl[2]);
                    if (!Enum.IsDefined(typeof(Manufacturer), code))
                        throw new ArgumentException("Некорркетный код");
                    Manufacturer manufacturer = (Manufacturer)code;
                    
                    computerInfoList.Add(new ComputerInfo(name, manufacturer, year));
                }
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
            catch (ArgumentException ex) {
                Console.WriteLine("ArgumentException");
                return;
            }

            // выполните сортировку одним выражением
            var computerInfoQuery = from computer in computerInfoList
                orderby computer.Owner descending, computer.ComputerManufacturer.ToString() , computer.Year descending
                select computer;

            PrintCollectionInOneLine (computerInfoQuery);

            Console.WriteLine();

            // выполните сортировку одним выражением
            var computerInfoMethods = computerInfoList
                .OrderByDescending(o => o.Owner)
                .ThenBy(o => o.ComputerManufacturer.ToString())
                .ThenByDescending(o => o.Year);
            PrintCollectionInOneLine(computerInfoMethods);
        }

        // выведите элементы коллекции на экран с помощью кода, состоящего из одной линии (должна быть одна точка с запятой)
        // Попробуйте осуществить вывод элементов коллекции с учетом разделителя, записав это ОДНИМ ВЫРАЖЕНИЕМ.
        // P.S. Есть два способа, оставьте тот, в котором применяется LINQ...
        /// <summary>
        ///     Выводит коллекцию 
        /// </summary>
        /// <param name="collection">Коллекция</param>
        public static void PrintCollectionInOneLine(IEnumerable<ComputerInfo> collection) {
            collection.Select(t => $"{t.Owner}: {t.ComputerManufacturer} [{t.Year}]")
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }

    /// <summary>
    ///     Изготовитель
    /// </summary>
    enum Manufacturer {
        Dell = 0,
        Asus = 1,
        Apple = 2,
        Microsoft = 3
    }

    /// <summary>
    ///     Класс для информации о компьбтере
    /// </summary>
    class ComputerInfo {

        /// <summary>
        ///     Конструктор информации о компьютере
        /// </summary>
        /// <param name="owner">Владелец</param>
        /// <param name="computerManufacturer">Изготовитель</param>
        /// <param name="year">Год выпуска</param>
        public ComputerInfo(string owner, Manufacturer computerManufacturer, int year) {
            Owner = owner;
            ComputerManufacturer = computerManufacturer;
            Year = year;
        }

        /// <summary>
        ///     Владелец компьютера
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        ///     Изготовитель
        /// </summary>
        public Manufacturer ComputerManufacturer { get; set; }
        /// <summary>
        ///     Год выпуска
        /// </summary>
        public int Year { get; set; }
    }
}