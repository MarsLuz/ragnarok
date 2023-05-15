using System;

class Program
{
static void Main()
{
Console.WriteLine("Enter the number of cities in the model:");
int numCities = int.Parse(Console.ReadLine());
string[] cityNames = new string[numCities];
    int[] contactNumbers = new int[numCities];
    int[] outbreakLevels = new int[numCities];

    for (int i = 0; i < numCities; i++)
    {
        Console.WriteLine($"Enter the details for City {i}:");
        
        Console.Write("City Name: ");
        cityNames[i] = Console.ReadLine();

        Console.Write("Number of cities in contact: ");
        int numContacts = int.Parse(Console.ReadLine());

        contactNumbers[i] = numContacts;

        Console.WriteLine("Enter the city numbers in contact:");

        for (int j = 0; j < numContacts; j++)
        {
            int contactCity;
            bool isValidCity;

            do
            {
                Console.Write($"City {j+1} in contact: ");
                contactCity = int.Parse(Console.ReadLine());

                isValidCity = (contactCity >= 0 && contactCity < numCities);

                if (!isValidCity)
                    Console.WriteLine("Invalid city number. Please enter again.");
            } while (!isValidCity);
        }

        outbreakLevels[i] = 0;
    }

    Console.WriteLine("City number\tCity name\tCOVID-19 Outbreak Level");
    for (int i = 0; i < numCities; i++)
    {
        Console.WriteLine($"{i}\t\t{cityNames[i]}\t\t{outbreakLevels[i]}");
    }

    while (true)
    {
        Console.WriteLine("\nEnter an event that occurred during the COVID-19 outbreak:");
        string userEvent = Console.ReadLine();

        if (userEvent == "Outbreak" || userEvent == "Vaccinate" || userEvent == "Lockdown")
        {
            Console.Write("Enter the city number where the incident took place: ");
            int cityNumber = int.Parse(Console.ReadLine());

            if (cityNumber >= 0 && cityNumber < numCities)
            {
                if (userEvent == "Outbreak")
                {
                    outbreakLevels[cityNumber] += 2;

                    // Increase outbreak level for adjacent cities
                    for (int j = 0; j < numCities; j++)
                    {
                        if (contactNumbers[j] > 0 && Array.IndexOf(contactNumbers, cityNumber) >= 0)
                        {
                            outbreakLevels[j] += 1;
                            outbreakLevels[j] = Math.Min(outbreakLevels[j], 3); // Ensure outbreak level doesn't exceed 3
                        }
                    }
                }
                else if (userEvent == "Vaccinate")
                {
                    outbreakLevels[cityNumber] = 0;
                }
            }
            else
            {
                Console.WriteLine("Invalid city number. Please try again.");
            }
        }
        else if (userEvent == "Spread")
        {
            for (int i = 0; i < numCities; i++)
            {
                if (outbreakLevels[i] > 0)
                {
                    for (int j = 0; j < numCities; j++)
                    {
                        if (contactNumbers[j] > 0 && outbreakLevels[j] > outbreakLevels[i])
                        {
                            outbreakLevels[i] += 1;
                            break;
                        }
                    }
                }
            }
        }
        else if (userEvent == "Exit")
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid event. Please try again.");
        }
    }
    Console.WriteLine("\nFinal COVID-19 Outbreak Levels:");
        Console.WriteLine("City number\tCity name\tCOVID-19 Outbreak Level");
        for (int i = 0; i < numCities; i++)
        {
            Console.WriteLine($"{i}\t\t{cityNames[i]}\t\t{outbreakLevels[i]}");
        }
    }
}