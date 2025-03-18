using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace is_lab_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeFlowerList();
        }

        private void InitializeFlowerList()
        {
            for (int i = 0; i < flowers.Length; i++)
            {
                FlowerList.Rows.Add();
                FlowerList.Rows[i].Cells[0].Value = (i + 1).ToString();
                FlowerList.Rows[i].Cells[1].Value = flowers[i];
                FlowerList.Rows[i].Cells[2].Value = $"{minQuantities[i]}-{maxQuantities[i]}";
                FlowerList.Rows[i].Cells[3].Value = prices[i].ToString();
            }
            var (minBudget, maxBudget) = CalculateBudgetRange();
            BudgetRangeLabel.Text = $"Диапазон бюджета: от {minBudget} до {maxBudget}";
        }

        private string[] flowers = new string[]
        {
            "Роза", "Тюльпан", "Лилии", "Гвоздика", "Орхидея", "Пион", "Хризантема", "Рускус", "Ирис", "Гипсофила Паникулята уайт",
            "Альстромерия", "Аспидистра", "Нарцисс", "Анемон", "Георгин", "Лаванда", "Подсолнух", "Мимоза", "Эустома", "Астра",
            "Питтоспорум", "Гортензия", "Ландыш", "Маргаритка", "Аспарагус", "Ромашка", "Фиалка", "Цинния", "Календула", "Берграс",
            "Писташ", "Петуния", "Папоротник", "Бегония", "Анютины глазки", "Лютик", "Крокус", "Гладиолус", "Лилия", "Мак",
            "Гиацинт", "Гербера", "Герань", "Пижма", "Гипсофила Снежинка",  "Фрезия", "Сирень", "Клематис", "Эвкалипт", "Бархатцы"
        };

        private int[] prices = new int[]
        {
            //173, 58, 143, 3, 228, 139, 47, 7, 23, 3,
            //45, 5, 77, 25, 115, 7, 211, 25, 213, 227,
            //7, 135, 55, 19, 269, 29, 8, 101, 89, 7,
            //5, 175, 5, 2, 59, 149, 159, 109, 217, 69,
            //27, 21, 237, 3, 5, 212, 129, 3, 2, 111
             10, 25, 5, 3, 6, 9, 7, 140, 1, 3, 250, 70, 8, 4, 6, 300, 1, 12, 23, 50, 35, 45, 160, 31, 29, 23, 200, 18, 15, 3, 14, 29,  27,  8, 112, 100, 5, 250,  130,
            100, 44, 350, 28, 190, 22, 21, 14, 1, 2, 23
    };

        private int[] minQuantities = new int[]
        {
            //1, 2, 1, 3, 1, 2, 2, 4, 2, 4,
            //1, 4, 3, 2, 2, 4, 2, 2, 3, 2,
            //7, 2, 3, 3, 1, 2, 1, 2, 2, 5,
            //7, 2, 5, 5, 1, 1, 2, 2, 2, 2,
            //1, 2, 3, 5, 5, 1, 1, 5, 7, 2
            2, 6, 1, 5, 3, 1, 4, 7, 2, 8, 3, 6, 1, 5, 3, 1, 4, 7, 2, 8, 3, 6, 1, 5, 3, 1, 4, 7, 2, 8, 3, 6, 1, 5, 3, 1, 4, 7, 2, 8, 3, 3, 1, 5, 3, 1, 4, 7, 2, 8
        };

        private int[] maxQuantities = new int[]
        {
            //3, 4, 3, 9, 3, 4, 4, 8, 5, 8,
            //5, 6, 4, 5, 5, 8, 5, 5, 5, 5,
            //10, 5, 5, 5, 2, 4, 5, 4, 4, 8,
            //10, 4, 8, 8, 4, 3, 4, 4, 4, 5,
            //4, 5, 4, 8, 7, 3, 2, 7, 9, 5
            5, 9, 3, 8, 6, 15, 7, 10, 5, 12, 6, 9, 3, 8, 6, 10, 7, 10, 5, 12, 6, 9, 3, 8, 6, 10, 7, 10, 5, 8, 6, 9, 3, 8, 6, 5, 7, 10, 5, 8, 6, 9, 3, 8, 6, 8, 7, 10, 5, 12
        };

        private (int minBudget, int maxBudget) CalculateBudgetRange()
        {
            int minBudget = 0;
            int maxBudget = 0;
            for (int i = 0; i < flowers.Length; i++)
            {
                minBudget += minQuantities[i] * prices[i];
                maxBudget += maxQuantities[i] * prices[i];
            }
            return (minBudget, maxBudget);
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BudgetTextBox.Text) || string.IsNullOrEmpty(IterationsTextBox.Text))
            {
                MessageBox.Show("Введите бюджет и количество итераций.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int budget = Convert.ToInt32(BudgetTextBox.Text);
            int generations = Convert.ToInt32(IterationsTextBox.Text);

            var (minBudget, maxBudget) = CalculateBudgetRange();
            if (budget < minBudget || budget > maxBudget)
            {
                MessageBox.Show($"Введенный бюджет вне допустимого диапазона. Пожалуйста, введите значение от {minBudget} до {maxBudget}.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Создаем список товаров
            List<Product> products = new List<Product>();
            for (int i = 0; i < flowers.Length; i++)
            {
                products.Add(new Product(flowers[i], prices[i], minQuantities[i], maxQuantities[i]));
            }

            //Генетический алгоритм
            GenAlgorithm genAlgorithm = new GenAlgorithm();
            List<Product> result = genAlgorithm.GetPopulationResult(products, generations, budget);

            // Обновляем интерфейс
            for (int i = 0; i < result.Count; i++)
            {
                FlowerList.Rows[i].Cells[4].Value = result[i].Count.ToString();
            }
            int totalCost = (int)genAlgorithm.SUM(result);
            ResultLabel.Text = $"Итоговая стоимость: {totalCost}";
            int difference = Math.Abs(totalCost - budget);
            label3.Text = $"Разница с бюджетом: {difference}";
        }
    }

    // Класс Product для представления товара (цветка)
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Count { get; set; }

        public Product(string name, double price, int min, int max)
        {
            Name = name;
            Price = price;
            Min = min;
            Max = max;
            Count = min; // Начальное количество
        }

        public void RandomCount()
        {
            Random random = new Random();
            Count = random.Next(Min, Max + 1);
        }

        public Product Copy()
        {
            return new Product(Name, Price, Min, Max) { Count = Count };
        }
    }
}