using System;
using System.Collections.Generic;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsHead { get; set; }

    public Student(string name, int age, bool isHead = false)
    {
        Name = name;
        Age = age;
        IsHead = isHead;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Head: {(IsHead ? "Yes" : "No")}");
    }
}

class Group
{
    protected List<Student> students = new List<Student>();
    protected string groupName;

    public Group(string name)
    {
        groupName = name;
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Group: {groupName}");
        Console.WriteLine("List of students:");
        foreach (var student in students)
        {
            student.DisplayInfo();
        }
    }

    public List<Student> GetStudents()
    {
        return students;
    }

    public string GetName()
    {
        return groupName;
    }

    public bool HasHead()
    {
        foreach (var student in students)
        {
            if (student.IsHead)
            {
                return true;
            }
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        Group group1 = new Group("Group 1");
        group1.AddStudent(new Student("John", 20));
        group1.AddStudent(new Student("Mary", 21));
        group1.AddStudent(new Student("Peter", 22, true));

        Group group2 = new Group("Group 2");
        group2.AddStudent(new Student("Anna", 19));
        group2.AddStudent(new Student("Alex", 20));
        group2.AddStudent(new Student("Helen", 23, true));

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Select mode:");
            Console.WriteLine("1. Display information about groups");
            Console.WriteLine("2. Edit information about groups");
            Console.WriteLine("3. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayGroupsInfo(group1, group2);
                    break;
                case "2":
                    EditGroupsInfo(group1, group2);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select again.");
                    break;
            }
        }
    }

    static void DisplayGroupsInfo(params Group[] groups)
    {
        Console.WriteLine("Select group to display:");
        for (int i = 0; i < groups.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {groups[i].GetName()}");
        }

        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > groups.Length)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        groups[choice - 1].DisplayInfo();
    }

    static void EditGroupsInfo(params Group[] groups)
    {
        Console.WriteLine("Select group to edit:");
        for (int i = 0; i < groups.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {groups[i].GetName()}");
        }

        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > groups.Length)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        EditGroupInfo(groups[choice - 1]);
    }

    static void EditGroupInfo(Group group)
    {
        Console.WriteLine($"Editing information about {group.GetName()}");
        group.DisplayInfo();

        Console.WriteLine("Select student index to edit:");
        List<Student> students = group.GetStudents();
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {students[i].Name}");
        }

        int studentIndex;
        if (!int.TryParse(Console.ReadLine(), out studentIndex) || studentIndex < 1 || studentIndex > students.Count)
        {
            Console.WriteLine("Invalid student index.");
            return;
        }

        Console.WriteLine($"Enter new name for student {students[studentIndex - 1].Name}:");
        string newName = Console.ReadLine();

        Console.WriteLine($"Enter new age for student {students[studentIndex - 1].Name}:");
        int newAge;
        if (!int.TryParse(Console.ReadLine(), out newAge))
        {
            Console.WriteLine("Invalid age.");
            return;
        }

        Console.WriteLine($"Is student {students[studentIndex - 1].Name} a head? (Y/N)");
        bool isHead = Console.ReadLine().ToUpper() == "Y";

        if (isHead && group.HasHead() && !students[studentIndex - 1].IsHead)
        {
            Console.WriteLine("This group already has a head student. You cannot add another one.");
            return;
        }

        students[studentIndex - 1].Name = newName;
        students[studentIndex - 1].Age = newAge;
        students[studentIndex - 1].IsHead = isHead;

        Console.WriteLine("Student information updated successfully.");
    }
}