using Microsoft.Data.Sqlite;
using PO_WIZ_bazy_danych.Views;
using System;
using System.Collections.Generic;
using System.IO;

public static class DatabaseService
{
    private static readonly string dbPath = "wnioski.db";

    public static void InitializeDatabase()
    {
        if (!File.Exists(dbPath))
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS Wnioski (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                NrAlbumu TEXT,
                NazwiskoImie TEXT,
                SemestrRok TEXT,
                KierunekStopien TEXT,
                DataWniosku TEXT,
                Przedmiot TEXT,
                Punkty TEXT,
                Prowadzacy TEXT,
                Uzasadnienie TEXT,
                DataStudent TEXT,
                PodpisStudenta TEXT,
                CzyZgoda INTEGER,
                Komisja1 TEXT,
                Komisja2 TEXT,
                Komisja3 TEXT,
                DataMiejsceKomisja TEXT,
                PodpisPieczec TEXT
            )";
            tableCmd.ExecuteNonQuery();
        }
    }

    public static void ZapiszWniosek(Wniosek wniosek)
    {
        
        string SafeString(string? input) => string.IsNullOrWhiteSpace(input) ? "Brak danych" : input;

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        connection.Open();

        var insertCmd = connection.CreateCommand();
        insertCmd.CommandText =
        @"
    INSERT INTO Wnioski (
        NrAlbumu, NazwiskoImie, SemestrRok, KierunekStopien, DataWniosku,
        Przedmiot, Punkty, Prowadzacy, Uzasadnienie, DataStudent,
        PodpisStudenta, CzyZgoda, Komisja1, Komisja2, Komisja3,
        DataMiejsceKomisja, PodpisPieczec
    ) VALUES (
        $nrAlbumu, $nazwiskoImie, $semestrRok, $kierunekStopien, $dataWniosku,
        $przedmiot, $punkty, $prowadzacy, $uzasadnienie, $dataStudent,
        $podpisStudenta, $czyZgoda, $komisja1, $komisja2, $komisja3,
        $dataMiejsce, $podpisPieczec
    )";

        insertCmd.Parameters.AddWithValue("$nrAlbumu", SafeString(wniosek.NrAlbumu));
        insertCmd.Parameters.AddWithValue("$nazwiskoImie", SafeString(wniosek.NazwiskoImie));
        insertCmd.Parameters.AddWithValue("$semestrRok", SafeString(wniosek.SemestrRok));
        insertCmd.Parameters.AddWithValue("$kierunekStopien", SafeString(wniosek.KierunekStopien));
        insertCmd.Parameters.AddWithValue("$dataWniosku", wniosek.DataWniosku == default ? DateTime.Now.ToString("yyyy-MM-dd") : wniosek.DataWniosku.ToString("yyyy-MM-dd"));
        insertCmd.Parameters.AddWithValue("$przedmiot", SafeString(wniosek.Przedmiot));
        insertCmd.Parameters.AddWithValue("$punkty", SafeString(wniosek.Punkty));
        insertCmd.Parameters.AddWithValue("$prowadzacy", SafeString(wniosek.Prowadzacy));
        insertCmd.Parameters.AddWithValue("$uzasadnienie", SafeString(wniosek.Uzasadnienie));
        insertCmd.Parameters.AddWithValue("$dataStudent", SafeString(wniosek.DataStudent));
        insertCmd.Parameters.AddWithValue("$podpisStudenta", SafeString(wniosek.PodpisStudenta));
        insertCmd.Parameters.AddWithValue("$czyZgoda", wniosek.CzyZgoda ? 1 : 0);
        insertCmd.Parameters.AddWithValue("$komisja1", SafeString(wniosek.Komisja1));
        insertCmd.Parameters.AddWithValue("$komisja2", SafeString(wniosek.Komisja2));
        insertCmd.Parameters.AddWithValue("$komisja3", SafeString(wniosek.Komisja3));
        insertCmd.Parameters.AddWithValue("$dataMiejsce", SafeString(wniosek.DataMiejsceKomisja));
        insertCmd.Parameters.AddWithValue("$podpisPieczec", SafeString(wniosek.PodpisPieczec));

        insertCmd.ExecuteNonQuery();
    }

    public static List<string> PobierzNrAlbumow()
    {
        var lista = new List<string>();

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        connection.Open();

        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT NrAlbumu FROM Wnioski";

        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(reader.GetString(0));
        }

        return lista;
    }
    public static Wniosek? PobierzWniosekPoNrAlbumu(string nrAlbumu)
    {
        using var connection = new SqliteConnection($"Data Source={dbPath}");
        connection.Open();

        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM Wnioski WHERE NrAlbumu = $nrAlbumu LIMIT 1";
        selectCmd.Parameters.AddWithValue("$nrAlbumu", nrAlbumu);

        using var reader = selectCmd.ExecuteReader();
        if (reader.Read())
        {
            return new Wniosek
            {
                NrAlbumu = reader["NrAlbumu"].ToString(),
                NazwiskoImie = reader["NazwiskoImie"].ToString(),
                SemestrRok = reader["SemestrRok"].ToString(),
                KierunekStopien = reader["KierunekStopien"].ToString(),
                DataWniosku = DateTime.Parse(reader["DataWniosku"].ToString()!),
                Przedmiot = reader["Przedmiot"].ToString(),
                Punkty = reader["Punkty"].ToString(),
                Prowadzacy = reader["Prowadzacy"].ToString(),
                Uzasadnienie = reader["Uzasadnienie"].ToString(),
                DataStudent = reader["DataStudent"].ToString(),
                PodpisStudenta = reader["PodpisStudenta"].ToString(),
                CzyZgoda = Convert.ToInt32(reader["CzyZgoda"]) == 1,
                Komisja1 = reader["Komisja1"].ToString(),
                Komisja2 = reader["Komisja2"].ToString(),
                Komisja3 = reader["Komisja3"].ToString(),
                DataMiejsceKomisja = reader["DataMiejsceKomisja"].ToString(),
                PodpisPieczec = reader["PodpisPieczec"].ToString()
            };
        }

        return null;
    }

}
