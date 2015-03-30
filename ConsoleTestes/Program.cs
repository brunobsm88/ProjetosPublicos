using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ConsoleTestes
{
    class Program
    {
        public static List<string> Lista { get; set; }

        static void Main(string[] args)
        {
            Lista = new List<string>();
            var nome = "Bruno dos Santos Moura";
            var ar = nome.Split(' ');
            //nova linha github
            var tamanhoMaximo = 30;
            var subDominio = string.Empty;

            foreach (var s in ar)
            {
                if ((subDominio + s).Length <= 30)
                {
                    subDominio += s;
                    if (!Lista.Exists(p => p.Contains(subDominio)))
                    {
                        Lista.Add(subDominio);
                        Console.WriteLine(subDominio);
                    }
                }
            }
            subDominio = string.Empty;
            foreach (var s in ar)
            {
                if ((subDominio + s).Length <= 27)
                {
                    subDominio += s;
                    for (int i = 1; i <= 10; i++)
                    {
                        var temp = subDominio + i.ToString();
                        if (!Lista.Exists(p => p.Contains(temp)))
                        {
                            //subDominio = temp;
                            Lista.Add(temp);
                            Console.WriteLine(temp);
                        }
                    }
                }
            }


            Console.ReadLine();
        }

    }
}
