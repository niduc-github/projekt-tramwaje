﻿using Edytor_mapy.Properties;
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

namespace Edytor_mapy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Globalne_flagi
        bool dodawanie_przystanku = false;
        bool edytowanie_przystanku = false;
        bool edytowanie_przystanku_2 = false;
        bool usuwanie_przystanku = false;
        bool tworzenie_linii = false;
        bool edytowanie_linii = false;
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
        Map map = new Map();
        TramStop wybrany_przystanek;
        #endregion

        Bitmap RysujMapę()
        {
            Bitmap b = new Bitmap(Resources.Wrocław);
            foreach(TramStop ts in map.TramStops)
            {
                Graphics.FromImage(b).DrawRectangle(new Pen(Color.Black), ts.getPosition().X - 5, ts.getPosition().Y - 5, 11, 11);
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
            dodawanie_przystanku = true;


        }

        private void picMap_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            p_x = e.X;
            p_y = e.Y;

            //Bitmap b = new Bitmap(img);
            //Graphics g = Graphics.FromImage(b);
            Graphics g = picMap.CreateGraphics();
            picMap.Refresh();
            g.DrawLine(new Pen(Color.Black), 0, p_y, 639, p_y);
            g.DrawLine(new Pen(Color.Black), p_x, 0, p_x, 479);
            //picMap.Image = b;
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
                map.AddTrackPoint(new TramStop(Interaction.InputBox("Nazwa przystanku"), new System.Numerics.Vector2(e.X, e.Y)));
                Graphics.FromImage(img).DrawRectangle(new Pen(Color.Black), e.X - 5, e.Y - 5, 11, 11);
                picMap.Image = img;

                dodawanie_przystanku = false;
            }
            else if (edytowanie_przystanku)
            {
                foreach (TramStop ts in map.TramStops)
                {
                    if ((abs(ts.getPosition().X - e.X) < 10 && abs(ts.getPosition().Y - e.Y) < 10))
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
                wybrany_przystanek.setPosition(new System.Numerics.Vector2(e.X, e.Y));

                edytowanie_przystanku_2 = false;
            }
            else if (usuwanie_przystanku)
            {

                usuwanie_przystanku = false;
            }
            else if (tworzenie_linii)
            {

                tworzenie_linii = false;
            }
            else if (edytowanie_linii)
            {

                edytowanie_linii = false;
            }
            else if (usuwanie_linii) 
            {

                usuwanie_linii = false;
            }

            picMap.Image = RysujMapę();
        }

        private void btnEdytujPrzystanek_Click(object sender, EventArgs e)
        {
            edytowanie_przystanku = true;
        }

        private void btnEdytujPrzystanek_Click(object sender, EventArgs e)
        {
            
        }
    }
}
