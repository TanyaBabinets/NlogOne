using NLog.Targets;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
//TEКСТ ДЛЯ ФАЙЛА
//In summer I like to play outside. Я долго думал об орлах и понял многое:  
//Орлы летают в облаках, Летают, никого не трогая. When it comes the summertime. 1,2,3 - summertime!


//Создайте приложение для поиска информации в файле по текстовому шаблону. Варианты поддерживаемых шаблонов:
// Отобразить все предложения, содержащие хотя бы одну маленькую, английскую букву 
// Отобразить все предложения, содержащие хотя бы одну цифру 
// Отобразить все предложения, содержащие хотя бы одну большую, английскою букву 
//В программе настройте логирование с использованием NLog.

//В программе настройте логирование с использованием NLog.
namespace NlogOne
{
    internal class Program
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            //string text = "In summer I like to play outside.\r\nЯ долго думал об орлах\r\nИ понял многое:\r\nОрлы летают в облаках,\r\nЛетают, никого не трогая.\r\nWhen it comes the summertime. 1,2,3 - summertime!";
            

            string[] sentences = GetFromFile("NewText.txt");
                                                           
            Console.WriteLine("Предложения с мал англ буквой");
            Console.BackgroundColor = ConsoleColor.Green; 
            Console.ForegroundColor = ConsoleColor.Black; 
            string[] sentencesWithLowerLetters = FindWithLowerLetter(sentences);
            Console.ResetColor(); // Сброс цветов в исходное состояние
            foreach (var text in sentencesWithLowerLetters)
            {
                Console.WriteLine(text);
            }


            Console.WriteLine("\nПредложения с больш англ буквой");
            Console.BackgroundColor = ConsoleColor.Yellow; 
            Console.ForegroundColor = ConsoleColor.Black; 

            List<string> sentencesWithUpperLetters = FindWithUpperLetter(sentences);
            Console.ResetColor(); 
            foreach (var text in sentencesWithUpperLetters)
            {
                Console.WriteLine(text);
            }
          
            Console.WriteLine("\nПредложения с цифрой");
            Console.BackgroundColor = ConsoleColor.Blue; 
            Console.ForegroundColor = ConsoleColor.Black; 
            string[] sentencesWithDigit = FindWithDigit(sentences);
            Console.ResetColor();
            foreach (var text in sentencesWithDigit)
            {
                Console.WriteLine(text);
            }
            Console.ReadKey();

        }

        static string[] GetFromFile(string name)
        {
            string text = File.ReadAllText(name);
            return GetSentences(text);

        }

        static string[] GetSentences(string text)
        {

            return text.Split(new[] { ".", "!", "?" }, StringSplitOptions.RemoveEmptyEntries);
        }

        static string[] FindWithLowerLetter(string[] sentences)
        {
            string pattern = @"[a-z]+";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var result = Array.FindAll(sentences, s => regex.IsMatch(s));
           
          
            logger.Info($"\nНашли {result.Length} предложений с малой англ. буквой ");
            foreach (var v in result)
            {
                logger.Info($"{v}");
            }
            return result;
          
        }

        static List<string> FindWithUpperLetter(string[] sentences)
        {
          //  string pattern = @"[A-Z]";
            //Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            //var result = Array.FindAll(sentences, s => regex.IsMatch(s));
            List<string> result = sentences
            .Where(sentence => Regex.IsMatch(sentence, "[A-Z]"))
            .ToList();
            logger.Info($"\nНашли {result.Count} предложений с большой англ. буквой ");
            foreach (var v in result)
            {
                logger.Info($"{v}");
            }
            return result;
        }
        static string[] FindWithDigit(string[] sentences)
        {
            string pattern = @"\d+";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var result = Array.FindAll(sentences, s => regex.IsMatch(s));

            logger.Info($"\nНашли {result.Length} предложений с цыфрой ");
            foreach (var v in result)
            {
                logger.Info($"{v}");
            }
            return result;
        }
    }
}

 

           
            



    


