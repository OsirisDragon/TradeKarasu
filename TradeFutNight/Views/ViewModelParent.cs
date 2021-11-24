using AutoMapper;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows;

namespace TradeFutNight.Views
{
    public class ViewModelParent<T> : ViewModelBase
    {
        public Mapper MapperInstance { get; set; }

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

        public DateTime DefaultMinDateTime
        {
            get { return new DateTime(2000, 1, 1); }
        }

        public virtual void SetCurrentAndSelectedItem(T item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentItem = default(T);
                SelectedItem = default(T);
                CurrentItem = item;
                SelectedItem = item;
            });
        }
    }
}