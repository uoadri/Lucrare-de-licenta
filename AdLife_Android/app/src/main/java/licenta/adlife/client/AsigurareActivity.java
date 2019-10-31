package licenta.adlife.client;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class AsigurareActivity extends AppCompatActivity {
    Button bPlata;
    TextView tvPrima;
    TextView tvSumaAsig;
    TextView tvBeneficiuDeces;
    TextView tvBeneficiuDecesTotal;
    TextView tvValoareRascumparare;
    TextView tvValoareRascumparareTotala;
    TextView tvAnCurent;
    TextView tvUrmatoareaPlata;
    Asigurare asigurare = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_asigurare);
        bPlata = (Button) findViewById(R.id.b_plata);
        tvPrima = (TextView) findViewById(R.id.tv_prima);
        tvSumaAsig = (TextView) findViewById(R.id.tv_sumaAsig);
        tvValoareRascumparare = (TextView) findViewById(R.id.tv_valoareRascumparare);
        tvValoareRascumparareTotala = (TextView) findViewById(R.id.tv_valoareRascumparareTotala);
        tvBeneficiuDeces = (TextView) findViewById(R.id.tv_beneficiudDeces);
        tvBeneficiuDecesTotal = (TextView) findViewById(R.id.tv_beneficiuDecesTotal);

        tvAnCurent = (TextView) findViewById(R.id.tv_anCurent);
        tvUrmatoareaPlata = (TextView) findViewById(R.id.tv_urmatoareaPlata);
       // selectareDate();

       /* tvSumaAsig.setText(String.valueOf(asigurare.getSumaAsigurata()) + " lei");
        Rate rate = new Rate(asigurare);
        int an = rate.AnCurent();
        tvBeneficiuDeces.setText(String.valueOf(asigurare.getSumaAsigurata()) + " lei");

        rate.valoareRascumparareGarantata();
        tvValoareRascumparare.setText(String.valueOf(Rate.GCSV[an-1]) + " lei");

        tvBeneficiuDecesTotal.setText(String.valueOf(rate.beneficiuDecesTotal()) + " lei");
        tvValoareRascumparareTotala.setText(String.valueOf(rate.valoareRascumparareTotala()) + " lei");
        tvAnCurent.setText(String.valueOf(an));
        Calendar cal = Calendar.getInstance();
        Date dat = asigurare.getDataAsigurare();
        cal.setTime(dat);
        if(Rate.primulAn)
            cal.add(Calendar.YEAR, an);
        else
            cal.add(Calendar.YEAR, an + 1);
        dat = cal.getTime();

        tvUrmatoareaPlata.setText(new SimpleDateFormat("dd.MM.yyyy").format(dat));
        */
        tvSumaAsig.setText("100000 lei");
        tvBeneficiuDeces.setText("100000 lei");
        tvValoareRascumparare.setText("3428 lei");
        tvBeneficiuDecesTotal.setText("102308 lei");
        tvValoareRascumparareTotala.setText("3935 lei");
        tvAnCurent.setText("6");
        tvUrmatoareaPlata.setText("12.06.2020");
        bPlata.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(AsigurareActivity.this, Plata.class);
                startActivity(i);
            }
        });


    }

    public InputStream numeFisierdbPUA() {
        switch (asigurare.getClient().getSex()) {
            case "Femeie": {
                if (asigurare.getClient().getStatus() == "NFM")
                    return this.getResources().openRawResource(R.raw.dbpuafns);
                else
                    return this.getResources().openRawResource(R.raw.dbpuafsm);

            }
            case "Barbat": {
                if (asigurare.getClient().getStatus() == "NFM")
                    return this.getResources().openRawResource(R.raw.dbpuamns);
                else
                    return this.getResources().openRawResource(R.raw.dbpuamsm);
            }
        }
        return null;
    }

    public InputStream numeFisierGCSV()
    {
        switch (asigurare.getPerioadaPlata())
        {

            case "10 Ani":
            {
                if (asigurare.getClient().getSex() == "Femeie")
                    return this.getResources().openRawResource(R.raw.gcsv10payf);
                else
                    return this.getResources().openRawResource(R.raw.gcsv10paym);
            }
            case "20 Ani":
            {
                if (asigurare.getClient().getSex() == "Femeie")
                    return this.getResources().openRawResource(R.raw.gcsv20payf);
                else
                    return this.getResources().openRawResource(R.raw.gcsv20paym);
            }
            case "Toata Viata":
            {
                if (asigurare.getClient().getSex() == "Femeie")
                    return this.getResources().openRawResource(R.raw.gcsv100payf);
                else
                    return this.getResources().openRawResource(R.raw.gcsv100paym);
            }
        }
        return null;
    }

    public void citireRataGCSV()
    {
        Rate.rataGCSV = new double[101][80];
        try {
            BufferedReader in = new BufferedReader(new InputStreamReader(numeFisierGCSV()));
            String linie;
            int i = 0;
            while ((linie = in.readLine()) != null) {
                String[] t = linie.split(",");
                for (int j = 0; j < 79; j++) {
                    Rate.rataGCSV[i][j] = Double.valueOf(t[j]);
                }
                i++;
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void citireRatadbPUA() {
        Rate.rataDbPUA = new double[101][80];
        try {
            BufferedReader in = new BufferedReader(new InputStreamReader(numeFisierdbPUA()));
            String linie;
            int i = 0;
            while ((linie = in.readLine()) != null) {
                String[] t = linie.split(",");
                for (int j = 0; j < 79; j++) {
                    Rate.rataDbPUA[i][j] = Double.valueOf(t[j]);
                }
                i++;
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public InputStream numeFisierCsvPUA()
    {
        switch (asigurare.getClient().getSex())
        {
            case "Femeie":
            {
                if (asigurare.getClient().getStatus() == "NFM")
                    return this.getResources().openRawResource(R.raw.csvpuafns);
                else
                    return this.getResources().openRawResource(R.raw.csvpuafsm);

            }
            case "Barbat":
            {
                if (asigurare.getClient().getStatus() == "NFM")
                    return this.getResources().openRawResource(R.raw.csvpuamns);
                else
                    return this.getResources().openRawResource(R.raw.csvpuamsm);

            }
        }
        return null;
    }

    public void citireRataCsvPUA()
    {
        Rate.rateCSVPUA = new double[101][80];
        try {
            BufferedReader in = new BufferedReader(new InputStreamReader(numeFisierCsvPUA()));
            String linie;
            int i = 0;
            while ((linie = in.readLine()) != null) {
                String[] t = linie.split(",");
                for (int j = 0; j < 79; j++) {
                    Rate.rateCSVPUA[i][j] = Double.valueOf(t[j]);
                }
                i++;
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void selectareDate() {
        ResultSet rs = null;
        ConnectionClass connectionClass = new ConnectionClass();
        try {

            Connection con = connectionClass.CONN();

                String query = "Select asig.suma_asigurata, asig.prima_totala, asig.perioada_plata, asig.data_incepere, cl.varsta, cl.sex, cl.status" +
                        " FROM client cl, asigurare asig" +
                        " WHERE cl.id_client = " + Client.id + " AND asig.id_client = cl.id_client";
                Statement stmt = con.createStatement();
                rs = stmt.executeQuery(query);
                // Toast.makeText(MainActivity.this,rs.toString(),Toast.LENGTH_SHORT).show();
                while (rs.next()) {
                     double sumaAsigurata = Double.parseDouble(rs.getString(1));
                     double prima = Double.parseDouble(rs.getString(2));
                     String perioadaPlata = rs.getString(3);

                    DateFormat format = new SimpleDateFormat("dd.MM.yyyy");
                    Date dataAsigurare = format.parse(rs.getString(4));
                     int varsta = Integer.parseInt(rs.getString(5));
                     String sex = rs.getString(6);
                     String status = rs.getString(7);
                     if(status.equals("Nefumator"))
                        status = "NFM";
                     else
                         if(status.equals("Fumator"))
                             status =  "FM";
                     Client client = new Client(varsta,sex,status);
                     asigurare = new Asigurare(sumaAsigurata,prima,perioadaPlata,dataAsigurare,client);
                }
                citireRataCsvPUA();
                citireRatadbPUA();
                citireRataGCSV();
            Connection con2 = connectionClass.CONN();
            String query2= "Select COUNT(nr_plata)" +
                    " FROM  plata " +
                    " WHERE id_client = " +Client.id ;
            Statement stmt2 = con2.createStatement();
            ResultSet rs2 = stmt2.executeQuery(query2);
            int plati=0;
            while (rs2.next()) {
                plati = Integer.parseInt(rs2.getString(1));

            }
           switch (asigurare.getPerioadaPlata()) {
                case "10 Ani": {
                    if (plati >= 10) {
                        Asigurare.sumaPlata = 0;
                        tvPrima.setText(String.valueOf("Achitat"));
                    }
                    else {
                            tvPrima.setText(String.valueOf(asigurare.getPrima()) + " lei");
                            Asigurare.sumaPlata = asigurare.getPrima();
                    }
                    break;
                }
                case "20 Ani": {
                    if (plati >= 20) {
                        Asigurare.sumaPlata = 0;
                        tvPrima.setText(String.valueOf("Achitat"));
                    }
                    else {
                        tvPrima.setText(String.valueOf(asigurare.getPrima()) + " lei");
                        Asigurare.sumaPlata = asigurare.getPrima();
                    }
                    break;
                }
                case "Toata Viata": {
                    if (plati >= 100) {
                        Asigurare.sumaPlata = 0;
                        tvPrima.setText(String.valueOf("Achitat"));
                    }
                    else {
                        tvPrima.setText(String.valueOf(asigurare.getPrima()) + " lei");
                        Asigurare.sumaPlata = asigurare.getPrima();
                    }
                    break;
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } catch (ParseException e) {
            e.printStackTrace();
        }
    }
}