using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Person
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public long MobileNo { get; set; }
    public string Sex { get; set; }
    public string Mail { get; set; }
    public string CitizenNo { get; set; }
}

public class Program
{
    static string filePath = "phonebook.dat";

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Magenta;
        Console.Clear();
        Start();
    }

    static void Start()
    {
        Menu();
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("\t\t********** WELCOME TO PHONEBOOK *************");
        Console.WriteLine("\n\n\t\t\t MENU \t\t\n\n");
        Console.WriteLine("\t1. Add New \t2. List \t3. Exit \n\t4. Modify \t5. Search \t6. Delete");

        switch (Console.ReadLine())
        {
            case "1":
                AddRecord();
                break;
            case "2":
                ListRecord();
                break;
            case "3":
                Environment.Exit(0);
                break;
            case "4":
                ModifyRecord();
                break;
            case "5":
                SearchRecord();
                break;
            case "6":
                DeleteRecord();
                break;
            default:
                Console.WriteLine("\nEnter 1 to 6 only");
                Console.WriteLine("\nEnter any key to continue...");
                Console.ReadKey();
                Menu();
                break;
        }
    }

    static void AddRecord()
    {
        Console.Clear();
        List<Person> people = LoadRecords();

        Person p = new Person();
        Console.Write("Enter name: ");
        p.Name = Console.ReadLine();
        Console.Write("Enter the address: ");
        p.Address = Console.ReadLine();
        Console.Write("Enter father name: ");
        p.FatherName = Console.ReadLine();
        Console.Write("Enter mother name: ");
        p.MotherName = Console.ReadLine();
        Console.Write("Enter phone no.: ");
        p.MobileNo = long.Parse(Console.ReadLine());
        Console.Write("Enter sex: ");
        p.Sex = Console.ReadLine();
        Console.Write("Enter e-mail: ");
        p.Mail = Console.ReadLine();
        Console.Write("Enter citizen no: ");
        p.CitizenNo = Console.ReadLine();

        people.Add(p);
        SaveRecords(people);

        Console.WriteLine("\nRecord saved.");
        Console.WriteLine("\nEnter any key to return to menu...");
        Console.ReadKey();
        Menu();
    }

    static void ListRecord()
    {
        Console.Clear();
        List<Person> people = LoadRecords();

        foreach (var p in people)
        {
            Console.WriteLine($"\n\nYOUR RECORD IS\n\n");
            Console.WriteLine($"Name: {p.Name}\nAddress: {p.Address}\nFather name: {p.FatherName}\nMother name: {p.MotherName}\nMobile no: {p.MobileNo}\nSex: {p.Sex}\nE-mail: {p.Mail}\nCitizen no: {p.CitizenNo}");
            Console.WriteLine("\nEnter any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine("\nEnter any key to return to menu...");
        Console.ReadKey();
        Menu();
    }

    static void SearchRecord()
    {
        Console.Clear();
        List<Person> people = LoadRecords();
        Console.WriteLine("Enter name of person to search:");
        string name = Console.ReadLine();

        var found = false;
        foreach (var p in people)
        {
            if (p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\nDetail Information About {name}");
                Console.WriteLine($"Name: {p.Name}\nAddress: {p.Address}\nFather name: {p.FatherName}\nMother name: {p.MotherName}\nMobile no: {p.MobileNo}\nSex: {p.Sex}\nE-mail: {p.Mail}\nCitizen no: {p.CitizenNo}");
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Record not found.");
        }

        Console.WriteLine("\nEnter any key to return to menu...");
        Console.ReadKey();
        Menu();
    }

    static void DeleteRecord()
    {
        Console.Clear();
        List<Person> people = LoadRecords();
        Console.WriteLine("Enter name of person to delete:");
        string name = Console.ReadLine();

        var found = false;
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                people.RemoveAt(i);
                found = true;
                break;
            }
        }

        if (found)
        {
            SaveRecords(people);
            Console.WriteLine("Record deleted successfully.");
        }
        else
        {
            Console.WriteLine("No contact's record to delete.");
        }

        Console.WriteLine("\nEnter any key to return to menu...");
        Console.ReadKey();
        Menu();
    }

    static void ModifyRecord()
    {
        Console.Clear();
        List<Person> people = LoadRecords();
        Console.WriteLine("Enter name of person to modify:");
        string name = Console.ReadLine();

        var found = false;
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Person p = people[i];
                Console.Write("Enter new name: ");
                p.Name = Console.ReadLine();
                Console.Write("Enter new address: ");
                p.Address = Console.ReadLine();
                Console.Write("Enter new father name: ");
                p.FatherName = Console.ReadLine();
                Console.Write("Enter new mother name: ");
                p.MotherName = Console.ReadLine();
                Console.Write("Enter new phone no.: ");
                p.MobileNo = long.Parse(Console.ReadLine());
                Console.Write("Enter new sex: ");
                p.Sex = Console.ReadLine();
                Console.Write("Enter new e-mail: ");
                p.Mail = Console.ReadLine();
                Console.Write("Enter new citizen no: ");
                p.CitizenNo = Console.ReadLine();

                people[i] = p;
                found = true;
                break;
            }
        }

        if (found)
        {
            SaveRecords(people);
            Console.WriteLine("Record modified successfully.");
        }
        else
        {
            Console.WriteLine("Record not found.");
        }

        Console.WriteLine("\nEnter any key to return to menu...");
        Console.ReadKey();
        Menu();
    }

    static List<Person> LoadRecords()
    {
        if (!File.Exists(filePath))
        {
            return new List<Person>();
        }

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (List<Person>)formatter.Deserialize(fs);
        }
    }

    static void SaveRecords(List<Person> people)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, people);
        }
    }
}
