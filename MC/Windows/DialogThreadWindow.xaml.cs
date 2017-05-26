﻿using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MC.Classes;

namespace MC.Windows
{
    /// <summary>
    /// Логика взаимодействия для DilogThreadWindow.xaml
    /// </summary>
    public partial class DialogThreadWindow : MetroWindow
    {
        private object item;

        public DialogThreadWindow(object item)
        { 
            this.item = item;
            InitializeComponent();
        }
        
        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Content.ToString())
            {
                case "Thread":
                    LogicForUi.ThreadOperation(item);
                    break;
                case "Parralel":
                    LogicForUi.ParallelOperation(item);
                    break;
                case "Tasks":
                    LogicForUi.TasksOperation(item);
                    break;
                case "Async":
                    LogicForUi.AsyncOperation(item);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
