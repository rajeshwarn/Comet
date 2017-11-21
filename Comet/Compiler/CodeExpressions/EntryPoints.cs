namespace Comet.Compiler.CodeExpressions
{
    #region Namespace

    using System.CodeDom;
    using System.Collections.Generic;

    #endregion

    public class EntryPoints
    {
        #region Events

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
            _codeEntryPointMethod.Comments.Add(CommentsExpressions.CreateFlatSummaryTag("The main entry point for the application."));

            MethodsHelper.AddMethodStatements(codeMemberMethods, codeExpression, _codeEntryPointMethod);

            return _codeEntryPointMethod;
        }

        #endregion
    }
}