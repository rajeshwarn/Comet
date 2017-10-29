namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Comet.Structure;

    #endregion

    internal class ConsoleManager
    {
        #region Variables

        private static readonly string CommandNamespace = "Comet.Commands";

        #endregion

        #region Events

        /// <summary>Creates text and returns the bool response.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The response.</returns>
        public static bool CommandBoolResponse(string text)
        {
            ReadInput();
            Console.Write(text);
            string input = Console.ReadLine()?.ToLower();
            return (input == "y") || (input == "yes");
        }

        /// <summary>Creates a console command instance.</summary>
        /// <param name="command">The console command.</param>
        public static void CreateCommandInstance(string command)
        {
            try
            {
                ConsoleCommand _command = new ConsoleCommand(command);
                string _result = InvokeCommandMethod(_command);
                WriteOutput(_result);
            }
            catch (Exception e)
            {
                ExceptionManager.WriteException(e.Message);
            }
        }

        /// <summary>Initializes the console manager libraries.</summary>
        public static void Initialize()
        {
            // Any static classes containing commands for use from the console are located in the Commands namespace.
            // Load references to each type in that namespace via reflection.
            _commandLibraries = new Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>>();

            // Use reflection to load all of the classes in the Commands namespace.
            var _commandTypes = from _fileClassType in Assembly.GetExecutingAssembly().GetTypes() where _fileClassType.IsClass && (_fileClassType.Namespace == CommandNamespace) select _fileClassType;
            var _commandClasses = _commandTypes.ToList();

            foreach (Type _commandClass in _commandClasses)
            {
                // Load the method info from each class into a dictionary.
                var _methods = _commandClass.GetMethods(BindingFlags.Static | BindingFlags.Public);
                var _methodDictionary = new Dictionary<string, IEnumerable<ParameterInfo>>();
                foreach (MethodInfo _method in _methods)
                {
                    string _commandName = _method.Name;
                    _methodDictionary.Add(_commandName, _method.GetParameters());
                }

                // Add the dictionary of methods for the current class into a dictionary of command classes.
                _commandLibraries.Add(_commandClass.Name, _methodDictionary);
            }
        }

        /// <summary>The console reads the input prompt command.</summary>
        /// <param name="promptCommand">The input prompt command.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ReadInput(string promptCommand = "")
        {
            Console.ForegroundColor = Settings.InputColor;
            Console.Write(Settings.InputCharacter + " ");
            Console.ForegroundColor = Settings.TextColor;
            Console.Write(promptCommand);
            return Console.ReadLine();
        }

        /// <summary>The console writes the output message.</summary>
        /// <param name="message">The output message.</param>
        public static void WriteOutput(string message = "")
        {
            if (message.Length <= 0)
            {
                return;
            }

            Console.ForegroundColor = Settings.OutputColor;
            Console.Write(Settings.ErrorCharacter + " ");
            Console.ForegroundColor = Settings.ErrorTextColor;
            Console.WriteLine(message);
        }

        private static Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>> _commandLibraries;

        /// <summary>Coerce the argument.</summary>
        /// <param name="type">The required type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="string" />.</returns>
        private static object CoerceArgument(Type type, string value)
        {
            TypeCode requiredTypeCode = Type.GetTypeCode(type);
            string exceptionMessage = $"Cannot coerce the input argument {value} to required type {type.Name}";
            object result = null;

            switch (requiredTypeCode)
            {
                case TypeCode.String:
                    {
                        result = value;
                        break;
                    }

                case TypeCode.Int16:
                    {
                        short number16;
                        if (short.TryParse(value, out number16))
                        {
                            result = number16;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Int32:
                    {
                        int number32;
                        if (int.TryParse(value, out number32))
                        {
                            result = number32;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Int64:
                    {
                        long number64;
                        if (long.TryParse(value, out number64))
                        {
                            result = number64;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Boolean:
                    {
                        bool trueFalse;
                        if (bool.TryParse(value, out trueFalse))
                        {
                            result = trueFalse;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Byte:
                    {
                        byte byteValue;
                        if (byte.TryParse(value, out byteValue))
                        {
                            result = byteValue;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Char:
                    {
                        char charValue;
                        if (char.TryParse(value, out charValue))
                        {
                            result = charValue;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.DateTime:
                    {
                        DateTime dateValue;
                        if (DateTime.TryParse(value, out dateValue))
                        {
                            result = dateValue;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Decimal:
                    {
                        decimal decimalValue;
                        if (decimal.TryParse(value, out decimalValue))
                        {
                            result = decimalValue;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Double:
                    {
                        double doubleValue;
                        if (double.TryParse(value, out doubleValue))
                        {
                            result = doubleValue;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Single:
                    {
                        float singleValue;
                        if (float.TryParse(value, out singleValue))
                        {
                            result = singleValue;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.UInt16:
                    {
                        ushort uInt16Value;
                        if (ushort.TryParse(value, out uInt16Value))
                        {
                            result = uInt16Value;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.UInt32:
                    {
                        uint uInt32Value;
                        if (uint.TryParse(value, out uInt32Value))
                        {
                            result = uInt32Value;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.UInt64:
                    {
                        ulong uInt64Value;
                        if (ulong.TryParse(value, out uInt64Value))
                        {
                            result = uInt64Value;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Empty:
                    {
                        break;
                    }

                case TypeCode.SByte:
                    {
                        sbyte sByteValue;
                        if (sbyte.TryParse(value, out sByteValue))
                        {
                            result = sByteValue;
                        }
                        else
                        {
                            throw new ArgumentException(exceptionMessage);
                        }

                        break;
                    }

                case TypeCode.Object:
                    break;
                case TypeCode.DBNull:
                    break;
                default:
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
            }
            return result;
        }

        /// <summary>Invokes the command method.</summary>
        /// <param name="command">The console command.</param>
        /// <returns>The <see cref="string" />.</returns>
        private static string InvokeCommandMethod(ConsoleCommand command)
        {
            Console.Write(Environment.NewLine);

            // Validate the class name and command name:
            // Validate the command name:
            if (!_commandLibraries.ContainsKey(command.LibraryClassName))
            {
                ExceptionManager.CommandNotRecognized(command);
                return string.Empty;
            }

            var methodDictionary = _commandLibraries[command.LibraryClassName];
            if (!methodDictionary.ContainsKey(command.Name))
            {
                ExceptionManager.CommandNotRecognized(command);
                return string.Empty;
            }

            // Make sure the correct number of required arguments are provided:
            var methodParameterValueList = new List<object>();
            IEnumerable<ParameterInfo> paramInfoList = methodDictionary[command.Name].ToList();

            // Validate proper # of required arguments provided. Some may be optional:
            var requiredParams = paramInfoList.Where(p => p.IsOptional == false);
            var optionalParams = paramInfoList.Where(p => p.IsOptional);
            int requiredCount = requiredParams.Count();
            int optionalCount = optionalParams.Count();
            int providedCount = command.Arguments.Count();
            if (requiredCount > providedCount)
            {
                ExceptionManager.WriteException(string.Format("Missing required argument. {0} required, {1} optional, {2} provided", requiredCount, optionalCount, providedCount));
                Console.Write(Environment.NewLine);
                return string.Empty;
            }

            // Make sure all arguments are coerced to the proper type, and that there is a 
            // value for every method parameter. The InvokeMember method fails if the number 
            // of arguments provided does not match the number of parameters in the 
            // method signature, even if some are optional:
            if (paramInfoList.Any())
            {
                // Populate the list with default values:
                methodParameterValueList.AddRange(paramInfoList.Select(param => param.DefaultValue));

                // Now walk through all the arguments passed from the console and assign 
                // accordingly. Any optional arguments not provided have already been set to 
                // the default specified by the method signature:
                for (var i = 0; i < command.Arguments.Count(); i++)
                {
                    ParameterInfo _parameterInfo = paramInfoList.ElementAt(i);
                    Type _parameterType = _parameterInfo.ParameterType;
                    try
                    {
                        // Coming from the Console, all of our arguments are passed in as 
                        // strings. Coerce to the type to match the method parameter:
                        object value = CoerceArgument(_parameterType, command.Arguments.ElementAt(i));
                        methodParameterValueList.RemoveAt(i);
                        methodParameterValueList.Insert(i, value);
                    }
                    catch (ArgumentException)
                    {
                        string argumentName = _parameterInfo.Name;
                        string argumentTypeName = _parameterType.Name;
                        string message = string.Format(string.Empty + "The value passed for argument '{0}' cannot be parsed to type '{1}'", argumentName, argumentTypeName);
                        throw new ArgumentException(message);
                    }
                }
            }

            // Set up to invoke the method using reflection:
            Assembly current = typeof(Program).Assembly;

            // Need the full Namespace for this:
            Type _commandLibraryClass = current.GetType(CommandNamespace + "." + command.LibraryClassName);
            object[] inputArgs = null;
            if (methodParameterValueList.Count > 0)
            {
                inputArgs = methodParameterValueList.ToArray();
            }

            Type typeInfo = _commandLibraryClass;

            // This will throw if the number of arguments provided does not match the number 
            // required by the method signature, even if some are optional:
            try
            {
                object result = typeInfo.InvokeMember(command.Name, BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, inputArgs);
                return result.ToString();
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        #endregion
    }
}