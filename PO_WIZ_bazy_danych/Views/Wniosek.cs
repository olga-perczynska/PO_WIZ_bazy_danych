using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_WIZ_bazy_danych.Views
{
    public class Wniosek
    {
        public string NrAlbumu { get; set; } = "";
        public string NazwiskoImie { get; set; } = "";
        public string SemestrRok { get; set; } = "";
        public string KierunekStopien { get; set; } = "";
        public DateTime DataWniosku { get; set; } = DateTime.Now; 
        public string Przedmiot { get; set; } = "";
        public string Punkty { get; set; } = "";
        public string Prowadzacy { get; set; } = "";
        public string Uzasadnienie { get; set; } = "";
        public string DataStudent { get; set; } = "";
        public string PodpisStudenta { get; set; } = "";
        public bool CzyZgoda { get; set; } = false;
        public string Komisja1 { get; set; } = "";
        public string Komisja2 { get; set; } = "";
        public string Komisja3 { get; set; } = "";
        public string DataMiejsceKomisja { get; set; } = "";
        public string PodpisPieczec { get; set; } = "";
    }

}
