namespace Rhymba.Services.Search
{
    using Rhymba.Models.Search;
    using System.Linq.Expressions;

    public class SearchRequestBuilder
    {
        private SearchRequest searchRequest;

        internal SearchRequestBuilder()
        {
            this.searchRequest = new SearchRequest();
        }

        public SearchRequestBuilder Expand(string[] expands)
        {
            this.searchRequest.expand = expands;
            return this;
        }

        public SearchRequestBuilder Filter<T>(Expression<Func<T, bool>> filter)
        {
            var expressionBuilder = new SearchFilterExpressionVisitor();
            expressionBuilder.Visit(filter);
            this.searchRequest.filter = expressionBuilder.GetExpression();
            return this;
        }

        public SearchRequestBuilder ById(int[] ids)
        {
            this.searchRequest.id_cdl = ids;
            return this;
        }

        public SearchRequestBuilder InlineCount(bool inlineCount)
        {
            this.searchRequest.inlinecount = inlineCount;
            return this;
        }

        public SearchRequestBuilder Keyword(string keyword)
        {
            this.searchRequest.keyword = keyword;
            return this;
        }

        public SearchRequestBuilder Select(string[] select)
        {
            this.searchRequest.select = select;
            return this;
        }

        public SearchRequestBuilder Skip(int skip)
        {
            this.searchRequest.skip = skip;
            return this;
        }

        public SearchRequestBuilder Top(int top)
        {
            this.searchRequest.top = top;
            return this;
        }

        public SearchRequestBuilder FullSearch(bool  fullSearch)
        {
            this.searchRequest.full_search = fullSearch;
            return this;
        }

        public SearchRequest GetRequest()
        {
            return this.searchRequest;
        }
    }
}
