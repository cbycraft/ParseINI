using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    internal class LexerStateScanCharacter : ILexerState
    {
        public void StateChange(LexerStateHandle stateContext)
        {
            switch (stateContext.CurrentCharacter)
            {
                case ';':
                    if ( stateContext.CurrentCharacter == 0)
                    {
                        stateContext.CurrentState = new LexerStateComment();
                    }
                    else
                    {
                        stateContext.WordBuffer = stateContext.WordBuffer + stateContext.CurrentCharacter.ToString();
                        stateContext.CurrentState = new LexerStateBuildString();
                    }
                    break;
                case '[':
                    stateContext.TokenLine.Add(new LexerTokenNode(LexerTokenNode.Token.OpenSquareBrace, "[", stateContext.LineNumber, stateContext.CharacterNumber));
                    stateContext.CurrentState = new LexerStateScanCharacter();
                    break;
                case '=':
                    stateContext.TokenLine.Add(new LexerTokenNode(LexerTokenNode.Token.EqualSign, "=", stateContext.LineNumber, stateContext.CharacterNumber));
                    stateContext.CurrentState = new LexerStateScanCharacter();
                    break;
                case ']':
                    stateContext.TokenLine.Add(new LexerTokenNode(LexerTokenNode.Token.CloseSquareBrace, "]", stateContext.LineNumber, stateContext.CharacterNumber));
                    stateContext.CurrentState = new LexerStateScanCharacter();
                    break;
                case '\r':
                    stateContext.CurrentState = new LexerStateScanCharacter();
                    break;
                case '\n':
                    stateContext.TokenLine.Add(new LexerTokenNode(LexerTokenNode.Token.EndOfLine, System.Environment.NewLine, stateContext.LineNumber, stateContext.CharacterNumber));
                    stateContext.CharacterNumber = 0;
                    stateContext.LineNumber = stateContext.LineNumber + 1;
                    stateContext.CurrentState = new LexerStateScanCharacter();
                    break;
                case ' ':
                    stateContext.CurrentState = new LexerStateScanCharacter();
                    break;
                case '\t':
                    stateContext.CurrentState = new LexerStateScanCharacter();
                    break;
                default:
                    stateContext.WordBuffer = stateContext.WordBuffer + stateContext.CurrentCharacter.ToString();
                    stateContext.CurrentState = new LexerStateBuildString();
                    break;
            }
        }
    }
}
