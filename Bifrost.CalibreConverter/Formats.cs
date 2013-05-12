using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Bifrost.CalibreConverter
{
    public abstract class Formats : IEnumerable<FormatItem>
    {
        #region members

        private readonly Dictionary<string, FormatItem> _formats;
        private readonly List<FormatItem> _sortedFormats;

        #endregion

        #region ctors

        protected Formats()
        {
            _formats = new Dictionary<string, FormatItem>(StringComparer.InvariantCultureIgnoreCase);
            _sortedFormats = new List<FormatItem>();
        }

        #endregion

        #region properties

        public int Count
        {
            get { return _formats.Count; }
        }

        #endregion

        #region methods

        public FormatItem this[string suffix]
        {
            get
            {
                Contract.Requires(!string.IsNullOrEmpty(suffix));

                if (suffix.StartsWith(".")) {
                    suffix = suffix.Substring(1);
                }

                FormatItem format;
                _formats.TryGetValue(suffix, out format);
                return format;
            }
        }

        public bool Contains(string suffix)
        {
            Contract.Requires(!string.IsNullOrEmpty(suffix));

            if (suffix.StartsWith(".")) {
                suffix = suffix.Substring(1);
            }

            bool contains = _formats.ContainsKey(suffix);
            return contains;
        }

        protected bool Add(FormatItem fmt)
        {
            Contract.Requires(fmt != null);

            bool added = false;
            if (!_formats.ContainsKey(fmt.Extension)) {
                _formats.Add(fmt.Extension, fmt);
                _sortedFormats.Add(fmt);
                added = true;
            }
            return added;
        }

        public IEnumerator<FormatItem> GetEnumerator()
        {
            return _sortedFormats.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
