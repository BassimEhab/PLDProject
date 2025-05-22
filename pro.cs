
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF        =  0, // (EOF)
        SYMBOL_ERROR      =  1, // (Error)
        SYMBOL_WHITESPACE =  2, // (Whitespace)
        SYMBOL_MINUS      =  3, // '-'
        SYMBOL_MINUSMINUS =  4, // --
        SYMBOL_EXCLAMEQ   =  5, // '!='
        SYMBOL_DOLLAR     =  6, // '$'
        SYMBOL_LPARAN     =  7, // '('
        SYMBOL_RPARAN     =  8, // ')'
        SYMBOL_TIMES      =  9, // '*'
        SYMBOL_TIMESTIMES = 10, // '**'
        SYMBOL_COMMA      = 11, // ','
        SYMBOL_DIV        = 12, // '/'
        SYMBOL_LBRACE     = 13, // '{'
        SYMBOL_RBRACE     = 14, // '}'
        SYMBOL_PLUS       = 15, // '+'
        SYMBOL_PLUSPLUS   = 16, // '++'
        SYMBOL_LT         = 17, // '<'
        SYMBOL_EQ         = 18, // '='
        SYMBOL_EQEQ       = 19, // '=='
        SYMBOL_GT         = 20, // '>'
        SYMBOL_COMMENT    = 21, // Comment
        SYMBOL_DIGIT      = 22, // Digit
        SYMBOL_DOUBLE     = 23, // double
        SYMBOL_ELSE       = 24, // else
        SYMBOL_FLOAT      = 25, // float
        SYMBOL_FOR        = 26, // for
        SYMBOL_ID         = 27, // Id
        SYMBOL_IF         = 28, // if
        SYMBOL_INT        = 29, // int
        SYMBOL_PRINT      = 30, // print
        SYMBOL_STRING     = 31, // string
        SYMBOL_WHILE      = 32, // while
        SYMBOL_ASSIGN     = 33, // <assign>
        SYMBOL_COMMENT2   = 34, // <comment>
        SYMBOL_CONCEPT    = 35, // <concept>
        SYMBOL_COND       = 36, // <cond>
        SYMBOL_DATA       = 37, // <data>
        SYMBOL_DIGIT2     = 38, // <digit>
        SYMBOL_EXP        = 39, // <exp>
        SYMBOL_EXPR       = 40, // <expr>
        SYMBOL_FACTOR     = 41, // <factor>
        SYMBOL_FOR_STMT   = 42, // <for_stmt>
        SYMBOL_ID2        = 43, // <id>
        SYMBOL_IF_STMT    = 44, // <if_stmt>
        SYMBOL_OP         = 45, // <op>
        SYMBOL_PRINT_STMT = 46, // <print_stmt>
        SYMBOL_PROGRAM    = 47, // <program>
        SYMBOL_STEP       = 48, // <step>
        SYMBOL_STMT_LIST  = 49, // <stmt_list>
        SYMBOL_TERM       = 50, // <term>
        SYMBOL_WHILE_STMT = 51  // <while_stmt>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_DOLLAR_DOLLAR                                     =  0, // <program> ::= '$' <stmt_list> '$'
        RULE_STMT_LIST                                                 =  1, // <stmt_list> ::= <concept>
        RULE_STMT_LIST2                                                =  2, // <stmt_list> ::= <concept> <stmt_list>
        RULE_CONCEPT                                                   =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                                  =  4, // <concept> ::= <if_stmt>
        RULE_CONCEPT3                                                  =  5, // <concept> ::= <for_stmt>
        RULE_CONCEPT4                                                  =  6, // <concept> ::= <while_stmt>
        RULE_CONCEPT5                                                  =  7, // <concept> ::= <comment>
        RULE_CONCEPT6                                                  =  8, // <concept> ::= <print_stmt>
        RULE_COMMENT_COMMENT                                           =  9, // <comment> ::= Comment
        RULE_ASSIGN_EQ                                                 = 10, // <assign> ::= <id> '=' <expr>
        RULE_ID_ID                                                     = 11, // <id> ::= Id
        RULE_EXPR_PLUS                                                 = 12, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                                = 13, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                                      = 14, // <expr> ::= <term>
        RULE_TERM_TIMES                                                = 15, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                                  = 16, // <term> ::= <term> '/' <factor>
        RULE_TERM                                                      = 17, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                         = 18, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                                    = 19, // <factor> ::= <exp>
        RULE_EXP_LPARAN_RPARAN                                         = 20, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                       = 21, // <exp> ::= <id>
        RULE_EXP2                                                      = 22, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                               = 23, // <digit> ::= Digit
        RULE_PRINT_STMT_PRINT_LPARAN_RPARAN                            = 24, // <print_stmt> ::= print '(' <expr> ')'
        RULE_IF_STMT_IF_LPARAN_RPARAN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE = 25, // <if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}' else '{' <stmt_list> '}'
        RULE_IF_STMT_IF_LPARAN_RPARAN_LBRACE_RBRACE                    = 26, // <if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}'
        RULE_IF_STMT_IF_LPARAN_RPARAN_LBRACE_RBRACE_ELSE               = 27, // <if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}' else <if_stmt>
        RULE_COND                                                      = 28, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                                     = 29, // <op> ::= '<'
        RULE_OP_GT                                                     = 30, // <op> ::= '>'
        RULE_OP_EQEQ                                                   = 31, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                               = 32, // <op> ::= '!='
        RULE_FOR_STMT_FOR_LPARAN_COMMA_COMMA_RPARAN_LBRACE_RBRACE      = 33, // <for_stmt> ::= for '(' <data> <assign> ',' <cond> ',' <step> ')' '{' <stmt_list> '}'
        RULE_WHILE_STMT_WHILE_LPARAN_RPARAN_LBRACE_RBRACE              = 34, // <while_stmt> ::= while '(' <cond> ')' '{' <stmt_list> '}'
        RULE_DATA_INT                                                  = 35, // <data> ::= int
        RULE_DATA_DOUBLE                                               = 36, // <data> ::= double
        RULE_DATA_FLOAT                                                = 37, // <data> ::= float
        RULE_DATA_STRING                                               = 38, // <data> ::= string
        RULE_STEP_MINUSMINUS                                           = 39, // <step> ::= -- <id>
        RULE_STEP_MINUSMINUS2                                          = 40, // <step> ::= <id> --
        RULE_STEP_PLUSPLUS                                             = 41, // <step> ::= <id> '++'
        RULE_STEP_PLUSPLUS2                                            = 42, // <step> ::= '++' <id>
        RULE_STEP                                                      = 43  // <step> ::= <assign>
    };

    public class MyParser
    {
        private LALRParser parser;

        public MyParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //(Whitespace)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //--
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOLLAR :
                //'$'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPARAN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPARAN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENT :
                //Comment
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRINT :
                //print
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENT2 :
                //<comment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRINT_STMT :
                //<print_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE_STMT :
                //<while_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_DOLLAR_DOLLAR :
                //<program> ::= '$' <stmt_list> '$'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <concept> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT4 :
                //<concept> ::= <while_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT5 :
                //<concept> ::= <comment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT6 :
                //<concept> ::= <print_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMMENT_COMMENT :
                //<comment> ::= Comment
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ :
                //<assign> ::= <id> '=' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPARAN_RPARAN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRINT_STMT_PRINT_LPARAN_RPARAN :
                //<print_stmt> ::= print '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPARAN_RPARAN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE :
                //<if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}' else '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPARAN_RPARAN_LBRACE_RBRACE :
                //<if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPARAN_RPARAN_LBRACE_RBRACE_ELSE :
                //<if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}' else <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LPARAN_COMMA_COMMA_RPARAN_LBRACE_RBRACE :
                //<for_stmt> ::= for '(' <data> <assign> ',' <cond> ',' <step> ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_STMT_WHILE_LPARAN_RPARAN_LBRACE_RBRACE :
                //<while_stmt> ::= while '(' <cond> ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_INT :
                //<data> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DOUBLE :
                //<data> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_FLOAT :
                //<data> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_STRING :
                //<data> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= -- <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> --
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            //todo: Report message to UI?
        }

    }
}
