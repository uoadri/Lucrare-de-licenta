package licenta.adlife.client;

import java.util.Calendar;
import java.util.Date;

public class Rate {
    private Asigurare asigurare;
    public static double[][] rataDbPUA;
    public static double[][] rataGCSV;
    public static double[] GCSV = new double[101];
    public static double[] dbPUA = new double[101];
    public static double[][] rateCSVPUA;
    public static boolean primulAn = false;

    public Rate(Asigurare asigurare) {
        this.asigurare = asigurare;
    }

    public Asigurare getAsigurare() {
        return asigurare;
    }

    public void setAsigurare(Asigurare asigurare) {
        this.asigurare = asigurare;
    }

    public int AnCurent() {
        Date now = new Date();
        int an = now.getYear() - asigurare.getDataAsigurare().getYear();
        Calendar cal = Calendar.getInstance();
        cal.setTime(now);

        cal.add(Calendar.YEAR, -an );

        now = cal.getTime();

        if (asigurare.getDataAsigurare().after(now)) {
            an--;
        }
        if(an == 0){
            primulAn = true;
            an = 1;
        }

        return an;
    }


    public void valoareRascumparareGarantata()
    {
       // GCSV = new double[101];
        //citireRataGCSV();

        for (int i = 0; i <= 100 - asigurare.getClient().getVarsta(); i++)
        {
            GCSV[i] = Math.round((rataGCSV[i][asigurare.getClient().getVarsta()] / 1000) * asigurare.getSumaAsigurata());
        }
    }

    public void beneficiuDecesExtra()
    {
      //  citireRatadbPUA();
      //  dbPUA = new double[101];
        dbPUA[0] = Math.round(rataDbPUA[0][asigurare.getClient().getVarsta()] * GCSV[0] * 0.055 * 0.01 / 10);
        for (int i = 1; i <= 100 - asigurare.getClient().getVarsta(); i++)
        {
            dbPUA[i] = dbPUA[i - 1] + Math.round(rataDbPUA[i][asigurare.getClient().getVarsta()] * GCSV[i] * 0.055 * 0.01 / 10);
        }
    }

    public double beneficiuDecesTotal()
    {
        double beneficiuDecesTotal= 0;
        beneficiuDecesExtra();
        int i = AnCurent() - 1;
        {
            beneficiuDecesTotal = asigurare.getSumaAsigurata() + dbPUA[i];
        }
        return beneficiuDecesTotal;
    }



    public double[] valoareRascumparareExtra()
    {
        double[] valoareRascumparareExtra = new double[101];
       // citireRataCsvPUA();
        for (int i = 0; i <= 100 - asigurare.getClient().getVarsta(); i++) {
            valoareRascumparareExtra[i] = Math.round(rateCSVPUA[i][asigurare.getClient().getVarsta()] / 1000 * dbPUA[i]);
        }
        return valoareRascumparareExtra;
    }

    public double valoareRascumparareTotala()
    {
        double valoareRascumparareTotala;
        valoareRascumparareGarantata();
        int i = AnCurent() - 1;

        valoareRascumparareTotala = GCSV[i] + valoareRascumparareExtra()[i];

        return valoareRascumparareTotala;
    }
}
