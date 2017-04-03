using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            var rndlist = new int[10].Select(s => rnd.Next(100)).ToList(); //new List<int>(new[]{ 16, 29, 50, 46, 55, 50, 76, 53, 34, 90 })

            rndlist.ForEach(s => Console.Write($"{s} "));
            Console.WriteLine();

            LookAround(rndlist);
            Console.ReadLine();
        }

        public static void Print(List<int> source, int intIndex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            source.Take(intIndex).ToList().ForEach(s => Console.Write($"{s} "));
            Console.ResetColor();
            source.Skip(intIndex).ToList().ForEach(s => Console.Write($"{s} "));
            Console.WriteLine();

        }

        private static void LookAround(List<int> source)
        {

            var intMaxNumber = source.Count - 1;
            var point = 0;

            while (point <= intMaxNumber)
            {
                byte flag = 0;
                point = 0;

                IEnumerable<int> temp;

                temp = source.TakeWhile((s, index) => index < intMaxNumber && ((flag < 1 ? (source[index] >= source[index + 1]) || flag++ < 1 : flag++ < 2 && source[0] > source[index])) || ((index == intMaxNumber) && source[0] > source[index]));

                var tempList = temp.ToList();
                var intCount = tempList.Count;
                if (intCount <= 1)
                {
                    temp =
                        source.TakeWhile(
                            s => point < intMaxNumber  && (flag < 3 &&(source[point] <= source[++point] || flag++ < 3)) || ((point == intMaxNumber) && source[point] >= source[point - 1]));
                    tempList = temp.ToList();
                    intCount = tempList.Count;
                    if (point == intMaxNumber && source[point] >= source[point - 1]) point++;
                }

                source.TurnAndTake(intCount);

                Print(source, intCount);
                /*
                ((temp = source.TakeWhile((s, index) => index < intMaxNumber && source[index] >= source[index + 1]))
                 .Count() != 0
                        ? temp
                        : (temp =
                            source.TakeWhile(
                                s => point < intMaxNumber && (source[point] <= source[++point] || ++flag != 0))))
                    .ToList().TurnAndTake(temp.Count());
                */

            }
        }


    }

    internal static class Stop
    {
        public static void TurnAndTake(this IList<int> source, int indexValue)
        {
            source.Take(indexValue).ToList().ForEach(s => source[--indexValue] = s);
        }
    }
}
