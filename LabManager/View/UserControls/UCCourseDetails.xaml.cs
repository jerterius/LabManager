﻿using LabManager.Model;
using LabManager.Utility;
using LabManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace LabManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for UCCourseDetails.xaml
    /// </summary>
    public partial class UCCourseDetails : UserControl
    {
        private TutorsViewModel tvm;
        bool editable = false;
        public UCCourseDetails(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            InitializeComponent();
        }

        private void BtnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Do you really want to remove " + lblName.Content + " ?";
            string sCaption = "Warning!";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    
                    tvm.DeleteCourse(tvm.SelectedCourse);
                    tvm.SelectedCourse = null;

                    Storyboard sb = this.FindResource("SlideOut") as Storyboard;
                    Storyboard.SetTarget(sb, this);
                    sb.Begin();

                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;

            }
        }

        private void BtnEditCourse_Click(object sender, RoutedEventArgs e)
        {

            ToggleCourseEditable(true);

            
            
            
        }

        private void BtnAbortChanges_Click(object sender, RoutedEventArgs e)
        {

            ToggleCourseEditable(false);

            

            tvm.SelectedCourse = tvm.SelectedCourse;
        }

        private void BtnConfirmChanges_Click(object sender, RoutedEventArgs e)
        {
            // General input handling
            Dictionary<String, String> inputValues = new Dictionary<string, string>();
            inputValues.Add("Code", tbxCode.Text);
            inputValues.Add("Name", tbxName.Text);
            inputValues.Add("Credits", tbxCredits.Name);

            String message;
            if (!InputHandler.IsFieldsFilledOut(out message, inputValues))
            {
                tvm.Status = message;
                return;
            }
            
            bool valid = int.TryParse(tbxCredits.Text, out int creditsParsed);


            if (!valid)
            {
                tvm.Status = "Please insert valid credits number.";
                return;
            }

            valid = int.TryParse(tbxNumberOfStudents.Text, out int numberOfStudentsParsed);
            if (!valid)
            {
                tvm.Status = "Please insert valid number of students number.";
                return;
            }

            Course tmpCourse = new Course(tbxCode.Text, tbxName.Text, creditsParsed, numberOfStudentsParsed);

            tvm.UpdateCourse(tmpCourse);
            ToggleCourseEditable(false);
        }

        private void ToggleCourseEditable(bool b)
        {
        
            tbxCredits.IsEnabled = b;
            tbxCredits.IsReadOnly = !b;

            tbxNumberOfStudents.IsEnabled = b;
            tbxNumberOfStudents.IsReadOnly = !b;

            if (b)
            {
                btnGrpConfirmation.Visibility = Visibility.Visible;

                btnEditCourse.Visibility = Visibility.Hidden;
                btnEditCourseDisabled.Visibility = Visibility.Visible;

                grdName.Visibility = Visibility.Visible;
                lblName.Visibility = Visibility.Hidden;
      
            }
            else
            {
                btnGrpConfirmation.Visibility = Visibility.Hidden;

                btnEditCourse.Visibility = Visibility.Visible;
                btnEditCourseDisabled.Visibility = Visibility.Hidden;

                grdName.Visibility = Visibility.Hidden;
                lblName.Visibility = Visibility.Visible;


            }

            this.editable = b;
        }

        private void BtnDeleteTutoringSession_Click(object sender, RoutedEventArgs e)
        {


            TutoringSession ts = (TutoringSession)lvTutoringSessions.SelectedItem;

            if (ts!= null)
            {
                string sMessageBoxText = "Do you really want to remove " + tvm.SelectedTutoringSession.Code +"'s tutoring session on \n" + tvm.SelectedTutoringSession.StartTime + " -- " + tvm.SelectedTutoringSession.EndTime + " ?";
                string sCaption = "Warning!";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:

                        var code = ts.Code;
                        var startTime = ts.StartTime;
                        var endTime = ts.EndTime;
                        var participants = ts.NumberOfParticipants;

                        tvm.DeleteTutoringSession(code, startTime, endTime, participants);

                        //lvTutoringSessions.GetBindingExpression(ListView.ItemsSourceProperty).UpdateTarget();
                        break;

                    case MessageBoxResult.No:
                        /* ... */
                        break;

                }

                

            } else
            {
                tvm.Status = "You must select a Tutoring Session";
            }
            
        }

        private void BtnEditTutoringSession_Click(object sender, RoutedEventArgs e)
        {
            if (lvTutoringSessions.SelectedItem != null)
            {
                ToggleTutoringSessionsEditable(true);
            }
            else
            {
                tvm.Status = "You must select a Tutoring Session to edit";
            }
          

        }

        private void ToggleTutoringSessionsEditable(bool b)
        {
            lvTutoringSessions.IsEnabled = !b;

            switch (b)
            {
                case true:
                    grdUpdateTutorSessions.Visibility = Visibility.Visible;

                    btnDeleteTutoringSession.Visibility = Visibility.Hidden;
                    btnEditTutoringSession.Visibility = Visibility.Hidden;

                    btnConfirmTutoringSessionsChanges.Visibility = Visibility.Visible;
                    btnAbortTutoringSessionsChanges.Visibility = Visibility.Visible;


                    break;

                case false:
                    grdUpdateTutorSessions.Visibility = Visibility.Hidden;

                    btnDeleteTutoringSession.Visibility = Visibility.Visible;
                    btnEditTutoringSession.Visibility = Visibility.Visible;

                    btnConfirmTutoringSessionsChanges.Visibility = Visibility.Hidden;
                    btnAbortTutoringSessionsChanges.Visibility = Visibility.Hidden;



                    dtpStartTime.SetBinding(DateTimePicker.ValueProperty, new Binding("SelectedItem.StartTime")
                    {

                        ElementName = "lvTutoringSessions",
                        Mode = BindingMode.OneWay
                    });

                    dtpEndTime.SetBinding(DateTimePicker.ValueProperty, new Binding("SelectedItem.EndTime")
                    {

                        ElementName = "lvTutoringSessions",
                        Mode = BindingMode.OneWay
                    });
                    iudParticipants.SetBinding(IntegerUpDown.ValueProperty, new Binding("SelectedItem.NumberOfParticipants")
                    {

                        ElementName = "lvTutoringSessions",
                        Mode = BindingMode.OneWay
                    });


                    break;
            }

        }

        private void BtnConfirmTutoringSessionsChanges_Click(object sender, RoutedEventArgs e)
        {
            Course tmpCourse = tvm.SelectedCourse;
            DateTime tmpStartDate = dtpStartTime.Value ?? default(DateTime);
            DateTime tmpEndDate = dtpEndTime.Value ?? default(DateTime);

           
            
                TutoringSession tmpSession = new TutoringSession(tmpCourse.Code, tmpStartDate, tmpEndDate, iudParticipants.Value);

            tvm.UpdateTutoringSession(tmpSession);
            ToggleTutoringSessionsEditable(false);
            



        }

        private void BtnAbortTutoringSessionsChanges_Click(object sender, RoutedEventArgs e)
        {
            ToggleTutoringSessionsEditable(false);
        }

        private void lvTutoringSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditTutoringSession.Visibility = Visibility.Visible;
            btnDeleteTutoringSession.Visibility = Visibility.Visible;
        }

        
    }
}
