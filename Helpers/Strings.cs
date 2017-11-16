using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LuisBot.Helpers
{
    public class Strings
    {
        public static List<string> Greetings
        {
            get
            {
                return new List<string>()
                {
                    "Здоров був малий, а ти чий будеш?",
                    "Привіт, як настрій?",
                    "Вечір в хату!",
                    "Здоров був. І що далі?"
                };
            }
        }

        public static string RandomGreeting => Greetings[new Random().Next(0, Greetings.Count)];

        public static string Help {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("# Дід панас я \n");
                sb.AppendLine("Пити вмію. Їсти вмію. Спати вмію. І трохи кусати:");
                sb.AppendLine("Пиши:\n");
                sb.AppendLine("*  **Коли в нас свято**, щоб дізнатися що тебечекає");
                sb.AppendLine("*  питай про **робочі години** чи **скільки я вже відпахав** -- все скажу ");
                sb.AppendLine("*  **інформація про бонуси та відпустку**, якщо це тебе цікавить ");
                sb.AppendLine("*  можу **пустити в хату** а можу і не пустити, якщо чужий ти.");
                sb.AppendLine("## ПРО Панаса\n");
                sb.AppendLine("  v0.1.0.0 \n");

                return sb.ToString();
            }
        }

        public static string VoteResult
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("# Ваш голос зараховано! \n");
                sb.AppendLine("## Поточний результат \n");
                sb.AppendLine("*  **Кіт**     :  26%");
                sb.AppendLine("*  **Собака**  :  22%");
                sb.AppendLine("*  **Сова**    :  52");

                return sb.ToString();
            }
        }
    }
}