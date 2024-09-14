using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhonePadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OldPhonePad("4433555 555666#"));  // Should return "HELLO"
            Console.WriteLine(OldPhonePad("33#"));              // Should return "E"
            Console.WriteLine(OldPhonePad("227*#"));            // Should return "B"
            Console.WriteLine(OldPhonePad("8 88777444666*664#"));// Should return ????
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();  // Waits for user input before closing
        }

        public static string OldPhonePad(string input)
        {
            Dictionary<char, string> keypad = new Dictionary<char, string>()
            {
                { '1', "&'(" },
                { '2', "ABC" },
                { '3', "DEF" },
                { '4', "GHI" },
                { '5', "JKL" },
                { '6', "MNO" },
                { '7', "PQRS" },
                { '8', "TUV" },
                { '9', "WXYZ" },
                { '0', " " } // Space for '0'
            };

            StringBuilder output = new StringBuilder();
            int count = 0;
            char lastChar = '\0';

            foreach (char c in input)
            {
                if (c == '#') // End of input
                {
                    break;
                }
                else if (c == '*') // Backspace
                {
                    if (output.Length > 0)
                    {
                        output.Remove(output.Length - 1, 1);
                    }
                }
                else if (c == ' ') // Pause between letters from the same button
                {
                    lastChar = '\0'; // Reset lastChar
                }
                else if (keypad.ContainsKey(c)) // Handle digits
                {
                    if (c == lastChar)
                    {
                        count++; // Cycle through letters for the same digit
                    }
                    else
                    {
                        lastChar = c;
                        count = 0;
                    }

                    string letters = keypad[c];
                    output.Append(letters[count % letters.Length]);
                }
            }

            return output.ToString();
        }
    }
}
