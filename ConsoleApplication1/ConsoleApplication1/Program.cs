using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShenanigansQuestionnaire
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!NameAuth())
            {
                Console.Read();
                return;
            }

            if (!QnAs())
            {
                Console.Read();
                return;
            }

            if (!MFA())
            {
                Console.Read();
                return;
            }

            LaunchTheShizz();
        }

        private static void LaunchTheShizz()
        {
            Console.WriteLine("You now have access. Press any key to continue and be mind-blown.");
            Console.Read();
            Process.Start("<ENTER LINK TO LAUNCH>");
        }

        private static bool MFA()
        {
            Console.WriteLine("For security reasons, we require additional information to verify you. (Mhm, you guesssed it - MFA)");

            string answer = GetResponseForPrompt("We've sent a code to your mobile device. Please enter the code to continue.");
            string[] expectedCode = { "<ENTER A CODE HERE THAT YOU WILL TEXT THE USER BEFORE RUNNING THIS>" };
            if (ValidateValue(answer, expectedCode))
                return true;

            Console.WriteLine("401 Unauthorized");
            return false;
        }

        private static bool QnAs()
        {
            Console.WriteLine("We need to authenticate you (ofcourse!). Answer these questions to prove you're the same person/robot who sits by the trash can in 4141.");

            string[] questions = {
                //<ENTER LIST OF QUESTIONS>
            };

            string[] hints = {
                //<ENTER LIST OF HINTS FOR THE QUESTIONS IN THE SAME ORDER AS THE QUESTIONS>
            };

            string[,] expectedAnswersLists = new string[3, 2] { 
                //<EACH ROW IS A SET OF CORRECT ANSWERS FOR THE QUESTIONS. REPALCE THE NEXT THREE LINES WITH THE LIST OF ANSWERS>
                { "", "" },
                { "", "" },
                { "", "" }
            };
            for(int i=0; i<questions.Length; i++)
            {
                if(!QnA(questions[i], hints[i], sliceArray(expectedAnswersLists,i)))
                    return false;
            }
            return true;
        }

        private static bool QnA(string question, string hint, string[] expectedAnswers)
        {
            string answer = GetResponseForPrompt(question);
            for (int i=0; i<3; i++)
            {
                if (ValidateValue(answer, expectedAnswers))
                    return true;
                else if (i == 2)
                    break;
                else
                    answer = GetResponseForPrompt("Try again [Hint:"+hint+"]:");
            }
            Console.WriteLine("401 Unauthorized");
            return false;
        }

        private static string[] sliceArray(string[,] array2d, int row)
        {
            LinkedList<string> result = new LinkedList<string>();
            for (int i = 0; i < 2; i++)
            {
                result.AddFirst(array2d[row, i]);
            }
            return result.ToArray();
        }

        private static bool NameAuth()
        {
            string namePrompt = "Please enter your full name:";
            string[] expectedNames = {
                //<ENTER LIST OF POSSIBLE NAMES THE USER WOULD GO BY>
            };
            string name = GetResponseForPrompt(namePrompt);
            if (ValidateValue(name, expectedNames))
                return true;

            Console.WriteLine("You are an impostor. Leave now, or I'll call 911.");
            return false;
            
        }

        private static bool ValidateValue(string value, string[] expectedValues)
        {
            foreach (string expectedValue in expectedValues)
            {
                if (value.ToLower().Equals(expectedValue.ToLower()))
                    return true;
            }
            return false;
        }

        private static string GetResponseForPrompt(string prompt)
        {
            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            return response;
        }
    }
}
