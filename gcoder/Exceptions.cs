using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcoder
{
    class FileLoadFailureException : Exception
    {
        public FileLoadFailureException()
        {
        }
        public FileLoadFailureException(string message) : base(message)
        {
        }
    }
    class UnknownCodeException : Exception
    {
        public UnknownCodeException()
        {

        }

        public UnknownCodeException(int line)
        {
            
        }
    }
    class InvalidParamException : Exception
    {
        public InvalidParamException()
        {

        }

        public InvalidParamException(int line) : base("Parameter error! Check line: " + line)
        {
        }

    }

    class EmptyFileException : Exception
    {
        public new string Message { get; set; }
        public EmptyFileException(string path)
        {
            Message = "Could not find any G-Codes in file " + path;
        }

    }

    class TooBigFileException : Exception
    {
        public new string Message { get; set; }

        public TooBigFileException()
        {
            Message = "The file is too big.";
        }
    }
}
