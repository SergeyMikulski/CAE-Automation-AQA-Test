using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAE_TestProject.TestData
{
    internal static class TestData
    {
        public static Dictionary<string, List<string>> Test2FirstData()
        {
            var dictionary = new Dictionary<string, List<string>>()
            {
                {"catalogItemToHover", new List<string>() {"Компьютеры и периферия" } },
                {"good", new List<string>() {"Ноутбуки" } },
                {"priceMin", new List<string>() {"2000" } },
                {"priceMax", new List<string>() {"22000" } },
                {"deliveryDate", new List<string>() {"Сегодня или завтра" } },
                {"producer", new List<string>() {"Lenovo", "Xiaomi" } },
                {"type", new List<string>() {"игровой (геймерский)" } },
                {"screenDiagonalMin", new List < string >() { "15.4" } },
                {"screenResolutionMin", new List < string >() { "1920x1080 (FullHD)" } },
                {"popularParameters", new List < string >() { "Подсветка клавиатуры", "Цифровое поле (Numpad)" } },
                {"ramMin", new List < string >() { "16 ГБ" } },
                {"ramMax", new List < string >() { "32 ГБ" } },
                {"ramType", new List < string >() { "DDR5" } },
                {"cpu", new List < string >() { "AMD Ryzen 5", "Intel Core i5" } },
                {"coresMin", new List < string >() { "8" } },
                {"launchDateMin", new List < string >() { "" } },
                {"launchDateMax", new List < string >() { "" } },
                {"productLine", new List < string >() { "" } }
            };

            return dictionary;
        }

        public static Dictionary<string, string> FilterHeaderNames()
        {
            var dictionary = new Dictionary<string, string>()
            {
                {"type", "Тип" },
                {"screenDiagonalMin", "Диагональ экрана" },
                {"screenResolutionMin", "Разрешение экрана" },
                {"ramMin", "Объем оперативной памяти" },
                {"ramMax", "Объем оперативной памяти" },
                {"cpu", "Процессор" },
                {"coresMin", "Количество ядер" },
                {"launchDateMin", "Дата выхода на рынок" },
                {"launchDateMax", "Дата выхода на рынок" },
                {"productLine", "Линейка" },

            };

            return dictionary;
        }
    }
}
