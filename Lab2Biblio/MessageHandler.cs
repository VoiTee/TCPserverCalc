using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Biblio
{
    public class MessageHandler
    {
        private int first;
        private string sign;
        private int second;

        public string firstMessage { get; set; }
        public string signMessage { get; set; }
        public string secondMessage { get; set; }

        public bool isEndFlag { get; set; }
        public bool isGoodFlag { get; set; }

        public MessageHandler()
        {
            this.first = 0;
            this.sign = "";
            this.second = 1;

            this.firstMessage = "";
            this.signMessage = "";
            this.secondMessage = "";

            this.isEndFlag = false;
            this.isGoodFlag = true;
        }
        public string makeResponse(string message)
        {
            if (message.Length != 0 && message != "q")
            {
                if (this.firstMessage == "")
                {
                    this.isGoodFlag = true;
                    try
                    {
                        this.first = Int32.Parse(message);
                        this.firstMessage = message;

                        Console.WriteLine($"First message ({this.first})parsed succesfully.");
                        return $"Enter a sign:";
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{message}'(first)");
                        this.isGoodFlag = false;
                        return "First number is incorrect. Try again!";
                    }

                }
                else if (this.signMessage == "")
                {
                    this.isGoodFlag = true;
                    try
                    {
                        if (message.Length > 1) throw new FormatException("Zly znak");
                        this.sign = message;
                        this.signMessage = message;

                        Console.WriteLine($"Sign message ({this.sign})parsed succesfully.");
                        return $"Enter a second number:";

                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{message}' (sign)");
                        this.isGoodFlag = false;
                        return "Sign is incorrect. Try again!";
                    }

                }
                else if (this.secondMessage == "")
                {
                    this.isGoodFlag = true;
                    try
                    {
                        this.second = Int32.Parse(message);
                        this.secondMessage = message;
                        Console.WriteLine($"Second message ({this.second})parsed succesfully.");
                        return calculate();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{message}'(second)");
                        this.isGoodFlag = false;
                        return "Second number is incorrect. Try again!";

                    }
                }
                else
                {
                    this.isGoodFlag = true;


                }

                return calculate();
            }
            else return "Zamykanie...";
        }

        public string calculate()
        {
            double result = 0.0;
            switch (this.sign)
            {
                case "+":
                    result = this.first + this.second;
                    break;
                case "-":
                    result = this.first - this.second;
                    break;
                case "*":
                    result = this.first * this.second;
                    break;
                case "/":
                    result = this.first / this.second;
                    break;
            }



            string resMessage = $"Result: {this.first} {this.sign} {this.second} = {result} \r\n\r\n Enter a first number:";
            resetProps();
            return resMessage;
        }

        public void resetProps()
        {
            this.firstMessage = "";
            this.signMessage = "";
            this.secondMessage = "";

            this.first = 0;
            this.sign = "";
            this.second = 0;
        }

    }


}
