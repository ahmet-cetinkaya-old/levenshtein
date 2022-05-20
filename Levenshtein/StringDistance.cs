using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levenshtein
{
    internal class StringDistance
    {
        private string source;
        private string target;

        public StringDistance(string source, string target)
        {
            this.source = source;
            this.target = target;
        }

        public Tuple<int, int[,]> levenshtein()
        {
            int s = this.source.Length;
            int t = this.target.Length;

            // Kelime boyları ile hesaplama matrisi oluşturulur. Boşluk karakteri ile de yapılacak karşılaştırmadan bir fazlası kullanılmıştır.
            int[,] matrix = new int[s + 1, t + 1];

            // Eğer değerlerinden biri boş ise diğerinin uzunluğu kadar maliyet çıkacaktır. Bunların kontrolü yaparak fonksiyon bitirilebilir.
            if (s == 0) return Tuple.Create(t, matrix);
            if (t == 0) return Tuple.Create(s, matrix);

            // Yatay ve düşey eksenlerdeki standart boşluk elemanları doldurulur.
            for (int i = 0; i <= s; i++) matrix[i, 0] = i;
            for (int j = 0; j <= t; j++) matrix[0, j] = j;

            // Harfleri tek tek gezerek karşılaştırma işlemleri yapılır. Sonuca göre ilgili matriks elemanına değer atanır.
            for (int i = 1; i <= s; i++)
            {
                for (int j = 1; j <= t; j++)
                {
                    if (this.source[i - 1] == this.target[j - 1]) matrix[i, j] = matrix[i - 1, j - 1]; // Harflerin eşit olması durumunda sol üst çaprazındaki eleman değiştirilmeden atanır.
                    else matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j], matrix[i, j - 1]), matrix[i - 1, j - 1]) + 1; // Farklı olması durumunda önceki değerlere bir eklenerek değeri en küçük olan atanır.
                }
            }

            return Tuple.Create(matrix[s, t], matrix);
        }
    }
}