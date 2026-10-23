// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.UI.Xaml.Data;
using System.Collections.Specialized;
using Windows.Foundation.Collections;

namespace CommunityToolkit.WinUI.UI
{
    /// <summary>
    /// A collection view implementation that supports filtering, grouping, sorting and incremental loading
    /// </summary>
    public partial class AdvancedCollectionView
    {
        /// <summary>
        /// Currently selected item changing event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnCurrentChanging(CurrentChangingEventArgs e)
        {
            if (_deferCounter > 0)
            {
                return;
            }

            CurrentChanging?.Invoke(this, e);
        }

        /// <summary>
        /// Currently selected item changed event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnCurrentChanged(object e)
        {
            if (_deferCounter > 0)
            {
                return;
            }

            CurrentChanged?.Invoke(this, e);

            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(nameof(CurrentItem));
        }

        /// <summary>
        /// Vector changed event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnVectorChanged(IVectorChangedEventArgs e)
        {
            if (_deferCounter > 0)
            {
                return;
            }

            VectorChanged?.Invoke(this, e);

            if (CollectionChanged != null)
            {
                switch (e.CollectionChange)
                {
                    case CollectionChange.ItemInserted:
                        CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _view[(int)e.Index], (int)e.Index));
                        break;
                    case CollectionChange.ItemRemoved:
                        if (e is VectorChangedEventArgs args)
                        {
                            CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, args.Item, (int)e.Index));
                        }

                        break;
                    case CollectionChange.ItemChanged:
                        CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, _view[(int)e.Index], _view[(int)e.Index], (int)e.Index));
                        break;
                    case CollectionChange.Reset:
                        CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        break;
                }
            }

            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(nameof(Count));
        }
    }
}