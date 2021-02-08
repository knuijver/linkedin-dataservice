using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace LinkedInApiClient.Types
{
    /// <summary>
    /// URL query parameter collection
    /// this is a Mutable collection but that may change in a future version to be immutable.
    /// </summary>
    public class QueryParameterCollection : IEnumerable<KeyValuePair<string, string>>
    {
        private IDictionary<string, string> parameters;

        public static readonly QueryParameterCollection EmptyParameters = new QueryParameterCollection();

        public QueryParameterCollection()
        {
            this.parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
        public QueryParameterCollection(IEnumerable<KeyValuePair<string, string>> queryParameters)
        {
            this.parameters = new Dictionary<string, string>(queryParameters, StringComparer.OrdinalIgnoreCase);
        }

        public string this[string index]
        {
            get => parameters[index];
            set => parameters[index] = value;
        }

        public QueryParameterCollection Add(string name, string value)
        {
            parameters.Add(name, value);
            return this;
        }

        public QueryParameterCollection Add(KeyValuePair<string, string> par)
        {
            parameters.Add(par);
            return this;
        }

        public QueryParameterCollection AddRange(IEnumerable<KeyValuePair<string, string>> par)
        {
            parameters = new Dictionary<string, string>(parameters.Concat(par), StringComparer.OrdinalIgnoreCase);
            return this;
        }

        public static QueryParameterCollection operator +(QueryParameterCollection collection, IEnumerable<KeyValuePair<string, string>> par)
        {
            return collection.AddRange(par);
        }
        public static QueryParameterCollection operator +(QueryParameterCollection collection, KeyValuePair<string, string> par)
        {
            collection[par.Key] = par.Value;
            return collection;
        }

        /// <summary>
        /// Adds all key, value pairs as querystring to the given URL.
        /// We do not check for existing or duplicate keys on the given URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string ToUrlQueryString(string url) =>
            UrlHelper.AppendQueryToUrl(url, this.parameters);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => parameters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => parameters.GetEnumerator();
    }
}
