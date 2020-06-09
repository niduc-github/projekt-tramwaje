using Edytor_mapy.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Niduc_Tramwaje;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Edytor_mapy
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Globalne_flagi
        bool dodawanie_skrzyzowania = false;
        bool dodawanie_przystanku = false;
        bool edytowanie_przystanku = false;
        bool edytowanie_przystanku_2 = false;
        bool usuwanie_przystanku = false;
        bool dodawanie_przystanku_do_linii = false;
        bool usuwanie_przystanku_z_linii = false;
        bool usuwanie_linii = false;


        #endregion
        #region Globalne_zmienne
        Bitmap img = Resources.Wrocław;
        int p_x = 0;
        int p_y = 0;
        int click_x = 0;
        int click_y = 0;
        #endregion
        #region Zmienne_do_tworzenia_mapy
        //Map map = new Map();
        //TramStop wybrany_przystanek;
        List<TrackPointSerializable> przystanki = new List<TrackPointSerializable>();
        List<TrackSerializable> linie = new List<TrackSerializable>();
        TramStopSerializable wybrany_przystanek;
        #endregion

        Bitmap RysujMapę()
        {
            Bitmap b = new Bitmap(Resources.Wrocław);
            foreach (TramStopSerializable ts in przystanki.OfType<TramStopSerializable>())
            {
                Graphics.FromImage(b).DrawRectangle(new Pen(Color.Black), ts.X - 5, ts.Y - 5, 11, 11);
            }
            foreach (JunctionSerializable ts in przystanki.OfType<JunctionSerializable>())
            {
                Graphics.FromImage(b).DrawRectangle(new Pen(Color.Black), ts.X - 2, ts.Y - 2, 5, 5);
            }
            foreach (TrackSerializable tr in linie)
            {
                for (int i = 0; i < tr.przystanki.Count - 1; i++)
                {
                    Graphics.FromImage(b).DrawLine(new Pen(Color.Black), tr.przystanki[i].X, tr.przystanki[i].Y, tr.przystanki[i + 1].X, tr.przystanki[i + 1].Y);
                }
            }
            return b;
        }

        float abs(float x)
        {
            if (x < 0) return -x;
            return x;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picMap.Image = Resources.Wrocław;
        }

        private void btnDodajPrzystanek_Click(object sender, EventArgs e)
        {
            dodawanie_skrzyzowania = false;
            dodawanie_przystanku = true;
            edytowanie_przystanku = false;
            edytowanie_przystanku_2 = false;
            usuwanie_przystanku = false;
            dodawanie_przystanku_do_linii = false;
            usuwanie_przystanku_z_linii = false;
            usuwanie_linii = false;
        }


        //dodaj skrzyzowanie
        private void button1_Click(object sender, EventArgs e)
        {
            dodawanie_skrzyzowania = true;
            dodawanie_przystanku = false;
            edytowanie_przystanku = false;
            edytowanie_przystanku_2 = false;
            usuwanie_przystanku = false;
            dodawanie_przystanku_do_linii = false;
            usuwanie_przystanku_z_linii = false;
            usuwanie_linii = false;
        }

        private void picMap_MouseHover(object sender, EventArgs e)
        {

        }

        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            p_x = e.X;
            p_y = e.Y;

            Graphics g = picMap.CreateGraphics();
            picMap.Refresh();
            g.DrawLine(new Pen(Color.Black), 0, p_y, 639, p_y);
            g.DrawLine(new Pen(Color.Black), p_x, 0, p_x, 479);
            g.Dispose();
        }

        private void picMap_Click(object sender, EventArgs e)
        {

        }

        private void picMap_MouseClick(object sender, MouseEventArgs e)
        {
            click_x = e.X;
            click_y = e.Y;

            if (dodawanie_przystanku)
            {
                przystanki.Add(new TramStopSerializable() { Name = Interaction.InputBox("Nazwa przystanku"), X = e.X, Y = e.Y, popularity = trackBar1.Value / 100f });
                Graphics.FromImage(img).DrawRectangle(new Pen(Color.Black), e.X - 5, e.Y - 5, 11, 11);
                picMap.Image = img;

                dodawanie_przystanku = false;
            }
            else if (edytowanie_przystanku)
            {
                foreach (TramStopSerializable ts in przystanki.OfType<TramStopSerializable>())
                {
                    if ((abs(ts.X - e.X) < 10 && abs(ts.Y - e.Y) < 10))
                    {
                        wybrany_przystanek = ts;
                        edytowanie_przystanku_2 = true;
                        break;
                    }
                }
                edytowanie_przystanku = false;
            }
            else if (edytowanie_przystanku_2)
            {
                wybrany_przystanek.X = e.X;
                wybrany_przystanek.Y = e.Y;
                wybrany_przystanek.Name = (Interaction.InputBox("Nazwa przystanku"));
                edytowanie_przystanku_2 = false;
            }
            else if (usuwanie_przystanku)
            {
                foreach (TramStopSerializable ts in przystanki.OfType<TramStopSerializable>())
                {
                    if ((abs(ts.X - e.X) < 10 && abs(ts.Y - e.Y) < 10))
                    {
                        wybrany_przystanek = ts;
                        przystanki.Remove(wybrany_przystanek);
                        foreach (TrackSerializable ln in linie)
                        {
                            ln.przystanki.Remove(wybrany_przystanek);
                        }
                        picMap.Image = RysujMapę();
                        break;
                    }
                }

                usuwanie_przystanku = false;
            }
            else if (dodawanie_przystanku_do_linii)
            {
                foreach (TrackPointSerializable ts in przystanki)
                {
                    if ((abs(ts.X - e.X) < 10 && abs(ts.Y - e.Y) < 10))
                    {
                        int numer = Convert.ToInt32(Interaction.InputBox("Numer linii"));
                        bool czy_istnieje = false;
                        for (int i = 0; i < linie.Count; i++)
                        {
                            if (linie[i].numer == numer)
                            {
                                czy_istnieje = true;
                                linie[i].przystanki.Add(ts);
                            }
                        }
                        if (!czy_istnieje)
                        {
                            TrackSerializable tr = new TrackSerializable();
                            tr.numer = numer;
                            tr.przystanki = new List<TrackPointSerializable>();
                            tr.przystanki.Add(ts);
                            linie.Add(tr);
                        }
                        break;
                    }
                }

                dodawanie_przystanku_do_linii = false;
            }
            else if (usuwanie_przystanku_z_linii)
            {
                foreach (TramStopSerializable ts in przystanki.OfType<TramStopSerializable>())
                {
                    if ((abs(ts.X - e.X) < 10 && abs(ts.Y - e.Y) < 10))
                    {
                        wybrany_przystanek = ts;
                        foreach (TrackSerializable ln in linie)
                        {
                            ln.przystanki.Remove(wybrany_przystanek);
                        }
                        picMap.Image = RysujMapę();
                        break;
                    }
                }
                usuwanie_przystanku_z_linii = false;
            }
            else if (dodawanie_skrzyzowania)
            {
                przystanki.Add(new JunctionSerializable() { X = e.X, Y = e.Y });
                Graphics.FromImage(img).DrawRectangle(new Pen(Color.Black), e.X - 5, e.Y - 5, 11, 11);
                picMap.Image = img;

                dodawanie_skrzyzowania = false;
            }
            picMap.Image = RysujMapę();

        }

        private void btnEdytujPrzystanek_Click(object sender, EventArgs e)
        {
            dodawanie_przystanku = false;
            edytowanie_przystanku = true;
            edytowanie_przystanku_2 = false;
            usuwanie_przystanku = false;
            dodawanie_przystanku_do_linii = false;
            usuwanie_przystanku_z_linii = false;
            usuwanie_linii = false;

        }

        private void btnUsuńPrzystanek_Click(object sender, EventArgs e)
        {
            dodawanie_przystanku = false;
            edytowanie_przystanku = false;
            edytowanie_przystanku_2 = false;
            usuwanie_przystanku = true;
            dodawanie_przystanku_do_linii = false;
            usuwanie_przystanku_z_linii = false;
            usuwanie_linii = false;
        }

        private void btnDodajPrzystanekDoLinii_Click(object sender, EventArgs e)
        {
            dodawanie_przystanku = false;
            edytowanie_przystanku = false;
            edytowanie_przystanku_2 = false;
            usuwanie_przystanku = false;
            dodawanie_przystanku_do_linii = true;
            usuwanie_przystanku_z_linii = false;
            usuwanie_linii = false;
        }

        private void btnUsuńPrzystanekZLinii_Click(object sender, EventArgs e)
        {
            dodawanie_przystanku = false;
            edytowanie_przystanku = false;
            edytowanie_przystanku_2 = false;
            usuwanie_przystanku = false;
            dodawanie_przystanku_do_linii = false;
            usuwanie_przystanku_z_linii = true;
            usuwanie_linii = false;
        }

        private void btnUsuńLinię_Click(object sender, EventArgs e)
        {
            int numer = Convert.ToInt32(Interaction.InputBox("Numer linii"));
            TrackSerializable ln = null;
            foreach (TrackSerializable ts in linie)
            {
                if (ts.numer == numer)
                {
                    ln = ts;
                    break;
                }
            }
            if (ln != null)
            {
                linie.Remove(ln);
            }
            picMap.Image = RysujMapę();
        }

        private void btnZapiszDoPliku_Click(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("przystanki.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, przystanki);
            stream.Close();

            Stream stream2 = new FileStream("linie.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream2, linie);
            stream2.Close();
        }

        private void btnWczytajZPliku_Click(object sender, EventArgs e)
        {
            przystanki = new List<TrackPointSerializable>();
            linie = new List<TrackSerializable>();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("przystanki.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            przystanki = (List<TrackPointSerializable>)formatter.Deserialize(stream);
            stream.Close();

            List<TrackSerializable> temp;

            Stream stream2 = new FileStream("linie.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            temp = (List<TrackSerializable>)formatter.Deserialize(stream2);
            stream2.Close();

            foreach (TrackSerializable ts in temp)
            {
                TrackSerializable ts1 = new TrackSerializable();
                ts1.przystanki = new List<TrackPointSerializable>();
                ts1.numer = ts.numer;
                foreach (TrackPointSerializable tps in ts.przystanki)
                {
                    foreach (TrackPointSerializable tps_real in przystanki)
                    {
                        if (tps.X == tps_real.X && tps.Y == tps_real.Y)
                        {
                            ts1.przystanki.Add(tps_real);
                        }
                    }
                }
                linie.Add(ts1);
            }

            picMap.Image = RysujMapę();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
