using System;
using System.Collections.Generic;
using System.Linq;

namespace FindNumberInStringStream
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> d = new Dictionary<int, string>();
            int ctr = 1;

            string s = string.Empty;
            foreach (var i in ExtractNumbers("A-j1df53a25dasf12-679879jkk--2344nn99---sfwergfwe141---54a1-55"))
            {
                if (i == -100 || i == -99)
                {
                    s = string.Empty;
                    ctr++;

                }
                else if (i != -100)
                {
                    s += i;
                    if (d.TryGetValue(ctr, out string r))
                    {
                        d[ctr] = s;
                    }
                    else
                        d.Add(ctr, s);
                }

            }
            d.Values.ToList().ForEach(o => Console.WriteLine(o));
            Console.ReadLine();
        }

        private static IEnumerable<int> ExtractNumbers(IEnumerable<char> input)
        {
            var e = input.GetEnumerator();
            bool isPreviousMinus = false;
            while (e.MoveNext())
            {
                if (e.Current.ToString() == "-")
                {
                    if (isPreviousMinus)
                        continue;
                    else
                        yield return -99;
                    isPreviousMinus = true;
                    continue;
                }

                if (Int32.TryParse(e.Current.ToString(), out var number))
                {                    
                    if (isPreviousMinus)
                    {
                        isPreviousMinus = false;
                        yield return number * -1;
                    }
                    else
                        yield return number;
                }
                else
                {
                    isPreviousMinus = false;
                    yield return -100; //this is a signal to break the main loop and store the accumulated numbers
                }
            }
        }
    }
}
