using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace koiraTietoVisa
{


    public partial class Form1 : Form
    {    // tässä on tehty String-taulukko tietovisan kysymyksiä varten
        // yksiulotteinen taulukko riittää, koska kysymyksiä 1 per rivi
        // taulukko on globaali, jotta se näkyy kaikille toiminnoille
        string[] kysymykset =
    {"Mikä on koiran latinankielinen nimi?",
     "Montako varvasta koiralla on?",
     "Mikä oli Suomen suosituin koirarotu vuonna 2019?",
     "Mikä on Suomen kansallisrotu?",
     "Mitä laumanvartijakoirat tekevät työkseen?",
     "Mikä on tärkeä osa koiran hyvinvointia?",
     "Saako koiralle antaa suklaata?",
     "Mikä on nopein koirarotu?",
     "Mikä on maailman toiseksi suosituin koirarotu?",
     "Kuinka usein koiran kynnet tulisi leikata?",
     "Onko Suomessa joidenkin koirarotujen omistaminen kiellettyä?",
     "Millainen paikka koirapuisto on?",
     "Minkä rotuinen koira oli pääosassa elokuvasarjassa Beethoven?",
     "Jos koirayksilön perimässä on rotuina beaglea ja huskya, onko se sekarotuinen vai monirotuinen koira?",
     "Mikä on maailman vanhin koirarotu?",
     "Onko muinaiset ihmiset kesyttäneet koiran vai onko koira itsekseen kesyyntynyt?",
     "Vastaako aikuinen koira kehistysasteeltaan täysikasvuista sutta vai sudenpentua?",
     "Montako hammasta on täysikasvuisella koiralla?",
     "Näkevätkö koirat punaisen värin?",
     "Mikä koirarotu on ainoa, joka ei hauku?"};
        // tässä on tehty 2-ulotteinen String-taulukko tietovisan vastauksille
        // koska vastausvaihtoehtoja on 2, taulukon pitää olla 2-ulotteinen
        // taulukko on globaali, jotta se näkyy kaikille toiminnoille
        // oikea vastaus löytyy aina kohdasta [n, 1]
        string[,] vastaukset = {
        { "Canis lupus", "Canis lupus familiaris" },
        {"10","18" },
        {"kultainennoutaja","labratorinnoutaja" },
        {"suomenajokoira","suomenpystykorva" },
        {"Paimentaa laumaa.","Vartioi laumaa." },
        {"Herkkujen antaminen.","Riittävä liikunta." },
        {"Saa.","Ei saa." },
        {"venäjänvinttikoira eli borzoi", "englanninvinttikoira eli greyhound" },
        {"labratorinnoutaja", "saksanpaimenkoira" },
        {"Kerran kuukaudessa.","Kerran viikossa." },
        {"On.","Ei ole." },
        {"Alue, jonne koirat voi jättää leikkimään keskenään.","Alue, jonne omistajat voivat mennä koiriensa kanssa." },
        {"newfoundlandinkoira","bernhardinkoira" },
        {"sekarotuinen","monirotuinen" },
        {"alaskanmalmuutti","saluki" },
        {"Muinaiset ihmiset kesyttivät koiran.","Koira on itsekseen kesyyntynyt." },
        {"täysikavuista sutta","sudenpentua" },
        {"38","42" },
        {"Näkevät.","Eivät näe." },
        {"chow chow","basenji" },
        };
        // alla esitellään globaaleja instansseja, muuttujia ja funktioita
        // nämä ovat globaaleja, koska ne pitää näkyä pelin eri toiminnoille
        // instanssi Random-luokasta, jota tarvitaan pelin kysymysten arvontaan
        Random arvanHeitto = new Random();
        // muuttuja, johon pelaajan saamat pisteet kerätään
        double kokoPisteet;
        // muuttuja, jolla määritellään moneenko kysymykseen pelaaja haluaa vastata
        int kysymystenMaara = 0;
        // muuttuja, joka kertoo tämän hetkisen esitetyn kysymyksen järjestysnumeron
        int kysymys = 0;
        // apumuuttuja, jolla määritellään kumman napin takana on oikea vastaus
        int oikeaVastaus = 0;
        // muuttuja, jolla lasketaan oikeiden vastausten suhteellinen määrä
        double suhdeLuku;
        // muuttuja, jolla liikutetaan picturebox1:stä
        bool oikea = true;
        // aputaulukko kysymys-taulukon indeksien arvontaan
        int[] kysytytKysymykset = new int[100];
        // funktio, joka arpoo uuden kysymyksen ja seuraa moneenko kysymykseen
        // pelaaja haluaa vastata, laskee kokonaispisteet ja suhdeluvun sekä
        // tulostaa pelin lopputuloksen
        // funktio alustaa arvot uutta peliä varten

        // funktio, joak käynnistää pelaajalle uuden pelin
        public void uusiPeli()
        {
            for (int l = 0; l < kysytytKysymykset.Length; l++)
            {
                kysytytKysymykset[l] = -1;
            }
                
        // gui-toimintojen näkyvyyden asetuksen (uuden) pelin alettua
            button1.Visible = false;
            label3.Visible = true;
            label1.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            label4.Visible = true;
            label2.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            kokoPisteet = 0.0;
            kysymys = 0;
            oikeaVastaus = 0;
            suhdeLuku = 0.0;
            oikea = true;
            pictureBox1.Visible = false;

            // kutsutaan funktiota uusiKysymys, jotta peli arpoo uuden kysymyksen
            uusiKysymys();
        }
        // funktio, joka lopettaa pelin ja kertoo lopputuloksen
        public void lopetaPeli()
        {
            // määritellään mitkä gui-toiminnot ovat näkyvissä pelin edetessä
            label3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label4.Visible = false;
            label2.Visible = false;
            label5.Visible = false;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            // kun pelaaja on vastannut kaikkiin kysymyksiin
            // lasketaan suhdeluku, jonka perusteella
            // tulostuu näkyviin pelin lopputulos
            // suhdeluku saadaan jakamalla kokoPisteet kysymystenMaaralla
            // koska vastaus ei ole kokonaisluku
            // muutetaan kysymystenMaara integeristä doubleksi
            suhdeLuku = kokoPisteet / (double)kysymystenMaara;
            // jos vastaaja saa enemmän kuin puolet oikein
            if (suhdeLuku > 0.5)
            {
                // pictureBox1:n kuva bordecolliesta tulee näkyväksi
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.bordercollie;
                // pictureBox1:n liikkumista ohjaava ajastin käynnistyy
                timer2.Enabled = true;
                // tuloksen teksti label tulee näkyväksi
                label6.Visible = true;
                // tämä on pelaajalle näkyvä lopputeksti
                label6.Text = "Onneksi olkoon! Olet koiratietämyksessä yhtä viisas kuin bordecollie!";

            }
            // muutoin, jos pelaaja saa alle puolet vastauksista oikein
            else
            {
                // pictureBox2:n kuva afgaaninvinttikoirasta tulee näkyväksi
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.afgaaninvinttikoira;
                // pictureBox1:n liikkumista ohjaava ajastin käynnistyy
                timer2.Enabled = true;
                // tuloksen teksti label tulee näkyväksi
                label7.Visible = true;
                // tämä on pelaajalle näkyvä lopputeksti
                label7.Text = "Voi voi. Olet koiratietämyksessä yhtä ymmärtämätön kuin afgaaninvinttikoira. Yritä uudelleen!";
            }
            button1.Visible = true;
            button1.Text = "Uusi peli";

        }

        public void uusiKysymys()
        {
            // kysymysten määrä lisääntyy yhdellä
            kysymys++;
            // tulostaa kokonaispisteet ja muuttaa int kokoPisteet Stringiksi
            label4.Text = "Pisteesi: " + kokoPisteet.ToString();
            // ehtorakenne, joka vertaa kysyttyjen kysymysten määrää 
            // pelaajan valitsemaan kysymysmäärään
            if (kysymys <= kysymystenMaara)
            {
                // arvotaan kysymykset-taulukon indeksinumero
                int i = arvanHeitto.Next(0, 19);
                // varmistetaan, että samaa kysymystä ei kysytä kahdesti yhden pelin ajan
                // varmistus tapahtuu niin, että jos taulukon elementti on jo
                // tallennettu aputaulukkoon kysytytKysymykset niin
                // arvotaan uusi indeksinumero
                while (kysytytKysymykset.Contains(i))
                {
                    // arvotaan uusi kysymykset-taulukon indeksi while-silmukassa
                    // käytetään Random-luokan metodia Next
                    i = arvanHeitto.Next(0, kysymykset.Length);
                }
                // tulostetaan kysymys ja niiden määrä pelaajalle näkyväksi
                label3.Text = "Kysymys: " + kysymys + "/" + kysymystenMaara + ": " + kysymykset[i];
                // silmukka lisää arvotun kysymykset- taulukon indeksin
                // aputaulukkoon kysytytKysymykset
                for (int k = 0; k < kysytytKysymykset.Length; k++)
                    {
                        if (kysytytKysymykset[k] == -1)
                        {
                            kysytytKysymykset[k] = i;
                            break;
                        }
                    }

                    // arvotaan kumpaan textBoxiin oikea vastaus tulostuu
                    // tällä vältetään se, että oikea vastaus olisi aina samassa laatikossa
                    int j = arvanHeitto.Next(0, 2);
                // ehtorakenne, jolla määritellään kummassa laatikossa oikea vastaus on
                if (j == 0)
                {
                    // oikea vastaus vastaukset-taulukosta asetetaan textBox1-laatikkoon
                    textBox1.Text = vastaukset[i, 1];
                    // väärä vastaus vastaukset-taulukosta asetetaan textBox2-laatikkoon
                    textBox2.Text = vastaukset[i, 0];
                    // apumuuttuja, joka kertoo minkä napin takana oikea vastaus on
                    oikeaVastaus = 1;
                }
                else
                {
                    // väärä vastaus vastaukset-taulukosta asetetaan textBox1-laatikkoon
                    textBox1.Text = vastaukset[i, 0];
                    // oikeavastaus asetetaan vastaukset-taulukosta textBox2-laatikkoon
                    textBox2.Text = vastaukset[i, 1];
                    // apumuuttuja, joka kertoo minkä napin takana oikea vastaus on
                    oikeaVastaus = 2;
                }
                // ajastin, joka määritelee kuinka kauan pelaajalla on aikaa 
                // vastata kysymykseen
                timer1.Enabled = true;
                timer1.Stop();
                timer1.Start();

            }
            else
            {
                // kutsutaan funktiota lopetaPeli
                lopetaPeli();
                
            }
            
        }



        public Form1()
        {
            // gui-toimintojen näkyvyyden asetukset pelin alussa
            // eli se miltä peli näyttää
            InitializeComponent();
            // toivottaa pelaajan tervetulleeksi peliin
            label1.Visible = true;
            // kertoo kysymyksen järjestysnumeron ja tulostaa kysymyksen näkyville taulukosta
            label3.Visible = false;
            // tähän tulostuu vastausvaihtohto pelin kulussa
            textBox1.Visible = false;
            // tähän tulostuu vastausvaihtoehto pelin kulussa
            textBox2.Visible = false;
            // vastauksen valintavaihtoehto pelin kulussa
            button2.Visible = false;
            // vastauksen valintavaihtoehto pelin kulussa
            button3.Visible = false;
            // tulostaa näkyviin kokonaispisteet pelin kulussa
            label4.Visible = false;
            // kysyy moneenko kysymykseen pelaaja haluaa vastata
            label2.Visible = true;
            // kysyy omistaako pelaaja koiran
            label5.Visible = true;
            // radioButtoneilla valitaan kuinka moneen kysymykseen vastataan
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            // checkBoxeilla kerrottaan omistaako pelaaja koiran vai ei
            checkBox1.Visible = true;
            checkBox2.Visible = true;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            // tämä nappi aloittaa koko pelin
            // alussa kokonaispisteet ovat 0
            kokoPisteet = 0;
            // alussa kysymyksen järjestysnumro on 0, koska yhtään kysymystä ei ole esitetty
            kysymys = 0;
            // tulostetaan pelaajan keräämä pistemäärä
            label4.Text = "Pisteesi: " + kokoPisteet.ToString();
            // jos pelaaja valitsee radioButton1:n
            if (radioButton1.Checked == true)
            {
                // peli esittää viisi kysymystä
                kysymystenMaara = 5;
            }
            // jos pelaaja valitsee radioButton2:n
            if (radioButton2.Checked == true)
            {
                // peli esittää kymmenen kysymystä
                kysymystenMaara = 10;
            }
            // jos pelaaja valitsee radioButton3:n
            if (radioButton3.Checked == true)
            {
                // peli esittää viisitoista kysymystä
                kysymystenMaara = 15;
            }
            // jos pelaaja valitsee radioButton4:n
            if (radioButton4.Checked == true)
            {
                // peli esittää kaksikymmentä kysymystä
                kysymystenMaara = 20;
            }
            // gui-toimintojen näkyvyyden asetuksen pelin alettua
            button1.Visible = false;
            label3.Visible = true;
            label1.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            label4.Visible = true;
            label2.Visible = false;
            label5.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            // pelin alussa kutsutaan funktiota uusiKysymys(),
            // joka arpoo kysymykset, laskee kokonaispisteet ja
            // tulostaa pelin lopputuloksen
            uusiPeli();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            // jos pelaaja painaa tätä nappia
            // eli valitsee tämän vastausvaihtoehdon
            // nappi vertaa apumuuttujaa oikeaVastaus
            // siihen, mitä vastauksia on asetettu textBoxeihin
            // jos vastaus on oikein, kokonaispisteet lisääntyvät
            if (oikeaVastaus == 1)
            {
                kokoPisteet++;
            }
            // kun vastauksen oikeellisuus on varmistettu ja pisteet laskettu
            // kutsutaan funktiota uusiKysymys, jotta peli jatkuu uudella kysymyksellä
            uusiKysymys();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // jos pelaaja painaa tätä nappia
            // eli valitsee tämän vastausvaihtoehdon
            // nappi vertaa apumuuttujaa oikeaVastaus
            // siihen, mitä vastauksia on asetettu textBoxeihin
            // jos vastaus on oikein, kokonaispisteet lisääntyvät
            if (oikeaVastaus == 2)
            {
                kokoPisteet++;
            }
            // kun vastauksen oikeellisuus on varmistettu ja pisteet laskettu
            // kutsutaan funktiota uusiKysymys, jotta peli jatkuu uudella kysymyksellä
            uusiKysymys();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // tällä koodilla määritellään
            // että pelaaja ei voi valita
            // molempia checkBoxeja saman aikaisesti
            if (checkBox1.Checked)
                checkBox2.Checked = false;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // tällä kodilla määritellään
            // että pelaaja ei voi valita
            // molempia checkBoxeja saman aikaisesti
            if (checkBox2.Checked)
                checkBox1.Checked = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // viitataan funktioon uusiKysymys eli aina,
            // kun aika on kulunut loppuun peli tulostaa
            // uuden kysymyksen eikä pelaaja saa pisteitä,
            // jos ei ehdi vastata kysymykseen
            uusiKysymys();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // tämä ajastin ohjaa sen kuvan liikkumista,
            // mikä tulostuu pelaajalle pelin lopputuloksessa
            // jos bool-muuttuja nimeltään oikea on totta (true)         
            if (oikea)
            {
                // jos vasemman reunan koordinaatti Form1:ssä
                // on isompi kuin 580, pictureBox1 ei liiku enää oikealle
                if (pictureBox1.Left > 580)
                {
                    oikea = false;
                }

            }
            // jos vasemman reunan koordinaatti Form1:ssä
            // on pienempi kuin 0, pictureBox1 ei liiku enää vasemmalle
            // eli vaihtaa suuntansa oikealle
            else
            {
                if (pictureBox1.Left < 0)
                {
                    oikea = true;
                }
            }
            // jos bool-muuttuja nimeltä oikea on totta (true)
            if (oikea)
            {
                // pictureBox1:n atribuutti Left lisääntyy yhdellä
                // eli pictureBox1 liikkuu oikealle
                pictureBox1.Left++;

            }
            // muutoin pictureBox1 liikkuu vasemmalle,
            // koska sen atribuutti Left vähenee yhdellä
            else
                pictureBox1.Left--;

        }


    }
}

