using AutoMapper;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Windows;

namespace TradeOptNight.Views
{
    public class ViewModelParent<T> : ViewModelBase
    {
        public Mapper MapperInstance { get; set; }

        public TableView View
        {
            get { return GetProperty(() => View); }
            set { SetProperty(() => View, value); }
        }

        public IList<T> MainGridData
        {
            get { return GetProperty(() => MainGridData); }
            set { SetProperty(() => MainGridData, value); }
        }

        public T CurrentItem
        {
            get { return GetProperty(() => CurrentItem); }
            set { SetProperty(() => CurrentItem, value); }
        }

        public T SelectedItem
        {
            get { return GetProperty(() => SelectedItem); }
            set { SetProperty(() => SelectedItem, value); }
        }

        public ColumnBase CurrentColumn
        {
            get { return GetProperty(() => CurrentColumn); }
            set { SetProperty(() => CurrentColumn, value); }
        }

        public DateTime DefaultMinDateTime
        {
            get { return new DateTime(2000, 1, 1); }
        }

        public virtual void FocusRow(T item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentItem = default(T);
                SelectedItem = default(T);
                CurrentItem = item;
                SelectedItem = item;
            });
        }

        public virtual void FocusRow(T item, ColumnBase col, bool isShowEditor, bool isSelectAll)
        {
            FocusRow(item);

            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentColumn = default(ColumnBase);
                CurrentColumn = col;

                if (isShowEditor)
                {
                    if (View != null)
                    {
                        View.ShowEditor(isSelectAll);
                    }
                }
            });
        }

        public virtual void Delete(T item)
        {
            MainGridData.Remove(item);
        }
    }
}