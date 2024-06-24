using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfunLang.CfunLang.funcionality;


/*Error codes 
1 - Error with slicing tokens to command
 
*/

namespace CfunLang.CfunLang
{
    public class Interpreter
    {
        private bool GuiMode;  
        private string code;
        private List<Token> tokens;
        public List<Command> commands;
        public Interpreter(string code, bool GuiMode) { 
            this.code = code;
            this.GuiMode = GuiMode;
            commands = new List<Command>();
        }

        public void Execute()
        {
            Lexer lexer = new Lexer();
            lexer.code = code;
            tokens = lexer.Tokenize();
            Slice();

            foreach(Command command in commands)
            {
                if (command.tokens[0].Type == Lexer.TT_keyword && command.tokens[0].Value == "print" && command.tokens[1].Type == Lexer.TT_string )
                {
                    if (GuiMode)
                    {

                    }
                    else
                    {
                       Print.print(command.tokens[1].Value);
                    }
                    
                }
            }
          

        }

        private void Slice()
        {
            Command command = new Command();
            foreach (Token token in tokens)
            {
                if(token.Type != Lexer.TT_semi)
                {
                    command.tokens.Add(token);
                }
                else
                {
                    try
                    {
                        this.commands.Add(command);
                    }
                    catch(Exception e)
                    {
                        Print.print("Error with value: 1");
                    }
                }
            }
        }
    }
}
