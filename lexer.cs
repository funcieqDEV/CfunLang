using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CfunLang.CfunLang
{
    public class Lexer
    {
        private List<Token> tokens;
        public string code;

        public Lexer()
        {
            this.code = string.Empty;
            tokens = new List<Token>();
        }

        private readonly string TT_int = "int";
        private readonly string TT_float = "float";
        private readonly string TT_operator = "operator";
        private readonly string TT_lparen = "lparen";
        private readonly string TT_rparen = "rparen";
        private readonly string TT_keyword = "keyword";
        private readonly string TT_semi = "semi";
        private readonly string TT_string = "string";
        private readonly string TT_equal = "equal";
        private readonly string TT_lbrace = "lbrace";
        private readonly string TT_rbrace = "rbrace";
        private readonly string TT_fn = "fn";
        private readonly string TT_var_int = "int_type";
        private readonly string TT_var_float = "float_type";
        private readonly string TT_var_string = "string_type";
        private readonly string TT_var_bool = "bool_type";
        private readonly string TT_identifier = "identifier";
        private readonly string TT_greater = "greater";
        private readonly string TT_less = "less";
        private readonly string TT_true = "true";
        private readonly string TT_false = "false";

        private bool IsDigit(char c) => c >= '0' && c <= '9';

        private bool IsOperator(char c) => c == '+' || c == '-' || c == '*' || c == '/';

        public List<Token> Tokenize()
        {
            tokens.Clear();
            string str_num = string.Empty;
            string word = string.Empty;
            int i = 0;

            while (i < code.Length)
            {
                if (IsDigit(code[i]))
                {
                    while (i < code.Length && (IsDigit(code[i]) || code[i] == '.'))
                    {
                        str_num += code[i];
                        i++;
                    }
                    if (str_num.Contains('.'))
                    {
                        tokens.Add(new Token(TT_float, str_num));
                    }
                    else
                    {
                        tokens.Add(new Token(TT_int, str_num));
                    }
                    str_num = string.Empty;
                }
                else if (IsOperator(code[i]))
                {
                    tokens.Add(new Token(TT_operator, code[i].ToString()));
                    i++;
                }
                else if (code[i] == '(')
                {
                    tokens.Add(new Token(TT_lparen, string.Empty));
                    i++;
                }
                else if (code[i] == '{')
                {
                    tokens.Add(new Token(TT_lbrace, string.Empty));
                    i++;
                }
                else if (code[i] == '}')
                {
                    tokens.Add(new Token(TT_rbrace, string.Empty));
                    i++;
                }
                else if (code[i] == ';')
                {
                    tokens.Add(new Token(TT_semi, string.Empty));
                    i++;
                }
                else if (code[i] == '"')
                {
                    i++;
                    while (i < code.Length && code[i] != '"')
                    {
                        word += code[i];
                        i++;
                    }
                    tokens.Add(new Token(TT_string, word));
                    word = string.Empty;
                    i++;
                }
                else if (code[i] == ')')
                {
                    tokens.Add(new Token(TT_rparen, string.Empty));
                    i++;
                }
                else if (i + 3 < code.Length && code.Substring(i, 5) == "print" && (code[i + 5] == ' '))
                {
                    tokens.Add(new Token(TT_keyword, "print"));
                    i += 5;
                }
                else if (code[i] == '=')
                {
                    tokens.Add(new Token(TT_equal, string.Empty));
                    i++;
                }
                else if (i + 2 < code.Length && code.Substring(i, 2) == "fn" && (code[i + 2] == ' ' || code[i + 2] == '('))
                {
                    tokens.Add(new Token(TT_fn, "function"));
                    i += 2;
                }
                else if (i + 2 < code.Length && code.Substring(i, 2) == "if" && (code[i + 2] == ' ' || code[i + 2] == '(' || code[i + 2] == '\0'))
                {
                    tokens.Add(new Token(TT_keyword, "if"));
                    i += 2;
                }
                else if (code[i] == '>')
                {
                    tokens.Add(new Token(TT_greater, string.Empty));
                    i++;
                }
                else if (code[i] == '<')
                {
                    tokens.Add(new Token(TT_less, string.Empty));
                    i++;
                }
                else if (i + 3 < code.Length && code.Substring(i, 4) == "else" && (code[i + 4] == ' ' || code[i + 4] == '('))
                {
                    tokens.Add(new Token(TT_keyword, "else"));
                    i += 4;
                }
                else if (i + 3 < code.Length && code.Substring(i, 3) == "int")
                {
                    tokens.Add(new Token(TT_var_int, string.Empty));
                    i += 3;
                }
                else if (i + 5 < code.Length && code.Substring(i, 5) == "float" && (code[i + 5] == ' ' || code[i + 5] == '('))
                {
                    tokens.Add(new Token(TT_var_float, string.Empty));
                    i += 5;
                }
                else if (i + 4 < code.Length && code.Substring(i, 4) == "true" && (code[i + 4] == ' ' || code[i + 4] == '\0' || code[i + 4] == ')' || code[i + 4] == ';'))
                {
                    tokens.Add(new Token(TT_true, string.Empty));
                    i += 4;
                }
                else if (i + 5 < code.Length && code.Substring(i, 5) == "false" && (code[i + 5] == ' ' || code[i + 5] == '\0' || code[i + 5] == ')' || code[i + 5] == ';'))
                {
                    tokens.Add(new Token(TT_false, string.Empty));
                    i += 5;
                }
                else if (i + 6 < code.Length && code.Substring(i, 6) == "string" && (code[i + 6] == ' ' || code[i + 6] == '('))
                {
                    tokens.Add(new Token(TT_var_string, string.Empty));
                    i += 6;
                }
                else if (code[i] == ' ')
                {
                    i++;
                }
                else
                {
                    if (char.IsLetter(code[i]))
                    {
                        string identifier = string.Empty;
                        while (i < code.Length && char.IsLetterOrDigit(code[i]))
                        {
                            identifier += code[i];
                            i++;
                        }
                        if (identifier == "int")
                        {
                            tokens.Add(new Token(TT_var_int, string.Empty));
                        }
                        else if (identifier == "float")
                        {
                            tokens.Add(new Token(TT_var_float, string.Empty));
                        }
                        else if (identifier == "string")
                        {
                            tokens.Add(new Token(TT_var_string, string.Empty));
                        }
                        else if (identifier == "fn")
                        {
                            tokens.Add(new Token(TT_fn, "function"));
                        }
                        else if (identifier == "bool")
                        {
                            tokens.Add(new Token(TT_var_bool, string.Empty));
                        }
                        else
                        {
                            tokens.Add(new Token(TT_identifier, identifier));
                        }
                    }
                    else
                    {
                        tokens.Add(new Token("unknown", code[i].ToString()));
                        i++;
                    }
                }
            }
            return tokens;
        }

        public void DisplayTokens()
        {
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}
