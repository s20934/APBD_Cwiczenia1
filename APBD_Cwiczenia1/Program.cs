using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace APBD_Cwiczenia1
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            //W sytuacji,kiedy parametr 1 nie został przekazany, powinniśmy zgłosić błąd ArgumentNullException
            if (args.Length < 1) throw new ArgumentNullException();
            var websiteURL = args[0];

            //W sytuacji kiedy przekazany parametr nie jest poprawnym adresem URL, powinniśmy zgłosić ArgumentException    
            if (Uri.IsWellFormedUriString(websiteURL, UriKind.Absolute) == false) throw new ArgumentException();
            
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(websiteURL);
            //Powinniśmy w poprawny sposób zwalniać zasoby (wykorzystanie metody Dispose()) związane z wykorzystaniem klasy HttpClient
            httpClient.Dispose();

            //Console.WriteLine(response);
            var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(response);
            
            //Regex adres mailowy
            var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");
            var matchCollection = regex.Matches(content);
            var set = new HashSet<string>();

            //W sytuacji,kiedy znaleźliśmy adresy -wyświetlamy je na konsoli. Chcielibyśmy wyświetlić wyłącznie unikalne adresy email.
            foreach (var item in matchCollection)
            {
                set.Add(item.ToString());

            }
            Console.WriteLine("Wynik wyszukiwania na stronie: "+ websiteURL);
            //Sprawdzanie zawartości HashSet
            if (set.Count == 0)
            {
                Console.WriteLine("Nie znaleziono adresów email");
            }

            foreach (var item in set)
            {

                Console.WriteLine(item);
            }
        }
    }
}