namespace Comet.Compiler
{
    #region Namespace

    using System;
    using System.CodeDom;
    using System.Collections.Generic;

    using Comet.Compiler.CodeExpressions;

    #endregion

    public class CodeGeneration
    {
        #region Events

        /// <summary>Creates a auto property with getters and setters that wrap the specified field.</summary>
        /// <param name="field">The field to get and set.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="type">The type of the property.</param>
        /// <returns>
        ///     <see cref="CodeMemberProperty" />
        /// </returns>
        public static CodeMemberProperty CreateAutoProperty(string field, string name, Type type)
        {
            CodeMemberProperty _autoProperty = new CodeMemberProperty
                {
                    Name = name,
                    Type = new CodeTypeReference(type),
                    Attributes = MemberAttributes.Public
                };

            _autoProperty.SetStatements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(null, field),
                new CodePropertySetValueReferenceExpression()));

            _autoProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(null, field)));

            return _autoProperty;
        }

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
            _namespace.Imports.Add(new CodeNamespaceImport("Comet"));

            _codeUnit.Namespaces.Add(_namespace);

            // Class
            CodeTypeDeclaration _class = new CodeTypeDeclaration("CometInstall");
            _class.Attributes = MemberAttributes.Public;
            _class.Comments.Add(CommentsExpressions.CreateFlatSummaryTag("The main class."));

            // if (false)
            // {
            // _class.BaseTypes.Add(new CodeTypeReference("Base"));
            // }

            // Create entry point method
            CodeEntryPointMethod _entryPointMethod = EntryPoints.MainEntryPointMethod();
            _entryPointMethod.Statements.Add(MethodInvoke.InvokeMethod(ReferenceTypes.InstallerType(), "Initialize"));
            _class.Members.Add(_entryPointMethod);

            // Create public variables
            _class.Members.Add(CreatePublicProperty("InstallPath", typeof(string)));
            _class.Members.Add(CreatePublicProperty("DownloadLink", typeof(string)));
            var _codeStatements = new List<CodeStatement>
                {
                    CodeAssignments.SetTitle("Comet")
                };

            var _codeExpression = new List<CodeExpression>
                {
                    MethodInvoke.WriteLineExpression("Comet Console"),
                    MethodInvoke.ReadLineExpression()
                };

            CodeMemberMethod _initializeMethod = MethodsHelper.MethodConstructor("Initialize", MemberAttributes.Static, typeof(void), null, _codeStatements, _codeExpression);
            _initializeMethod.Comments.Add(CommentsExpressions.CreateFlatSummaryTag("The initialize method."));
            _initializeMethod.Statements.Add(new CodeCommentStatement("TODO: Insert download and installer logic."));
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
            CodeTypeDeclaration _class = new CodeTypeDeclaration("CometInstall");
            _class.Attributes = MemberAttributes.Public;
            _class.Comments.Add(CommentsExpressions.CreateFlatSummaryTag("The main class."));

            // Create entry point method
            _class.Members.Add(EntryPoints.MainEntryPointMethod());

            _namespace.Types.Add(_class);
            return _codeUnit;
        }

        /// <summary>Create a public property.</summary>
        /// <param name="name">The property name.</param>
        /// <param name="type">The property type.</param>
        /// <returns>
        ///     <see cref="CodeSnippetTypeMember" />
        /// </returns>
        public static CodeSnippetTypeMember CreatePublicProperty(string name, Type type)
        {
            CodeSnippetTypeMember _codeSnippetTypeMember = new CodeSnippetTypeMember
                {
                    Text = "public " + type + " " + name + " { get; set; }"
                };

            _codeSnippetTypeMember.Comments.Add(new CodeCommentStatement("The " + name + " property.", false));
            return _codeSnippetTypeMember;
        }

        #endregion
    }
}