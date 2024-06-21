using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfunLang.CfunLang
{
    public class Token
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public Token(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            if (Value == "")
            {
                return $"Token(Type: {Type})";
            }
            else
            {
                return $"Token(Type: {Type}, Value: {Value})";
            }
        }
    }
}
