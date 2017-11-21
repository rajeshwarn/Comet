namespace Comet.Compiler.CodeExpressions
{
    #region Namespace

    using System.CodeDom;

    #endregion

    public class CodeAssignments
    {
        #region Events

        /// <summary>Set title property code.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="CodeAssignStatement" />
        /// </returns>
        public static CodeAssignStatement SetTitle(string text)
        {
            return new CodeAssignStatement(new CodeVariableReferenceExpression("System.Console.Title"), new CodePrimitiveExpression(text));
        }

        #endregion
    }
}