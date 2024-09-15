using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhonePadApp
{
    class Program
    {
        public static string OldPhonePad(string input)
        {
            // Mapping of digits to letters on the old phone keypad
            string[] keypad = {
            "",      // 0 (no letters)
            "",      // 1 (no letters)
            "ABC",   // 2
            "DEF",   // 3
            "GHI",   // 4
            "JKL",   // 5
            "MNO",   // 6
            "PQRS",  // 7
            "TUV",   // 8
            "WXYZ"   // 9
        };

            StringBuilder output = new StringBuilder();  // To store the result string
            int currentPressCount = 0;  // Track how many times the current digit has been pressed
            char currentDigit = '\0';   // Track the current digit being pressed

            foreach (char c in input)
            {
                if (c == '#')
                {
                    break;  // End of input
                }
                else if (c == '*')
                {
                    // Process the current sequence before deleting the last character
                    if (currentDigit != '\0')
                    {
                        int digit = currentDigit - '0';
                        string letters = keypad[digit];
                        if (letters.Length > 0)
                        {
                            int index = (currentPressCount - 1) % letters.Length;
                            output.Append(letters[index]);
                        }
                        currentPressCount = 0;  // Reset after processing the sequence
                        currentDigit = '\0';
                    }

                    // Remove last character in output if any
                    if (output.Length > 0)
                    {
                        output.Remove(output.Length - 1, 1);
                    }
                }
                else if (c == ' ')
                {
                    // Process current digit sequence before resetting
                    if (currentDigit != '\0')
                    {
                        int digit = currentDigit - '0';
                        string letters = keypad[digit];
                        if (letters.Length > 0)
                        {
                            int index = (currentPressCount - 1) % letters.Length;
                            output.Append(letters[index]);
                        }
                    }
                    // Reset for next sequence
                    currentPressCount = 0;
                    currentDigit = '\0';
                }
                else if (char.IsDigit(c))
                {
                    if (currentDigit == c)
                    {
                        currentPressCount++;  // Same digit is being pressed again, increment count
                    }
                    else
                    {
                        // Process the previous digit sequence
                        if (currentDigit != '\0')
                        {
                            int digit = currentDigit - '0';
                            string letters = keypad[digit];
                            if (letters.Length > 0)
                            {
                                int index = (currentPressCount - 1) % letters.Length;
                                output.Append(letters[index]);
                            }
                        }
                        // Start new sequence
                        currentPressCount = 1;
                        currentDigit = c;
                    }
                }
            }

            // Process the last sequence (if there's no space or * before the end of input)
            if (currentDigit != '\0')
            {
                int digit = currentDigit - '0';
                string letters = keypad[digit];
                if (letters.Length > 0)
                {
                    int index = (currentPressCount - 1) % letters.Length;
                    output.Append(letters[index]);
                }
            }

            return output.ToString();  // Return the final result string
        }


        public static void Main(string[] args)
            {
                
                Console.WriteLine("OldPhonePad\n");
                Console.WriteLine("2:ABC\n3:DEF\n4:GHI\n5:JKL\n6:MNO\n7:PQRS\n8:TUV\n9:WXYZ\n\n");

                // Original Test cases

                Console.WriteLine("Original Test cases\n");
                Console.WriteLine("Input string: (\"33#\")  Output: " + OldPhonePad("33#"));                 // Output: E
                Console.WriteLine("Input string: (\"227*#\")  Output: " + OldPhonePad("227*#"));               // Output: B
                Console.WriteLine("Input string: (\"4433555 555666#\")  Output: " + OldPhonePad("4433555 555666#"));     // Output: HELLO
                Console.WriteLine("Input string: (\"8 88777444666*664#\")  Output: " + OldPhonePad("8 88777444666*664#"));  // Output: TURING

                // Additional Edge cases
                Console.WriteLine("\nAdditional Edge cases\n");

                Console.WriteLine("Input string: (\"#\")  Output: " + OldPhonePad("#"));                   // Output: "" (Empty input)
                Console.WriteLine("Input string: (\"   #\")  Output: " + OldPhonePad("   #"));                // Output: "" (Input with only spaces)
                Console.WriteLine("Input string: (\"223*7*#\")  Output: " + OldPhonePad("223*7*#"));             // Output: "B" (Multiple consecutive '*')
                Console.WriteLine("Input string: (\"22***#\")  Output: " + OldPhonePad("22***#"));              // Output: "" (Excessive '*' deletes all)
                Console.WriteLine("Input string: (\"2*#\")  Output: " + OldPhonePad("2*#"));                 // Output: "" (Single character deleted)
                Console.WriteLine("Input string: (\"555*5#\")  Output: " + OldPhonePad("555*5#"));              // Output: J (First 'L', deleted, then 'J')
                Console.WriteLine("Input string: (\"44 55*#\")  Output: " + OldPhonePad("44 55*#"));             // Output: H (First 'H', then 'K', deleted 'K')
                Console.WriteLine("Input string: (\"22223333#\")  Output: " + OldPhonePad("22223333#"));           // Output: AD (No spaces between digits)
                Console.WriteLine("Input string: (\"443*#\")  Output: " + OldPhonePad("443*#"));               // Output: H (First 'H', deleted 'I')
                Console.WriteLine("Input string: (\"7777 666 555#\")  Output: " + OldPhonePad("7777 666 555#"));       // Output: SOL (Multiple digit groups with spaces)
                Console.WriteLine("Input string: (\"7777777#\")  Output: " + OldPhonePad("7777777#"));            // Output: R (Max characters for a single key)


                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();  // Wait for user input to close

        }
    }
}
