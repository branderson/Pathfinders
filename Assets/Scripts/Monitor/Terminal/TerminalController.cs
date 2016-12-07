using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Monitor.Terminal
{
    public class TerminalController : MonoBehaviour
    {
        public const char AsciiBold = (char)29;
        public const char AsciiItalic = (char)30;
        public const char AsciiPage = (char)28;
        public const char AsciiCancelText = (char)'`';
        public const char AsciiIgnoreString = (char)31;
        public const char AsciiToggleFontSize = '~';
        public const char AsciiPause = (char)26;

//        [SerializeField] private MainMenuController _menuController;
        [SerializeField] private TerminalScreenController _screen;
        [SerializeField] private AudioClip _keyStroke;
        [SerializeField] private AudioClip _return;
        [SerializeField] private AudioClip _space;
        [SerializeField] private AudioClip _backspace;
        [SerializeField] private AudioClip _newPaper;
        [SerializeField] private string _promptString = "$ >";

        private Text _text;
        private AudioSource _audio;
        private FontStyle _fontStyle;
        private string _typeString;         // String which is being typed to the terminal
        private bool _acceptInput;          // Is the typewriter accepting player input
        private bool _inputDisabled;        // Is the typewriter allowed to accept player input
        private bool _ignoreString;         // Should the typewriter attempt to decipher lines
        private int _waitTimer;             // Frames that must elapse before another character can be handled
        private int _allowedBackspaces;     // Number of characters which may be backspaced
        private int _lineChars;             // Number of characters typed this line
        private int _lines;                 // Number of lines typed this page

        // Special decipher states
        private int _knockKnock;

        public void Start()
        {
            _audio = GetComponent<AudioSource>();
            _text = _screen.Text;
            _typeString = null;
            _acceptInput = true;
            _inputDisabled = false;
            _ignoreString = false;
            _allowedBackspaces = 0;
            _fontStyle = _text.fontStyle;

            _knockKnock = 0;
            _text.text = _promptString;
        }

        public void Update()
        {
            // If autotyping, autotype
            if (_typeString != null)
            {
                if (_waitTimer-- <= 0)
                {
                    // Ready to handle character
                    if (_typeString.Length == 0)
                    {
                        // End of string
                        _typeString = null;
                        _ignoreString = false;
                        if (!_inputDisabled)
                        {
                            _acceptInput = true;
                        }
                    }
                    else
                    {
                        // Type character to typewriter
                        HandleCharacter(_typeString[0]);
                        _typeString = _typeString.Remove(0, 1);
                    }
                }
            }
            // Handle user input
            if (_acceptInput && Input.anyKeyDown)
            {
                if (Input.inputString.Length <= 0) return;
                // This throws out LF from CRLF
                char c = Input.inputString[0];
                // Commands called before being read in input string
                if (c == AsciiCancelText)
                {
                    _typeString = null;
                    _ignoreString = false;
                    if (!_inputDisabled)
                    {
                        _acceptInput = true;
                    }
                }
                else
                {
                    _typeString += c;
                    //                        if (c == '\b') { } // Prevent checking char info for backspace
                    //                        // Catch carriage return from keyboard (The CR in CRLF in Windows
                    //                        else if (c == '\n' || c == 13 || 
                    //                            _screen.GetLineWidth(_lineChars) + _screen.GetTextWidth(_typeString) >= _maxLineWidth)
                    //                        {
                    //    //                        PreventInputWhileAutotyping();
                    //                        }
                }
            }
            else if (Input.anyKeyDown)
            {
                if (Input.inputString.Length <= 0) return;
                // Commands that ignore _acceptInput
                char c = Input.inputString[0];
                if (c == AsciiCancelText)
                {
                    _typeString = null;
                    _ignoreString = false;
                    if (!_inputDisabled)
                    {
                        _acceptInput = true;
                    }
                }
            }
        }

        /// <summary>
        /// Type out the given string on the typewriter
        /// </summary>
        /// <remarks>
        /// Disables input while typing, preserving previous input restrictions after finished
        /// </remarks>
        /// <param name="s">
        /// String to type
        /// </param>
        /// <param name="clearString">
        /// Whether to clear the contents of the type queue or append to the end of it
        /// </param>
        public void TypeString(string s, bool clearString=true)
        {
            // TODO: Check if string will fit on page, new page if not
            // Go to new line if we need to
            if (_allowedBackspaces > 0)
            {
                HandleCharacter('\n');
            }
            if (!_inputDisabled)
            {
                PreventInputWhileAutotyping();
            }
            // Do not try to decipher string
            if (clearString)
            {
                _typeString = s;
            }
            else
            {
                _typeString += s;
            }
            _ignoreString = true;
        }

        public void HandleCharacter(char c)
        {
            // TODO: Implement these or get rid of them
            if (c == AsciiBold)
            {
                // Toggle bold
                switch (_fontStyle)
                {
                    case FontStyle.Normal:
                        break;
                    case FontStyle.Bold:
                        break;
                    case FontStyle.Italic:
                        break;
                    case FontStyle.BoldAndItalic:
                        break;
                }
                _text.fontStyle = _fontStyle;
            }
            else if (c == AsciiItalic)
            {
                // Toggle italic
                switch (_fontStyle)
                {
                    case FontStyle.Normal:
                        break;
                    case FontStyle.Bold:
                        break;
                    case FontStyle.Italic:
                        break;
                    case FontStyle.BoldAndItalic:
                        break;
                }
                _text.fontStyle = _fontStyle;
            }
            else if (c == AsciiPage)
            {
                // New paper
                _lines = 0;
                _text.text = _promptString;
//                _screen.Clear();
            }
            else if (c == AsciiIgnoreString)
            {
                _ignoreString = !_ignoreString;
            }
            else if (c == AsciiToggleFontSize)
            {
                if (_screen.FontMode == FontSizeMode.Normal)
                {
                    _screen.FontMode = FontSizeMode.Small;
                }
                else
                {
                    _screen.FontMode = FontSizeMode.Normal;
                }
            }
            else if (c == AsciiPause)
            {
                // Wait for half a second
                _waitTimer = 30;
            }
            else if (c == '\b')
            {
                if (_text.text.Length > 0 && _allowedBackspaces > 0)
                {
                    _text.text = _text.text.Remove(_text.text.Length - 1);
                    _audio.PlayOneShot(_backspace);
                    _waitTimer = 5;
                    _allowedBackspaces--;
                    _lineChars--;
                }
            }
            // Catch carriage return from keyboard (The CR in CRLF from Windows)
            else if (c == '\n' || c == 13)
            {
                CarriageReturn();
                _text.text += _promptString;
            }
            // Add to current line
            else if (c == ' ')
            {
                if (_screen.GetLineWidth(_lineChars) >= _screen.MaxLineWidth)
                {
                    // Go down a line instead
                    CarriageReturn();
                }
                else
                {
                    _text.text += c;
                    _audio.PlayOneShot(_space);
                    _waitTimer = 2;
                    _allowedBackspaces++;
                    _lineChars++;
                }
            }
            else
            {
                if (_screen.GetLineWidth(_lineChars) >= _screen.MaxLineWidth)
                {
                    if (!_text.text.EndsWith("-") && !_text.text.EndsWith(" "))
                    {
                        // Add a hyphen
                        _text.text += "-";
                        _audio.PlayOneShot(_keyStroke);
                        _waitTimer = 2;
                        _lineChars++;
                    }
                    // Continue on next line
                    CarriageReturn();
                    // Push the character to the front of the queue so it gets read again
                    _typeString = c + _typeString;
                }
                else
                {
                    _text.text += c;
                    _audio.PlayOneShot(_keyStroke);
                    _waitTimer = 2;
                    _allowedBackspaces++;
                    _lineChars++;
                }
            }
        }

        private void CarriageReturn()
        {
            string lineString = _screen.GetLineString(_lineChars);
//            if (_lines > _screen.GetMaxLines())
//            {
//                // New paper
////                _screen.Clear();
//                _lines = 0;
//                _text.text = _promptString;
//            }
//            else
            {
                // New line
                _text.text += '\n';
//                _screen.Clear();
                _waitTimer = 0;
                _lines++;
                _audio.PlayOneShot(_return);
                _lineChars = 0;
                _allowedBackspaces = 0;
            }

            // Don't let the player type anything else until finished
//            PreventInputWhileAutotyping();

            // See if the string meant anything
            if (!_ignoreString)
            {
                DecipherString(lineString);
            }
        }

        private void DecipherString(string str)
        {
            List<string> commands = str.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (commands.Count == 0) return;
            string opcode = commands[0];
            List<string> options = commands.Where(item => item.StartsWith("-")).ToList();
            string target = commands.Last();
            if (commands.IndexOf(target) == 1 || target.StartsWith("-"))
            {
                return;
            }

            switch (opcode)
            {
                case "door":
//                    string pass
                    if (options.Any(item => item == "-o" || item == "--open"))
                    {
                    }
                    if (options.Any(item => item == "-o" || item == "--open"))
                    {
                    }
                    break;
                case "enemy":
                    break;
                default:
                    Debug.Log("Unrecognized command");
                    break;
            }
            // Knock knock jokes
            if (_knockKnock == 0 && str.ToLower() == "knock knock")
            {
                _knockKnock = 1;
                TypeString("Who's there?\n", false);
            }
            else switch (_knockKnock)
            {
                case 1:
                    _knockKnock = 2;
                    TypeString(str.Trim() + " who?\n", false);
                    break;
                case 2:
                    _knockKnock = 0;
                    TypeString("Haha, very funny...\n", false);
                    break;
                default:
                    _knockKnock = 0;
                    break;
            }

            // Limericks
            if (str.ToLower().Contains("limerick"))
            {
                _screen.FontMode = FontSizeMode.Small;
                TypeString("There once was a player whose prime\n" +
                           "Was spent asking their keyboard to rhyme\n" +
                           "It replied with contempt\n" +
                           "That they should attempt\n" +
                           "To stop wasting their game-playing time\n", false);
            }

            if (str.ToLower().Contains("haiku"))
            {
                TypeString("Nah" + AsciiPause + "." + AsciiPause + "." + AsciiPause + "." + AsciiPause +
                    " Not feelin' it\n", false);
            }

            if (str.ToLower() == "load game")
            {
                Debug.Log("Load Game string detected");
            }
            else if (str.ToLower() == "settings")
            {
                Debug.Log("Settings string detected");
            }
        }
//
//        public void LoadPaper()
//        {
//            _audio.PlayOneShot(_newPaper);
//        }
//
//        public void RemovingPaper()
//        {
//            _waitTimer = 180;
//            _lines = 0;
//
//            if (_lineChars > 0)
//            {
//                _audio.PlayOneShot(_return);
//            }
//            _lineChars = 0;
//            _allowedBackspaces = 0;
//            // Don't let the player type anything else until finished
//            PreventInputWhileAutotyping();
//        }

        public void DisableInput()
        {
            _acceptInput = false;
            _inputDisabled = true;
        }

        public void PreventInputWhileAutotyping()
        {
            _acceptInput = false;
        }

        public void EnableInput()
        {
            _inputDisabled = false;
            if (_typeString == null)
            {
                _acceptInput = true;
            }
        }

        public void PreventBackspace()
        {
            _allowedBackspaces = 0;
        }
    }
}