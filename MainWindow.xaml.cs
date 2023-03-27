using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using ПМ_02_Абдуллин_Владислав_Радомирович_билет__4.Word;



namespace ПМ_02_Абдуллин_Владислав_Радомирович_билет__4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal cost;
        public MainWindow()
        {
            InitializeComponent();

            performances.SelectedIndex = 0;
        }
        
        private void Calculate(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(count.Text))
            {
                if (vip.IsChecked != true && groundFloor.IsChecked != true && balcony.IsChecked != true)
                {
                    MessageBox.Show("Выберите зону", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                int numberCount = 0;

                try
                {
                    numberCount = Convert.ToInt32(count.Text); //Попытка получить количество из окна приложения

                    if (numberCount < 1) //Если количество меньше 1, то вызывается ошибка
                        throw new Exception();
                }
                catch
                {
                    MessageBox.Show("Введите коректное целое положительное число", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                decimal price = -1;

                switch (performances.SelectedIndex) //Проверка какое представление выбрано (стоимость билета задается в программе)
                {
                    case 0:
                        price = 100;
                        break;
                    case 1:
                        price = 150;
                        break;
                    case 2:
                        price = 90;
                        break;
                    case 3:
                        price = 250;
                        break;
                    case 4:
                        price = 300;
                        break;
                    case 5:
                        price = 50;
                        break;
                }

                if (price == -1)
                {
                    MessageBox.Show("Попробуйте выбрать другое представление", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                decimal cost = Calculate(price, numberCount, vip.IsChecked, groundFloor.IsChecked, balcony.IsChecked);

                this.cost = cost;

                result.Text = $"Общая стоимость билетов {cost}";
            }
            else
                MessageBox.Show("Укажите количество", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public decimal Calculate(decimal price, int count, bool? vip, bool? groundFloor, bool? balcony)
        {
            decimal cost = price;

            if (vip == true) //Если выбран вип, то к цене билета прибавляется 50%
                cost += price * Convert.ToDecimal(0.5);
            if (groundFloor == true) //Если выбран партер, то к цене билета прибавляется 7%
                cost += price * Convert.ToDecimal(0.07);
            if (balcony == true) //Если выбран балкон, то к цене билета прибавляется 20%
                cost += price * Convert.ToDecimal(0.2);

            cost *= count; //Умножение цены билета на количество билетов

            if (count > 30)
                cost = cost * Convert.ToDecimal(0.75); //Если больше 30 билетов, то скидка 25%
            else if (count > 20)
                cost = cost * Convert.ToDecimal(0.90); //Если больше 20 билетов, то скидка 10%
            else if (count > 15)
                cost = cost * Convert.ToDecimal(0.93); //Если больше 15 билетов, то скидка 7%
            else if (count > 10)
                cost = cost * Convert.ToDecimal(0.95); //Если больше 10 билетов, то скидка 5%

            return cost;
            this.cost = cost;
        }

        private void CreateWord(object sender, RoutedEventArgs e)
        {
            Random rnd1 = new Random();
            int c = rnd1.Next(0, 100000);

            string tovar = "";
            if (performances.SelectedIndex == 0)
            {
                tovar += "Красная шапочка";
            }
            if(performances.SelectedIndex == 1)
                {
                tovar += "Летучий корабль";
            }
            if (performances.SelectedIndex == 2)
                {
                tovar += "Лебединое озеро";
            }
            if (performances.SelectedIndex == 3)
                {
                tovar += "Донкихот";
            }
            if (performances.SelectedIndex == 4)
                {
                tovar += "Алые паруса";
            }

            var helper = new WordHelper("чек.docx");

            {
                var items = new Dictionary<string, string>
            {
               {"{итог}",Convert.ToString(cost)},
                  {"{Товар}",$@"{tovar }"},
                {"{дата}", Convert.ToString(DateTime.Now)},
                {"{Уникальный_номер}", Convert.ToString(c)}


            };
                helper.Process(items);

            }
        }

        private void AddPoster(object sender, RoutedEventArgs e)
        {
            
            if(performances.SelectedIndex == 0)
            {
               
            }
        }
    }
}
