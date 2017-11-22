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

        /// <summary>Adds the method statements to the method.</summary>
        /// <param name="codeMemberMethods">The code Member MethodsHelper.</param>
        /// <param name="codeExpression">The code Expression.</param>
        /// <param name="codeMemberMethod">The code member method.</param>
        public static void AddMethodStatements(IReadOnlyCollection<CodeStatement> codeMemberMethods, IReadOnlyCollection<CodeExpression> codeExpression, CodeMemberMethod codeMemberMethod)
        {
            if (codeMemberMethods != null)
            {
                foreach (CodeStatement _codeMethod in codeMemberMethods)
                {
                    codeMemberMethod.Statements.Add(_codeMethod);
                }
            }

            if (codeExpression != null)
            {
                foreach (CodeExpression _codeExpression in codeExpression)
                {
                    codeMemberMethod.Statements.Add(_codeExpression);
                }
            }
        }

        /// <summary>Create a class constructor.</summary>
        /// <param name="codeMemberMethods">The code Member MethodsHelper.</param>
        /// <param name="codeExpression">The code Expression.</param>
        /// <returns>
        ///     <see cref="CodeConstructor" />
        /// </returns>
        public static CodeConstructor ClassConstructor(List<CodeStatement> codeMemberMethods = null, List<CodeExpression> codeExpression = null)
        {
            CodeConstructor _classConstructor = new CodeConstructor
                {
                    Attributes = MemberAttributes.Public
                };

            AddMethodStatements(codeMemberMethods, codeExpression, _classConstructor);

            return _classConstructor;
        }

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

        /// <summary>Create a code member field.</summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="nameSpace">The namespace.</param>
        /// <param name="fieldName">The field name.</param>
        /// <returns>
        ///     <see cref="CodeMemberField" />
        /// </returns>
        public static CodeMemberField CreateField(string name, Type type, string nameSpace, string fieldName)
        {
            CodeMemberField _codeMemberField = new CodeMemberField();
            _codeMemberField.Name = name;
            _codeMemberField.Type = new CodeTypeReference(type);
            _codeMemberField.Attributes = MemberAttributes.Private | MemberAttributes.Static;
            _codeMemberField.InitExpression = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(nameSpace), fieldName);

            return _codeMemberField;
        }

        /// <summary>Create a flat summary tag.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="CodeCommentStatement" />
        /// </returns>
        public static CodeCommentStatement CreateFlatSummaryTag(string text)
        {
            return new CodeCommentStatement("<summary>" + text + "</summary>", true);
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
                    Text = "public static " + type + " " + name + " { get; set; }"
                };

            // _codeSnippetTypeMember.Comments.Add(new CodeCommentStatement("The " + name + " property.", false));
            return _codeSnippetTypeMember;
        }

        /// <summary>Creates a summary tag comment collection statement.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="CodeCommentStatementCollection" />
        /// </returns>
        public static CodeCommentStatementCollection CreateSummaryTag(string text)
        {
            CodeCommentStatementCollection _codeCommentStatementCollection = new CodeCommentStatementCollection
                {
                    new CodeCommentStatement("<summary>", true),
                    new CodeCommentStatement(text, true),
                    new CodeCommentStatement("</summary>", true)
                };

            return _codeCommentStatementCollection;
        }

        /// <summary>Method: Execute comet method.</summary>
        /// <param name="method">The method.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression ExecuteCometMethod(string method)
        {
            return new CodeMethodInvokeExpression(ReferenceLibrary.CometType(), method);
        }

        /// <summary>Get the install directory path.</summary>
        /// <param name="path">The full file path.</param>
        /// <returns>
        ///     <see cref="CodeAssignStatement" />
        /// </returns>
        public static CodeAssignStatement GetInstallDirectory(string path)
        {
            return new CodeAssignStatement(new CodeVariableReferenceExpression("System.IO.Path"), new CodeVariableReferenceExpression(path));
        }

        /// <summary>Invoke the method expression.</summary>
        /// <param name="nameSpace">The namespace.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression InvokeMethod(CodeTypeReferenceExpression nameSpace, string methodName)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(nameSpace, methodName));
        }

        /// <summary>Invoke the method expression.</summary>
        /// <param name="nameSpace">The namespace.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression InvokeMethod(string nameSpace, string methodName)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(nameSpace), methodName));
        }

        /// <summary>Creates the code entry point method.</summary>
        /// <param name="codeMemberMethods">The code Member MethodsHelper.</param>
        /// <param name="codeExpression">The code Expression.</param>
        /// <returns>
        ///     <see cref="CodeEntryPointMethod" />
        /// </returns>
        public static CodeEntryPointMethod MainEntryPointMethod(List<CodeStatement> codeMemberMethods = null, List<CodeExpression> codeExpression = null)
        {
            CodeEntryPointMethod _codeEntryPointMethod = new CodeEntryPointMethod
                {
                    ReturnType = new CodeTypeReference(typeof(void)),
                    Attributes = MemberAttributes.Private & MemberAttributes.Static,
                    Name = "Main"
                };

            _codeEntryPointMethod.CustomAttributes.Add(new CodeAttributeDeclaration("STAThread"));
            _codeEntryPointMethod.Comments.Add(CreateFlatSummaryTag("The main entry point for the application."));

            AddMethodStatements(codeMemberMethods, codeExpression, _codeEntryPointMethod);

            return _codeEntryPointMethod;
        }

        /// <summary>Create a method.</summary>
        /// <param name="name">The method name.</param>
        /// <param name="memberAttributes">The member Attributes.</param>
        /// <param name="returnType">The return Type.</param>
        /// <param name="customAttributes">The custom Attributes.</param>
        /// <param name="codeMemberMethods">The code Member MethodsHelper.</param>
        /// <param name="codeExpression">The code Expression.</param>
        /// <returns>
        ///     <see cref="CodeConstructor" />
        /// </returns>
        public static CodeMemberMethod MethodConstructor(string name, MemberAttributes memberAttributes, Type returnType, List<CodeStatement> customAttributes = null, List<CodeStatement> codeMemberMethods = null, List<CodeExpression> codeExpression = null)
        {
            CodeMemberMethod _methodConstructor = new CodeMemberMethod
                {
                    Attributes = memberAttributes,
                    Name = name,
                    ReturnType = new CodeTypeReference(returnType)
                };

            // Add custom attributes.
            if (customAttributes != null)
            {
                foreach (CodeStatement _codeMethod in customAttributes)
                {
                    _methodConstructor.Statements.Add(_codeMethod);
                }
            }

            AddMethodStatements(codeMemberMethods, codeExpression, _methodConstructor);

            return _methodConstructor;
        }

        /// <summary>Method: ReadLine.</summary>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression ReadLineExpression()
        {
            return new CodeMethodInvokeExpression(ReferenceLibrary.SystemConsoleType(), "ReadLine");
        }

        /// <summary>Set the download path.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="CodeAssignStatement" />
        /// </returns>
        public static CodeAssignStatement SetDownloadPath(string text)
        {
            return new CodeAssignStatement(new CodeVariableReferenceExpression("CometInstall.DownloadLink"), new CodePrimitiveExpression(text));
        }

        /// <summary>Set the install path.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="CodeAssignStatement" />
        /// </returns>
        public static CodeAssignStatement SetInstallPath(string text)
        {
            return new CodeAssignStatement(new CodeVariableReferenceExpression("CometInstall.InstallPath"), new CodeVariableReferenceExpression(text));
        }

        /// <summary>Set title property code.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="CodeAssignStatement" />
        /// </returns>
        public static CodeAssignStatement SetTitle(string text)
        {
            return new CodeAssignStatement(new CodeVariableReferenceExpression("System.Console.Title"), new CodePrimitiveExpression(text));
        }

        /// <summary>Get the to string code expression.</summary>
        /// <param name="methodName">The method name.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression ToString(string methodName)
        {
            return new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(methodName), "ToString");
        }

        /// <summary>Method: WriteLine.</summary>
        /// <param name="message">The message to write.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression WriteLineExpression(string message)
        {
            return new CodeMethodInvokeExpression(ReferenceLibrary.SystemConsoleType(), "WriteLine", new CodePrimitiveExpression(message));
        }

        #endregion
    }
}