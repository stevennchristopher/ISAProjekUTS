﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ManajemenArtis_Lib
{
    public class Artis
    {
        public enum status_manajer
        {
            kosong,
            aktif
        }

        #region DATAMEMBER
        private int id;
        private string nama;
        private DateTime tanggal_lahir;
        private DateTime tanggal_masuk;
        private string username;
        private string password;
        private status_manajer status;
        private Manager manager;
        #endregion

        #region CONSTRUCTOR
        public Artis()
        {
            this.Id = 0;
            this.Nama = "";
            this.Tanggal_lahir = DateTime.Now;
            this.Tanggal_masuk = DateTime.Now;
            this.Username = "";
            this.Password = "";
        }

        public Artis(int id, string nama, DateTime tanggal_lahir, DateTime tanggal_masuk, string username, string password, status_manajer status, Manager manager)
        {
            this.Id = id;
            this.Nama = nama;
            this.Tanggal_lahir = tanggal_lahir;
            this.Tanggal_masuk = tanggal_masuk;
            this.Username = username;
            this.Password = password;
            this.Status = status;
            this.Manager = manager;
        }
        #endregion

        #region PROPERTIES
        public int Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public DateTime Tanggal_lahir { get => tanggal_lahir; set => tanggal_lahir = value; }
        public DateTime Tanggal_masuk { get => tanggal_masuk; set => tanggal_masuk = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public status_manajer Status { get => status; set => status = value; }
        public Manager Manager { get => manager; set => manager = value; }
        #endregion

        #region METHODS
        public static Artis CekLogin(string username, string password)
        {
            //Password tidak diambil karena tidak perlu menyimpan password pada obyek artis untuk alasan keamanan
            string sql = "SELECT id, nama, tanggal_lahir, tanggal_masuk, username, status_manajer " +
                "FROM artis " +
                "WHERE username='" + username + "' AND password='" + password + "';";

            MySqlDataReader result = Koneksi.JalankanQuery(sql);

            Artis tmp = new Artis();
            if (result.Read())
            {
                status_manajer tmpStatus = status_manajer.kosong;

                tmp = new Artis(
                    result.GetInt32("id"),
                    result.GetString("nama"),
                    result.GetDateTime("tanggal_lahir"),
                    result.GetDateTime("tanggal_masuk"),
                    result.GetString("username"),
                    "",
                    tmpStatus, null);
            }
            return tmp;
        }

        public static List<Artis> BacaData(string criteriaName, string criteriaValue)
        {
            //string sql = "SELECT m.id, m.nama, m.tanggal_lahir, m.tanggal_masuk, m.username, m.jabatan " +
            //    "FROM manajer as m " +
            //    "INNER JOIN artis as a on m.artis_id = a.id " +
            //    "WHERE m.username='" + username + "' AND m.password='" + password + "';";
            string sql = "SELECT a.id, a.nama, a.tanggal_lahir, a.tanggal_masuk, a.username, a.status_manajer " +
                "FROM artis as a" +
                "INNER JOIN manajer as m on m.id=a.manajer_id";
            if (criteriaName == "")
            {
                sql += ";";
            }
            else
            {
                sql += "WHERE " + criteriaName + " LIKE '%" + criteriaValue + "%';";
            }
            MySqlDataReader result = Koneksi.JalankanQuery(sql);
            List<Artis> tmpList = new List<Artis>();

            while (result.Read())
            {
                Manager tmpMan = new Manager();
                tmpMan.Id = result.GetInt32("manajer_id");
                tmpMan.Nama = result.GetString("nama");

                Artis tmpData = new Artis(
                    result.GetInt32("id"),
                    result.GetString("nama"),
                    result.GetDateTime("tanggal_lahir"),
                    result.GetDateTime("tanggal_masuk"),
                    result.GetString("username"),
                    "",
                    (result.GetString("status_manajer") == "kosong" ? status_manajer.kosong : status_manajer.aktif),
                    tmpMan);

                tmpList.Add(tmpData);
            }
            return tmpList;
        }
        #endregion
    }
}
