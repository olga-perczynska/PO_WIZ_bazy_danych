using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace PO_WIZ_bazy_danych.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseService.InitializeDatabase();
            var albumy = DatabaseService.PobierzNrAlbumow();
            WpisyComboBox.ItemsSource = albumy; 
            ZapiszButton.Click += ZapiszButton_Click;
        }

        private void ZapiszButton_Click(object? sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("333");
            var wniosek = new Wniosek
            {
                NrAlbumu = NrAlbumuTextBox.Text,
                NazwiskoImie = NazwiskoImieTextBox.Text,
                SemestrRok = SemestrRokTextBox.Text,
                KierunekStopien = KierunekStopienTextBox.Text,
                DataWniosku = DataWnioskuPicker.SelectedDate?.DateTime ?? DateTime.Now,
                Przedmiot = PrzedmiotTextBox.Text,
                Punkty = PunktyTextBox.Text,
                Prowadzacy = ProwadzacyTextBox.Text,
                Uzasadnienie = UzasadnienieTextBox.Text,
                DataStudent = DataStudentTextBox.Text,
                PodpisStudenta = PodpisTextBox.Text,
                CzyZgoda = ZgodaRadioButton.IsChecked ?? false,
                Komisja1 = KomisjaCzlon1TextBox.Text,
                Komisja2 = KomisjaCzlon2TextBox.Text,
                Komisja3 = KomisjaCzlon3TextBox.Text,
                DataMiejsceKomisja = DataMiejsceKomisjaTextBox.Text,
                PodpisPieczec = PodpisPieczecTextBox.Text
            };

            DatabaseService.ZapiszWniosek(wniosek);
            OdswiezComboBox();

        }

        private void WpisyComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (WpisyComboBox.SelectedItem is string nrAlbumu)
            {
                var wniosek = DatabaseService.PobierzWniosekPoNrAlbumu(nrAlbumu);
                if (wniosek != null)
                {
                    
                    NrAlbumuTextBox.Text = wniosek.NrAlbumu;
                    NazwiskoImieTextBox.Text = wniosek.NazwiskoImie;
                    SemestrRokTextBox.Text = wniosek.SemestrRok;
                    KierunekStopienTextBox.Text = wniosek.KierunekStopien;
                    DataWnioskuPicker.SelectedDate = wniosek.DataWniosku;
                    PrzedmiotTextBox.Text = wniosek.Przedmiot;
                    PunktyTextBox.Text = wniosek.Punkty;
                    ProwadzacyTextBox.Text = wniosek.Prowadzacy;
                    UzasadnienieTextBox.Text = wniosek.Uzasadnienie;
                    DataStudentTextBox.Text = wniosek.DataStudent;
                    PodpisTextBox.Text = wniosek.PodpisStudenta;
                    ZgodaRadioButton.IsChecked = wniosek.CzyZgoda;
                    BrakZgodyRadioButton.IsChecked = !wniosek.CzyZgoda;
                    KomisjaCzlon1TextBox.Text = wniosek.Komisja1;
                    KomisjaCzlon2TextBox.Text = wniosek.Komisja2;
                    KomisjaCzlon3TextBox.Text = wniosek.Komisja3;
                    DataMiejsceKomisjaTextBox.Text = wniosek.DataMiejsceKomisja;
                    PodpisPieczecTextBox.Text = wniosek.PodpisPieczec;
                }
            }
        }
        private void OdswiezComboBox()
        {
            var albumy = DatabaseService.PobierzNrAlbumow();
            WpisyComboBox.ItemsSource = albumy;
        }



    }
}