namespace Comet.Compiler
{
    #region Namespace

    using System;
    using System.CodeDom;
    using System.Collections.Generic;

    #endregion

    public class MethodsHelper
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

        /// <summary>Create a method.</summary>
        /// <param name="name">The method name.</param>
        /// <param name="memberAttributes">The member Attributes.</param>
        /// <param name="returnType">The return Type.</param>
        /// <param name="customAttributes">The custom Attributes.</param>
        /// <param name="codeMemberMethods">The code Member MethodsHelper.</param>
        /// <param name="codeExpression">The code Expression.</param>
        /// <returns><see cref="CodeConstructor"/></returns>
        public static CodeMemberMethod MethodConstructor(string name, MemberAttributes memberAttributes, Type returnType, List<CodeStatement> customAttributes = null, List<CodeStatement> codeMemberMethods = null, List<CodeExpression> codeExpression = null)
        {
            CodeMemberMethod _methodConstructor = new CodeMemberMethod
            {
                    Attributes = memberAttributes
                };

            _methodConstructor.Name = name;
            _methodConstructor.ReturnType = new CodeTypeReference(returnType);

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

        #endregion
    }
}