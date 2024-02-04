namespace Rhymba.Services.Search
{
    using System.Linq.Expressions;

    internal class SearchFilterExpressionVisitor : ExpressionVisitor
    {
        private string expression;

        internal SearchFilterExpressionVisitor()
        {
            this.expression = string.Empty;
        }

        public string GetExpression()
        {
            return this.expression;
        }

        public override Expression? Visit(Expression? node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    this.expression += "(";
                    base.Visit(node.Left);
                    this.expression += ") and (";
                    base.Visit(node.Right);
                    this.expression += ")";
                    break;
                case ExpressionType.Equal:
                    base.Visit(node.Left);
                    this.expression += " eq ";

                    var isString = node.Right.Type == typeof(string);
                    if (isString)
                    {
                        this.expression += "'";
                    }
                    base.Visit(node.Right);
                    if (isString)
                    {
                        this.expression += "'";
                    }
                    break;
                case ExpressionType.NotEqual:
                    this.expression += " ne ";
                    base.Visit(node.Left);
                    this.expression += ":";
                    base.Visit(node.Right);
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    this.expression += "(";
                    base.Visit(node.Left);
                    this.expression += ") or (";
                    base.Visit(node.Right);
                    this.expression += ")";
                    break;
                case ExpressionType.GreaterThan:
                    base.Visit(node.Left);
                    this.expression += " gt ";
                    base.Visit(node.Right);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    base.Visit(node.Left);
                    this.expression += " ge ";
                    base.Visit(node.Right);
                    break;
                case ExpressionType.LessThan:
                    base.Visit(node.Left);
                    this.expression += " lt ";
                    base.Visit(node.Right);
                    break;
                case ExpressionType.LessThanOrEqual:
                    base.Visit(node.Left);
                    this.expression += " le ";
                    base.Visit(node.Right);
                    break;
                default:
                    break;
            }

            return base.VisitConstant(Expression.Constant(true));
        }
    }
}
