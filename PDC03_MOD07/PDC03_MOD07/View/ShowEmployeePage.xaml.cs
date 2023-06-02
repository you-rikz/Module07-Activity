﻿using PDC03_MOD07.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PDC03_MOD07.ViewModel;
using PDC03_MOD07.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDC03_MOD07.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowEmployeePage : ContentPage
    {
        EmployeeViewModel viewModel;
        public ShowEmployeePage()
        {
            InitializeComponent();
            viewModel = new EmployeeViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            showEmployee();
        }
        private void showEmployee()
        {
            var res = viewModel.GetAllEmployees().Result;
            lstData.ItemsSource = res;
        }
        private void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddEmployee());
        }
        private async void lstData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                EmployeeModel obj = (EmployeeModel)e.SelectedItem;
                string res = await DisplayActionSheet("Operation", "Cancel", null, "Update", "Delete");
            

            switch (res)
            {
                case "Update":
                    await this.Navigation.PushAsync(new AddEmployee(obj));

                    break;
                case "Delete":
                    viewModel.DeleteEmployee(obj);
                    showEmployee();
                    break;
            }
            lstData.SelectedItem = null;
            }
        }
    }
}