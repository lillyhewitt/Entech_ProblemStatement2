using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InputValidations
{
    public class VariableValidation
    {
        public string ReadPrompt(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        // reads a integer value from the console
        public int ReadInt(string prompt)
        {
            string input = ReadPrompt(prompt);
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid long value entered.");
            }
        }

        // reads a long value from the console
        public long ReadLong(string prompt)
        {
            string input = ReadPrompt(prompt);
            if (long.TryParse(input, out long result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid long value entered.");
            }
        }

        // reads a DateTime value from the console
        public DateTime? ReadDateTime(string prompt)
        {
            string input = ReadPrompt(prompt);

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid DateTime value entered.");
            }
        }

        // reads a MetricTypes value from the console
        public MetricTypes ReadMetric(string prompt)
        {
            string input = ReadPrompt(prompt);
            if (Enum.TryParse(input, out MetricTypes metric))
            {
                return metric;
            }
            else
            {
                throw new ArgumentException("Invalid metric type entered.");
            }
        }

        // reads a StatusTypes value from the console
        public StatusTypes ReadStatus(string prompt)
        {
            string input = ReadPrompt(prompt);
            if (Enum.TryParse(input, out StatusTypes status))
            {
                return status;
            }
            else
            {
                throw new ArgumentException("Invalid metric type entered.");
            }
        }

        // reads a decimal value from the console
        public decimal? ReadDecimal(string prompt)
        {
            string input = ReadPrompt(prompt);

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (decimal.TryParse(input, out decimal result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid decimal value entered.");
            }
        }
    }
}