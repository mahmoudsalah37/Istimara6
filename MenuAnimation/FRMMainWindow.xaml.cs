﻿using MenuAnimado1.Controls;
using Astmara6Con.Controls;
using System.Windows;
using System.Data.Entity;
using Astmara6.Data;
using System.Linq;
using System;
using Astmara6.Controls.Fixed_Data;

namespace Astmara6Con
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class FRMMainWindow : Window
    {
        private readonly CollegeContext context = new CollegeContext();

        public FRMMainWindow()
        {
            InitializeComponent();
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCLogin());
            CollegeContext model = new CollegeContext();
            if (model.Database.CreateIfNotExists())
            {
                model.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                            "ALTER DATABASE Astmara6 COLLATE Arabic_CI_AI_KS_WS");
            }
            try
            {
                var levels = (from p in context.Subjects
                              select p).First();
            }
            catch (Exception)
            {
                
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void listViewItem_Selected(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCFixedData());
            string STRNamePage = "البيانات الثابتة";
            ChFormName(STRNamePage);

        }
        public void home()
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCLevels());
            string STRNamePage = "المستويات";
            ChFormName(STRNamePage);
        }

     
        public void ChFormName(string STRNamePage)
        {
            formname.Content = STRNamePage;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void listViewItem1_Selected(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCChangingData());
            string STRNamePage = "البيانات المتغيرة";
            ChFormName(STRNamePage);
        }


        private void ListViewItem2_Selected(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCSendData());
            string STRNamePage = "طباعة البيانات";
            ChFormName(STRNamePage);
        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
              gridShow.Children.Clear();
            gridShow.Children.Add(new UCHome());
            string STRNamePage = " ";
            ChFormName(STRNamePage);
        }

        private void btnfixed_Click(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCFixedData());
            string STRNamePage = "البيانات الثابتة";
            ChFormName(STRNamePage);

        }

        private void btnchindeing_Click(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCChangingData());
            string STRNamePage = "البيانات المتغيرة";
            ChFormName(STRNamePage);
        }

        private void printing_Click(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCSendData());
            string STRNamePage = "طباعة البيانات";
            ChFormName(STRNamePage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCEditLogin());
            string STRNamePage = "سجل الدخول اولا  ";
            ChFormName(STRNamePage);
        }
    }
}
