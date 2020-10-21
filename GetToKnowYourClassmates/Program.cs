using System;

namespace GetToKnowYourClassmates
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initiate data
            string[] names = { "Alfonzo Uno", "Barry Deux", "Charlotte Drei", "David Net", "Edith Cinque", "Fabio Exi", "Gunther Sju" };
            string[] hometowns = { "Seville", "Paris", "Berlin", "Seoul", "Rome", "Athens", "Oslo" };
            string[] favoriteFoods = { "Risotto", "Veal", "Black Forest Cake", "Guknap Bowl", "Rigatoni", "Feta Salad", "Meatballs" };
            string[] majors = { "Culinary Studies", "Computer Science", "English", "French", "Game Design", "Theatre", "History" };
            int studentCount = names.Length;

            // Application Loop
            bool done = false;
            while (!done)
            {
                Console.WriteLine("Welcome to the Classmates Program!");
                MakeLineSpace(1);
                Console.WriteLine("==================================");
                MakeLineSpace(1);

                // Display Classmate Data for User
                Console.WriteLine("Current Class Roster:");
                MakeLineSpace(1);
                for (int i = 0; i < studentCount; i++)
                {
                    Console.WriteLine("{0}. {1}", i, names[i]);
                }
                MakeLineSpace(1);

                // Prompt User to choose a Student to look into
                int targetStudentID = ChooseStudent(names);

                // Prompt User to look into selected Student's hometown, favroite food or major
                bool inputSubjectValid = false;
                while (!inputSubjectValid)
                {
                    Console.Write("What would you like to know about {0}? ", names[targetStudentID]);
                    string inputSubject = PromptForInput("(Enter either 'hometown', 'favorite food' or 'major') ");
                    // Validate Subject input (Must be any of the specified options in the prompt)
                    if ((inputSubject.Trim().ToLower()).Equals("hometown"))
                    {
                        Console.WriteLine("{0}'s hometown is {1}!", names[targetStudentID], hometowns[targetStudentID]);
                        inputSubjectValid = true;
                    }
                    else if ((inputSubject.Trim().ToLower()).Equals("favorite food"))
                    {
                        Console.WriteLine("{0}'s favorite food is {1}!", names[targetStudentID], favoriteFoods[targetStudentID]);
                        inputSubjectValid = true;
                    }
                    else if ((inputSubject.Trim().ToLower()).Equals("major"))
                    {
                        Console.WriteLine("{0}'s major is {1}!", names[targetStudentID], majors[targetStudentID]);
                        inputSubjectValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: Option not recognized. Please choose one of the specified terms above.");
                    }
                    MakeLineSpace(1);
                }
                // Prompt User to Continue
                done = AskToContinue();
            }
        }

        // Has the User select a student by either providing their ID number (array index) or their name.
        // Returns the array index value of a found valid student.
        //
        // Using exceptions, this is an alternative to using TryParse
        //      Providing a non-integer input that causes a FormatException will have the program use the input as a name.
        //          If no matching name is found, declare the value as invalud and prompt the user to try again.
        //      Providing an integer input that is outside the range of the student list causes an IndexOutOfRangeException that will have the program
        //          declare that the value is invalid and prompt the user to try again.
        public static int ChooseStudent(string[] names)
        {
            int studentCount = names.Length;
            int targetStudentID = -1;
            bool inputStuIDValid = false;
            while (!inputStuIDValid)
            {
                string inputStuIDStr = PromptForInput("Please choose a Student by ID Number or Name: ");
                // Validate Student ID/Name input (Must be an integer within the range of student ID values, or a name that matches one of the students')
                int inputStuID = -1;
                try
                {
                    // Initially assume input is an integer
                    inputStuID = int.Parse(inputStuIDStr);
                    Console.WriteLine("Integer Input detected!");
                    try
                    {
                        // Assume integer is in range
                        Console.WriteLine("You have selected Student #{0}: {1}!", inputStuID, names[inputStuID]);
                        targetStudentID = inputStuID;
                        inputStuIDValid = true;
                    }
                    catch (IndexOutOfRangeException) // Input integer out of range
                    {
                        Console.WriteLine("Error: No student with that ID exists. Please enter another number.");
                    }
                }
                catch (FormatException) // Input is non-integer, treat it as a string with a student's name and search for exact match
                {
                    Console.WriteLine("String Input detected!");
                    for (int i = 0; i < studentCount; i++)
                    {
                        if (inputStuIDStr.Equals(names[i]))
                        {
                            Console.WriteLine("You have selected Student #{0}: {1}!", i, names[i]);
                            targetStudentID = i;
                            inputStuIDValid = true;
                        }
                    }
                    if (!inputStuIDValid) // No match is found within the list of names
                    {
                        Console.WriteLine("Error: No Student with the given string as a name was found in the system. Please try again.");
                    }
                }
                MakeLineSpace(1);
            }
            return targetStudentID;
        }

        // Prompts user for an input, with the message parameter serving as context. Returns the string generated by the user's input.
        public static string PromptForInput(string message)
        {
            Console.Write(message);
            string userInput = Console.ReadLine();
            return userInput;
        }

        // Prompts user if they want to continue using the program. 
        // If yes, then let the loop iterate. Otherwise, stop the loop by setting done to true.
        public static bool AskToContinue()
        {
            while (true)
            {
                string inputStr = PromptForInput("Would you like to know more? ('yes'/'no') ").Trim();
                if (inputStr.Equals("yes"))
                {
                    Console.Clear();
                    return false;
                } 
                else if (inputStr.Equals("no"))
                {
                    Console.WriteLine($"Thank you for using the Classmate Program! Have a nice day!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: Please input 'yes' or 'no'.");
                }
            }
        }

        // Adds empty lines in console for formatting
        public static void MakeLineSpace(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine(" ");
            }
        }

        // Legacy Code: This is just an old version of ChooseStudent that used TryParse instead of Try-Catch statements with Exceptions
        public static int LegacyChooseStudent(string[] names)
        {
            int studentCount = names.Length;
            int targetStudentID = -1;
            bool inputStuIDValid = false;
            while (!inputStuIDValid)
            {
                string inputStuIDStr = PromptForInput("Please choose a Student by ID Number or Name: ");
                // Validate Student ID input (Must be an integer within the range of student ID values)
                int inputStuID = -1;
                if (int.TryParse(inputStuIDStr, out inputStuID))
                {
                    Console.WriteLine("Integer Input detected!");
                    if (inputStuID >= 0 && inputStuID < studentCount)
                    {
                        Console.WriteLine("You have selected Student #{0}: {1}!", inputStuID, names[inputStuID]);
                        targetStudentID = inputStuID;
                        inputStuIDValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: No student with that ID exists. Please enter another number.");
                    }
                }
                else
                {
                    Console.WriteLine("String Input detected!");
                    for (int i = 0; i < studentCount; i++)
                    {
                        if (inputStuIDStr.Equals(names[i]))
                        {
                            Console.WriteLine("You have selected Student #{0}: {1}!", i, names[i]);
                            targetStudentID = i;
                            inputStuIDValid = true;
                        }
                    }
                    if (!inputStuIDValid)
                    {
                        Console.WriteLine("Error: No Student with the given string as a name was found in the system. Please try again.");
                    }
                }
                MakeLineSpace(1);
            }
            return targetStudentID;
        }
    }
}
