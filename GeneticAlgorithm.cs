using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace is_lab_3
{
    internal class GenAlgorithm
    {
        // Метод для вычисления общей стоимости текущей популяции
        public double SUM(List<Product> population)
        {
            double sum = 0; 
            foreach (Product product in population) // Перебор всех товаров в популяции
            {
                sum += product.Count * product.Price; // Добавление стоимости товара (цена * текущее количество)
            }
            return sum; 
        }

        // Основной метод генетического алгоритма, который выполняет заданное количество итераций
        public List<Product> GetPopulationResult(List<Product> population, int generationsCount, double objective)
        {
            List<Product> _population = CopyList(population); // Создание копии начальной популяции
            for (int i = 0; i < generationsCount; i++) // Цикл по количеству итераций
            {
                var (pop1, pop2) = Mutation(_population); // Мутация популяции.
                var (pop3, pop4) = Crossing(pop1, pop2); // Скрещивание популяций.
                _population = Selection(new List<List<Product>> { _population, pop1, pop2, pop3, pop4 }, objective); // Селекция лучших решений
            }
            return _population; // Возврат итоговой популяции после всех итераций
        }

        // Метод для создания копии списка товаров
        public List<Product> CopyList(List<Product> list)
        {
            List<Product> res = new List<Product>(); // Инициализация нового списка.
            foreach (var item in list) // Перебор всех элементов в исходном списке.
            {
                res.Add(item.Copy()); // Добавление копии каждого товара в новый список.
            }
            return res; // Возврат копии списка.
        }

        // Метод для выполнения мутации в популяции
        public (List<Product>, List<Product>) Mutation(List<Product> population)
        {
            Random random = new Random(); // Инициализация генератора случайных чисел
            List<Product> pop1 = CopyList(population); // Создание копии популяции
            List<Product> pop2 = CopyList(population); // Создание второй копии популяции

            int n = random.Next(0, pop1.Count / 2); // Выбор случайного индекса в первой половине списка
            int m = random.Next(pop2.Count / 2, pop2.Count); // Выбор случайного индекса во второй половине списка

            pop1[n].RandomCount(); // Мутация случайного элемента в первой популяции
            pop2[m].RandomCount(); // Мутация случайного элемента во второй популяции

            return (pop1, pop2); // Возврат двух мутированных популяций.
        }

        // Метод для выполнения скрещивания двух популяций
        public (List<Product>, List<Product>) Crossing(List<Product> population1, List<Product> population2)
        {
            List<Product> pop3 = CopyList(population1); // Создание копии первой популяции
            List<Product> pop4 = CopyList(population2); // Создание копии второй популяции

            for (int i = pop3.Count / 2; i < pop3.Count; i++) // Перебор второй половины первой популяции
            {
                pop3[i].Count = population2[i].Count; // Замена количества во второй половине первой популяции на значения из второй популяции
            }

            for (int i = pop4.Count / 2; i < pop4.Count; i++) // Перебор второй половины второй популяции
            {
                pop4[i].Count = population1[i].Count; // Замена количества во второй половине второй популяции на значения из первой популяции
            }

            return (pop3, pop4); // Возврат двух новых популяций после скрещивания
        }

        // Метод для выбора лучшей популяции на основе близости к целевому значению
        public List<Product> Selection(List<List<Product>> populations, double objective)
        {
            //добавить ещё начальную популяцию
            List<Product> result = CopyList(populations[4]); // Инициализация результата последней популяцией
            for (int i = 0; i < 4; i++) // Перебор первых четырех популяций
            {
                if (Math.Abs(SUM(populations[i]) - objective) <= Math.Abs(SUM(result) - objective)) // Проверка, какая популяция ближе к целевому значению
                {
                    result = CopyList(populations[i]); // Если текущая популяция лучше, она становится результатом
                }
            }
            return result; 
        }
    }
}