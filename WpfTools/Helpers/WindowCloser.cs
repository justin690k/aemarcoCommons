﻿using aemarcoCommons.WpfTools.BaseModels;
using System;
using System.Windows;

namespace aemarcoCommons.WpfTools.Helpers;

/// <summary>
/// Can be attached to a window, so it can be closed by the view model (needs to inherit BaseViewModel)
/// </summary>
public class WindowCloser
{
    public static bool GetEnableWindowClosing(DependencyObject obj) => (bool)obj.GetValue(EnableWindowClosingProperty);
    public static void SetEnableWindowClosing(DependencyObject obj, bool value) => obj.SetValue(EnableWindowClosingProperty, value);

    public static readonly DependencyProperty EnableWindowClosingProperty = DependencyProperty.RegisterAttached(
        "EnableWindowClosing",
        typeof(bool),
        typeof(WindowCloser),
        new PropertyMetadata(false, OnEnableWindowClosingChanged));

    private static void OnEnableWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //ensure is a window
        if (d is not Window window)
            return;

        window.Loaded += (_, _) =>
        {
            //ensure data context implements IBaseViewModel
            if (window.DataContext is not IBaseViewModel vm)
                return;

            vm.CloseCommand.CommandAction = _ => window.Close();
        };
        window.Closing += (_, _) =>
        {
            if (window.DataContext is not IBaseViewModel vm)
                return;

            // ReSharper disable once SuspiciousTypeConversion.Global
            if (vm is IDisposable dis)
            {
                dis.Dispose();
            }
        };
    }
}