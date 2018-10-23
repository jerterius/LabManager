﻿using LabManager.Model;
using LabManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabManager.View
{
    /// <summary>
    /// Interaction logic for PublicView.xaml
    /// </summary>
    public partial class PublicView : Window
    {

        static DateTime startDate = System.DateTime.Now;

        DateTime endDate = startDate.AddMonths(1);

        TutorsViewModel tvm = new TutorsViewModel();

        public PublicView()
        {
           
            InitializeComponent();

           
        
            dgGeneralTemplate.ItemsSource = tvm.Tutors;
           
            
           



            if (startDate != null && endDate != null)
            {


                while (startDate < endDate)
                {
                    DataGridTextColumn newColumn = new DataGridTextColumn();
                    newColumn.Header = startDate.ToString("ddd dd/M", CultureInfo.InvariantCulture);
                    dgGeneralTemplate.Columns.Add(newColumn);
                    

                    startDate = startDate.AddDays(1);
                }
            }
        }

        private bool isEditable = false;
        private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditable) { 
                tbxSsn.IsEnabled = true;
                tbxSsn.IsReadOnly = false;

                tbxEmail.IsEnabled = true;
                tbxEmail.IsReadOnly = false;
            } else
            {
                tbxSsn.IsEnabled = false;
                tbxSsn.IsReadOnly = true;

                tbxEmail.IsEnabled = false;
                tbxEmail.IsReadOnly = true;
            }
            isEditable = !isEditable;

           
            

        }

        private void BtnDeleteTutor_Click(object sender, RoutedEventArgs e)
        {
            String ssn = tbxSsn.Text;
            tvm.DeleteTutor(ssn);
        }



        //private void dpStartEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    startDate = (DateTime)dpStartDate.SelectedDate;
        //    dpEndDate.SelectedDate = startDate.AddMonths(1);

        //    if (startDate!=null && endDate != null)
        //    {
        //        for (int i = 1; i < dgGeneralTemplate.Columns.Count; i++)
        //        {
        //            Console.WriteLine("Removing" + i);
        //            dgGeneralTemplate.Columns.RemoveAt(i);

        //        }


        //        while (startDate < endDate)
        //        {

        //            DataGridTextColumn newColumn = new DataGridTextColumn();
        //            newColumn.Header = startDate.ToString("ddd dd/M", CultureInfo.InvariantCulture);


        //            dgGeneralTemplate.Columns.Add(newColumn);


        //        }
        //    }

        //}
    }
}
