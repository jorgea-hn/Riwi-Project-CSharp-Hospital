using System;
using System.Globalization;

namespace HospitalSanVicente.UI
{
    public static class ConsoleReader
    {
        public static string ReadString(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(result))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(result));
            return result;
        }

        public static DateTime ReadDate(string prompt)
        {
            DateTime result;
            const string format = "yyyy-MM-dd HH:mm";
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParseExact(Console.ReadLine(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                Console.WriteLine($"Invalid date format. Please use '{format}' and try again.");
            }
        }

        public static int ReadMenuOption(string prompt, int maxOption)
        {
            int option;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out option) && option > 0 && option <= maxOption)
                {
                    return option;
                }
                Console.WriteLine($"Invalid option. Please enter a number between 1 and {maxOption}.");
            }
        }
    }
}
