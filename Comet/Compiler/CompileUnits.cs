namespace Comet.Compiler
{
    #region Namespace

    using System.CodeDom;

    using Comet.Compiler.CodeExpressions;

    #endregion

    public class CompileUnits
    {
        #region Events

        /// <summary>Create a installer code unit to compile.</summary>
        /// <returns>
        ///     <see cref="CodeCompileUnit" />
        /// </returns>
        public static CodeCompileUnit CreateInstallerCode()
        {
            CodeCompileUnit _codeUnit = new CodeCompileUnit();

            // References
            _codeUnit.ReferencedAssemblies.Add("System.dll");
            _codeUnit.ReferencedAssemblies.Add("Comet.dll");

            // Namespace
            CodeNamespace _namespace = new CodeNamespace("Installer");

            // Using/s
            _namespace.Imports.Add(new CodeNamespaceImport("System"));
            _namespace.Imports.Add(new CodeNamespaceImport("System.Diagnostics"));
            _namespace.Imports.Add(new CodeNamespaceImport("Comet"));

            _codeUnit.Namespaces.Add(_namespace);

            // Class
            CodeTypeDeclaration _class = new CodeTypeDeclaration("CometInstall");
            _class.Attributes = MemberAttributes.Public;
            _class.Comments.Add(CodeGeneration.CreateFlatSummaryTag("The main class."));

            // if (false)
            // {
            // _class.BaseTypes.Add(new CodeTypeReference("Base"));
            // }

            // Create entry point method
            CodeEntryPointMethod _entryPointMethod = CodeGeneration.MainEntryPointMethod();
            _entryPointMethod.Statements.Add(CodeGeneration.InvokeMethod(ReferenceLibrary.InstallerType(), "Initialize"));
            _class.Members.Add(_entryPointMethod);

            // Create public variables
            _class.Members.Add(CodeGeneration.CreatePublicProperty("InstallPath", typeof(string)));
            _class.Members.Add(CodeGeneration.CreatePublicProperty("DownloadLink", typeof(string)));

            CodeMemberMethod _initializeMethod = InitializeMethod();
            _class.Members.Add(_initializeMethod);

            _namespace.Types.Add(_class);

            return _codeUnit;
        }

        /// <summary>Create a main entry point code unit to compile.</summary>
        /// <returns>
        ///     <see cref="CodeCompileUnit" />
        /// </returns>
        public static CodeCompileUnit CreateMainEntryPoint()
        {
            CodeCompileUnit _codeUnit = new CodeCompileUnit();

            // References
            _codeUnit.ReferencedAssemblies.Add("System.dll");
            _codeUnit.ReferencedAssemblies.Add("Comet.dll");

            // Namespace
            CodeNamespace _namespace = new CodeNamespace("Installer");

            // Using/s
            _namespace.Imports.Add(new CodeNamespaceImport("System"));
            _namespace.Imports.Add(new CodeNamespaceImport("Comet"));

            _codeUnit.Namespaces.Add(_namespace);

            // Class
            CodeTypeDeclaration _class = new CodeTypeDeclaration("CometInstall")
                {
                    Attributes = MemberAttributes.Public
                };
            _class.Comments.Add(CodeGeneration.CreateFlatSummaryTag("The main class."));

            // Create entry point method
            _class.Members.Add(CodeGeneration.MainEntryPointMethod());

            _namespace.Types.Add(_class);
            return _codeUnit;
        }

        private static CodeMemberMethod InitializeMethod()
        {
            CodeMemberMethod _initializeMethod = new CodeMemberMethod();
            _initializeMethod.Comments.Add(CodeGeneration.CreateFlatSummaryTag("The initialize method."));

            // Variables
            CodeMethodInvokeExpression _stringDownloadLink = CodeGeneration.ToString("DownloadLink");
            CodeMethodInvokeExpression _stringInstallPath = CodeGeneration.ToString("InstallPath");

            // CodeMethodInvokeExpression _stringInstallDirectory = ToString("");
            CodeMethodInvokeExpression _writeDownloadLink = new CodeMethodInvokeExpression(ReferenceLibrary.SystemConsoleType(), "WriteLine", _stringDownloadLink);
            CodeMethodInvokeExpression _writeInstallPath = new CodeMethodInvokeExpression(ReferenceLibrary.SystemConsoleType(), "WriteLine", _stringInstallPath);

            // CodeMethodInvokeExpression _writeInstallDirectory = new CodeMethodInvokeExpression(ReferenceLibrary.SystemConsoleType(), "WriteLine", _stringInstallPath);
            _initializeMethod.Name = "Initialize";
            _initializeMethod.Attributes = MemberAttributes.Static;
            _initializeMethod.ReturnType = new CodeTypeReference(typeof(void));

            _initializeMethod.Statements.Add(CodeGeneration.SetTitle("Comet"));
            _initializeMethod.Statements.Add(CodeGeneration.WriteLineExpression("Comet Console Debug"));

            // Initialize variables
            _initializeMethod.Statements.Add(CodeGeneration.SetDownloadPath("https://raw.githubusercontent.com/DarkByte7/Comet/master/Comet/Update.package"));
            _initializeMethod.Statements.Add(CodeGeneration.SetInstallPath("Comet.Managers.ApplicationManager.GetMainModuleFileName()"));

            // Output
            _initializeMethod.Statements.Add(CodeGeneration.WriteLineExpression("Download link:"));
            _initializeMethod.Statements.Add(_writeDownloadLink);
            _initializeMethod.Statements.Add(CodeGeneration.WriteLineExpression("Install path:"));
            _initializeMethod.Statements.Add(_writeInstallPath);

            // _initializeMethod.Statements.Add(MethodInvoke.WriteLineExpression("Install directory:"));

            // ReadLine
            _initializeMethod.Statements.Add(CodeGeneration.ReadLineExpression());

            return _initializeMethod;
        }

        #endregion
    }
}