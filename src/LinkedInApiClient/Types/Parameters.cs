using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable

namespace LinkedInApiClient.Types
{
    /// <summary>
    /// URL query parameter collection
    /// this is a Mutable collection but that may change in a future version to be immutable.
    /// </summary>
    public class Parameters : IEnumerable<KeyValuePair<string, string?>>
    {
        private IDictionary<string, string?> parameters;

        public static readonly Parameters EmptyParameters = new Parameters();

        public Parameters()
        {
            this.parameters = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        }
        public Parameters(IEnumerable<KeyValuePair<string, string?>> queryParameters)
        {
            this.parameters = new Dictionary<string, string?>(queryParameters, StringComparer.OrdinalIgnoreCase);
        }

        public bool DisableValueEcoding { get; set; }

        /// <summary>
        /// Get parameter value based on name
        /// </summary>
        /// <param name="index"></param>
        public string? this[string index]
        {
            get => parameters[index];
            set => parameters[index] = value;
        }

        /// <summary>
        /// Checks the existence of a parameter
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return (this.Any(k => string.Equals(k.Key, key)));
        }

        public Parameters Add(string name, string value)
        {
            parameters.Add(name, value);
            return this;
        }

        public Parameters Add(KeyValuePair<string, string?> par)
        {
            parameters.Add(par);
            return this;
        }

        public Parameters AddRange(IEnumerable<KeyValuePair<string, string?>> par)
        {
            parameters = new Dictionary<string, string?>(parameters.Concat(par), StringComparer.OrdinalIgnoreCase);
            return this;
        }

        public static Parameters operator +(Parameters collection, IEnumerable<KeyValuePair<string, string?>> par)
        {
            return collection.AddRange(par);
        }
        public static Parameters operator +(Parameters collection, KeyValuePair<string, string?> par)
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
            UrlHelper.AppendQueryToUrl(url, this.parameters, !DisableValueEcoding);

        public IEnumerator<KeyValuePair<string, string?>> GetEnumerator() => parameters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => parameters.GetEnumerator();

        /// <summary>
        /// Turns anonymous type or dictionary in Parameters (mainly for backwards compatibility)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Parameters FromObject(object values)
        {
            if (values == null)
            {
                return EmptyParameters;
            }

            if (values is Dictionary<string, string?> dictionary)
            {
                return new Parameters(dictionary);
            }

            dictionary = new Dictionary<string, string?>();

            foreach (var prop in values.GetType().GetRuntimeProperties())
            {
                var value = prop.GetValue(values) as string;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    dictionary.Add(prop.Name, value);
                }
            }

            return new Parameters(dictionary);
        }

        /// <summary>
        /// Merge two parameter sets
        /// </summary>
        /// <param name="additionalValues"></param>
        /// <returns>Merged parameters</returns>
        public Parameters Merge(Parameters additionalValues)
        {
            if (additionalValues != null)
            {
                var merged =
                    this.Concat(additionalValues.Where(add => !this.ContainsKey(add.Key)))
                        .Select(s => new KeyValuePair<string, string?>(s.Key, s.Value));

                return new Parameters(merged);
            }

            return this;
        }
    }
}
