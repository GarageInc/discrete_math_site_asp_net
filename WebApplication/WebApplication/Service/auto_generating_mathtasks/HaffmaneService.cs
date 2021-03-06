﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApplication.Service.auto_generating_mathtasks
{
    public class HaffmaneService
    {

        // Функция, которая генерирует последовательность цифр от 0 до 9
        public static string GetRandomNumbersString( int level )
        {
            Random r = new Random();
            List<string> res = new List<string>();

            // Количество цифр
            var count = r.Next(level + 2, (level + 2) * 4);

            // Генерируем последовательности
            int i = 0;
            for (i = 0; i < count; i++)
            {
                var length = r.Next(level + 2, (level + 2)*4).ToString();

                res.Add(length);
            }

            string result = "";

            // Теперь склеиваем их в результат - кодовая строка типа 00110 000 1110 11 0 00101 0....
            for (i = 0; i < count; i++)
            {
                result += res[i];
            }

            return result;
        }


        // Проверка правильности строки для кода Хаффмана
        public static ReturnResult CheckString04(string reply, string generated)
        {
            try
            {
                bool yes = true;

                string input = generated;

                if (input.CompareTo("") == 0)
                {
                    return new ReturnResult(false, "Вы не ввели текст!");
                }

                HuffmanTree huffmanTree = new HuffmanTree();

                // Строим дерево Хаффмана по весам слов
                huffmanTree.Build(input);

                // Кодируем
                BitArray encoded = (BitArray)huffmanTree.Encode(input);

                if (huffmanTree.str != reply)
                    yes = false;

                if (yes)
                    return new ReturnResult(true, "Абсолютно верное решение! ");

                return new ReturnResult(false, "Это не оптимальный код Хаффмана. Вот он: " + huffmanTree.str);

            }
            catch (Exception e)
            {
                return new ReturnResult(false, "Ошибка ввода! " + e.Message);
            }
        }
    }
}