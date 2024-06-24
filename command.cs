using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfunLang.CfunLang
{
    public class Command
    {
        public List<Token> tokens;
        public Command() { 
        tokens = new List<Token>();
        }

        public void AddToken(Token token)
        {
            tokens.Add(token);
        }
    }
}
