﻿//@neuecc neuecc/ObservableCollectionExtensions.cs
//Last active 2 years ago •
//Code
//Revisions 3
//Stars 2
//UniRx's ObservableCollectionExtensions. ObservableCollection and interfaces. https://github.com/mono/mono/tree/master/mcs/class/System/System.Collections.ObjectModel https://github.com/mono/mono/tree/master/mcs/class/System/System.Collections.Specialized
//ObservableCollectionExtensions.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UniRx;
using System.Reactive.Linq;
using System.Reactive;

namespace UniRx
{
    /// <summary>Value pair of OldItem and NewItem.</summary>
    public class OldNewPair<T>
    {
        public T OldItem { get; private set; }
        public T NewItem { get; private set; }

        public OldNewPair(T oldItem, T newItem)
        {
            this.OldItem = oldItem;
            this.NewItem = newItem;
        }

        public override string ToString()
        {
            return "{ Old = " + OldItem + ", New = " + NewItem + " }";
        }
    }

    public static class INotifyCollectionChangedExtensions
    {
        /// <summary>Converts CollectionChanged to an observable sequence.</summary>
        public static IObservable<NotifyCollectionChangedEventArgs> CollectionChangedAsObservable<T>(this T source)
            where T : INotifyCollectionChanged
        {
            return Observable.FromEvent<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                h => (sender, e) => h(e),
                h => source.CollectionChanged += h,
                h => source.CollectionChanged -= h);
        }
    }

    public static class ObservableCollectionExtensions
    {
        /// <summary>Observe CollectionChanged:Add and take single item.</summary>
        public static IObservable<T> ObserveAddChanged<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Add)
                .Select(e => (T)e.NewItems[0]);
            return result;
        }

        /// <summary>Observe CollectionChanged:Add.</summary>
        public static IObservable<T[]> ObserveAddChangedItems<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Add)
                .Select(e => e.NewItems.Cast<T>().ToArray());
            return result;
        }

        /// <summary>Observe CollectionChanged:Remove and take single item.</summary>
        public static IObservable<T> ObserveRemoveChanged<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Remove)
                .Select(e => (T)e.OldItems[0]);
            return result;
        }

        /// <summary>Observe CollectionChanged:Remove.</summary>
        public static IObservable<T[]> ObserveRemoveChangedItems<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Remove)
                .Select(e => e.OldItems.Cast<T>().ToArray());
            return result;
        }

        /// <summary>Observe CollectionChanged:Move and take single item.</summary>
        public static IObservable<OldNewPair<T>> ObserveMoveChanged<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Move)
                .Select(e => new OldNewPair<T>((T)e.OldItems[0], (T)e.NewItems[0]));
            return result;
        }

        /// <summary>Observe CollectionChanged:Move.</summary>
        public static IObservable<OldNewPair<T[]>> ObserveMoveChangedItems<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Move)
                .Select(e => new OldNewPair<T[]>(e.OldItems.Cast<T>().ToArray(), e.NewItems.Cast<T>().ToArray()));
            return result;
        }

        /// <summary>Observe CollectionChanged:Replace and take single item.</summary>
        public static IObservable<OldNewPair<T>> ObserveReplaceChanged<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Replace)
                .Select(e => new OldNewPair<T>((T)e.OldItems[0], (T)e.NewItems[0]));
            return result;
        }

        /// <summary>Observe CollectionChanged:Replace.</summary>
        public static IObservable<OldNewPair<T[]>> ObserveReplaceChangedItems<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Replace)
                .Select(e => new OldNewPair<T[]>(e.OldItems.Cast<T>().ToArray(), e.NewItems.Cast<T>().ToArray()));
            return result;
        }

        /// <summary>Observe CollectionChanged:Reset.</summary>
        public static IObservable<Unit> ObserveResetChanged<T>(this ObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Reset)
                .Select(_ => Unit.Default);
            return result;
        }

        // readonlycollection

        /// <summary>Observe CollectionChanged:Add and take single item.</summary>
        public static IObservable<T> ObserveAddChanged<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Add)
                .Select(e => (T)e.NewItems[0]);
            return result;
        }

        /// <summary>Observe CollectionChanged:Add.</summary>
        public static IObservable<T[]> ObserveAddChangedItems<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Add)
                .Select(e => e.NewItems.Cast<T>().ToArray());
            return result;
        }

        /// <summary>Observe CollectionChanged:Remove and take single item.</summary>
        public static IObservable<T> ObserveRemoveChanged<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Remove)
                .Select(e => (T)e.OldItems[0]);
            return result;
        }

        /// <summary>Observe CollectionChanged:Remove.</summary>
        public static IObservable<T[]> ObserveRemoveChangedItems<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Remove)
                .Select(e => e.OldItems.Cast<T>().ToArray());
            return result;
        }

        /// <summary>Observe CollectionChanged:Move and take single item.</summary>
        public static IObservable<OldNewPair<T>> ObserveMoveChanged<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Move)
                .Select(e => new OldNewPair<T>((T)e.OldItems[0], (T)e.NewItems[0]));
            return result;
        }

        /// <summary>Observe CollectionChanged:Move.</summary>
        public static IObservable<OldNewPair<T[]>> ObserveMoveChangedItems<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Move)
                .Select(e => new OldNewPair<T[]>(e.OldItems.Cast<T>().ToArray(), e.NewItems.Cast<T>().ToArray()));
            return result;
        }

        /// <summary>Observe CollectionChanged:Replace and take single item.</summary>
        public static IObservable<OldNewPair<T>> ObserveReplaceChanged<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Replace)
                .Select(e => new OldNewPair<T>((T)e.OldItems[0], (T)e.NewItems[0]));
            return result;
        }

        /// <summary>Observe CollectionChanged:Replace.</summary>
        public static IObservable<OldNewPair<T[]>> ObserveReplaceChangedItems<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Replace)
                .Select(e => new OldNewPair<T[]>(e.OldItems.Cast<T>().ToArray(), e.NewItems.Cast<T>().ToArray()));
            return result;
        }

        /// <summary>Observe CollectionChanged:Reset.</summary>
        public static IObservable<Unit> ObserveResetChanged<T>(this ReadOnlyObservableCollection<T> source)
        {
            var result = source.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Reset)
                .Select(_ => Unit.Default);
            return result;
        }
    }
}

