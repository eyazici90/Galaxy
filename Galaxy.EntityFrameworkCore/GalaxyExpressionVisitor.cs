using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Galaxy.EntityFrameworkCore
{
   public  class GalaxyExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public GalaxyExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
            {
                return _newValue;
            }

            return base.Visit(node);
        }
    }
}
