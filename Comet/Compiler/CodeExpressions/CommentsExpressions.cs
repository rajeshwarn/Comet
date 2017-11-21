namespace Comet.Compiler.CodeExpressions
{
    #region Namespace

    using System.CodeDom;

    #endregion

    public class CommentsExpressions
    {
        #region Events

        /// <summary>Create a flat summary tag.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="CodeCommentStatement" />
        /// </returns>
        public static CodeCommentStatement CreateFlatSummaryTag(string text)
        {
            return new CodeCommentStatement("<summary>" + text + "</summary>", true);
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

        #endregion
    }
}