using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Bifrost.CalibreConverter
{
    internal class Resources : IDisposable
    {
        #region members

        private static readonly Resources _instance = new Resources();

        private ImageResourcesImpl _images;
        private IconResourcesImpl _icons;

        #endregion

        #region ctors

        private Resources()
        {
        }

        #endregion

        #region properties

        public static Resources Instance
        {
            get { return _instance; }
        }

        public ImageResources Images
        {
            get { return _images ?? (_images = new ImageResourcesImpl()); }
        }

        public IconResources Icons
        {
            get { return _icons ?? (_icons = new IconResourcesImpl()); }
        }

        #endregion

        #region methods

        public void Dispose()
        {
            if (_images != null) {
                _images.Dispose();
                _images = null;
            }
            if (_icons != null) {
                _icons.Dispose();
                _icons = null;
            }
        }

        #endregion

        #region inner types

        internal interface ImageResources
        {
            Image Application { get; }
            Image Add { get; }
            Image Remove { get; }
            Image RemoveFinished { get; }
            Image ShowLog { get; }
            Image Convert { get; }
        }

        internal interface IconResources
        {
            Icon Application { get; }
            Icon Log { get; }
        }

        private abstract class ResourcesImpl<T> : IDisposable where T : class, IDisposable
        {
            #region members

            private readonly Dictionary<string, T> _items;

            #endregion

            #region ctors

            protected ResourcesImpl()
            {
                _items = new Dictionary<string, T>();
            }

            public void Dispose()
            {
                foreach (KeyValuePair<string, T> pair in _items) {
                    pair.Value.Dispose();
                }

                _items.Clear();
            }

            #endregion

            #region methods

            protected T GetOrAdd(string name)
            {
                T item;
                if (!_items.TryGetValue(name, out item)) {
                    Stream str = GetStream(name);
                    if (str != null) {
                        item = LoadItem(str);
                        if (item != null) {
                            _items.Add(name, item);
                        }
                    }
                }

                return item;
            }

            private Stream GetStream(string name)
            {
                string ns = GetType().Namespace;
                Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.Resources.{1}", ns, name));
                return str;
            }

            protected abstract T LoadItem(Stream str);

            #endregion
        }

        private class ImageResourcesImpl : ResourcesImpl<Image>, ImageResources
        {
            #region properties

            public Image Application
            {
                get { return GetOrAdd("app.png"); }
            }

            public Image Add
            {
                get { return GetOrAdd("add.png"); }
            }

            public Image Remove
            {
                get { return GetOrAdd("remove.png"); }
            }

            public Image RemoveFinished
            {
                get { return GetOrAdd("clear.png"); }
            }

            public Image ShowLog
            {
                get { return GetOrAdd("show_log.png"); }
            }

            public Image Convert
            {
                get { return GetOrAdd("convert.png"); }
            }

            #endregion

            #region methods

            protected override Image LoadItem(Stream str)
            {
                Image image = Image.FromStream(str);
                return image;
            }

            #endregion
        }

        private class IconResourcesImpl : ResourcesImpl<Icon>, IconResources
        {
            #region properties

            public Icon Application
            {
                get { return GetOrAdd("app.ico"); }
            }

            public Icon Log
            {
                get { return GetOrAdd("show_log.ico"); }
            }

            #endregion

            #region methods

            protected override Icon LoadItem(Stream str)
            {
                Icon icon = new Icon(str);
                return icon;
            }

            #endregion
        }

        #endregion
    }
}
