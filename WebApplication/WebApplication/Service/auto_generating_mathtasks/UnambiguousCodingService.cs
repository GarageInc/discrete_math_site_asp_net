﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Service.auto_generating_mathtasks
{
    public class UnambiguousCodingService
    {

        // Функция, которая генерирует случайную последовательность из нулей и единиц
        public static string GetRandomOneZeroString(int level)
        {
            Random r = new Random();
            List<string> res = new List<string>();

            // Количество последовательностей
            var count = r.Next(level+2, (level+2)*4);

            // Генерируем последовательности
            int i = 0;
            for (i = 0; i < count; i++)
            {
                string s = "";
                var length = r.Next(level + 2, (level + 2) * 4);

                for (int j = 0; j < length; j++)
                {
                    if (level > 1)
                    {
                        char symbol = r.Next(1, 100)%2 == 0 ? 'x' : 'y';
                        s += symbol;
                    }
                    else
                    {
                        s += r.Next(1, 100)%2;
                    }
                }

                res.Add(s);
            }

            string result = "";
            // Теперь склеиваем их в результат - кодовая строка типа 00110 000 1110 11 0 00101 0....
            for (i = 0; i < count - 1; i++)
            {
                result += res[i] + " ";
            }

            result += res[i];

            return result;
        }


        // Проверка правильности строки
        public static ReturnResult CheckString01(string reply, string generated)
        {
            // Проверим - нет ли в строке суффиксного/префиксного кода?
            // Если есть - вернём ошибку
            reply = reply.ToLowerInvariant();

            if (reply == "да" || reply == "нет")
            {
                bool yes = true;
                string error = "";
                List<string> res = generated.Split(' ').ToList();

                for (int i = 0; i < res.Count; i++)
                {
                    for (int j = 0; j < res.Count; j++)
                    {
                        if (j != i)
                        {
                            // Если какая-то строка является окончанием другой - это не хорошо, это ошибка
                            if (res[i].EndsWith(res[j]))
                            {
                                error = "[" + res[i] + "]" + " & " + "[" + res[j] + "]";
                                yes = false;
                                break;
                            }
                        }
                    }
                    if (!yes)
                        break;
                }

                if (reply == "да" && yes)
                    return new ReturnResult(true,"Абсолютно верное решение! Строка однозначно декодируется");
                if (reply == "нет" && !yes)
                    return new ReturnResult(true, "Абсолютно верное решение! Строка неоднозначно декодируется");
                if (reply == "да" && !yes)
                    return new ReturnResult(false, "Нет, строка неоднозначно декодируема. Не удовлетворяют суффиксному коду: " + error);
                if (reply == "нет" && yes)
                    return new ReturnResult(false, "Нет, строка однозначно декодируема!");
            }

            return new ReturnResult(false,"Ваш ответ не подходит под условие");
        }

    }
}