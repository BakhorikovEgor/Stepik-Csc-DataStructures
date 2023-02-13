//Телефонная книга
//Реализовать структуру данных, эффективно обрабатывающую запросы вида add number name, del number и find number


//Релизация будет на основе прямой адресации



using System;
using System.Text;

class Program
{
    static void Main()
    {
        string[] allPhoneNumbers = new string[10000000];

        int requestNumber = int.Parse(Console.ReadLine());
        StringBuilder output = new StringBuilder();

        for (int i = 0; i < requestNumber; i++)
        {
            string[] request = Console.ReadLine().Split();

            switch (request[0][0])
            {
                case 'a':
                    allPhoneNumbers[int.Parse(request[1])] = request[2];
                    break;

                case 'f':
                    output.Append(allPhoneNumbers[int.Parse(request[1])] != null
                                  ? allPhoneNumbers[int.Parse(request[1])] + "\n"
                                  : new string("not found" + "\n"));
                    break;

                case 'd':
                    allPhoneNumbers[int.Parse(request[1])] = null;
                    break;
            }
        }

        Console.WriteLine(output.ToString());
    }
}

