using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LuisBot.Helpers
{
    public class Strings
    {
        public static string Help {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("# Дід панас я \n");
                sb.AppendLine("Пити вмію. Їсти вмію. Спати вмію. І трохи кусати:");
                sb.AppendLine("Пиши:\n");
                sb.AppendLine("*  **Коли в нас свято**, щоб дізнатися що тебечекає");
                sb.AppendLine("*  питай про **робочі години** чи **скільки я вже відпахав** -- все скажу ");
                sb.AppendLine("*  можу **пустити в хату** а можу і не пустити, якщо чужий ти.");
                sb.AppendLine("# ПРО Панаса\n");
                sb.AppendLine("  v0.1.0.0 \n");

                return sb.ToString();
            }
        }
    }
}