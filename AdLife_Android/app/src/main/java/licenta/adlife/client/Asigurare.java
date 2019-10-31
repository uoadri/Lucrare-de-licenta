package licenta.adlife.client;

import java.util.Date;

public class Asigurare {
    private double sumaAsigurata;
    private double prima;
    private String perioadaPlata;
    private Date dataAsigurare;
    private Client client;
    public static double  sumaPlata;



    public Asigurare(double sumaAsigurata, double prima, String perioadaPlata, Date dataAsigurare, Client client) {
        this.sumaAsigurata = sumaAsigurata;
        this.prima = prima;
        this.perioadaPlata = perioadaPlata;
        this.dataAsigurare = dataAsigurare;
        this.client = client;
    }

    public double getSumaAsigurata() {
        return sumaAsigurata;
    }

    public void setSumaAsigurata(double sumaAsigurata) {
        this.sumaAsigurata = sumaAsigurata;
    }

    public double getPrima() {
        return prima;
    }

    public void setPrima(double prima) {
        this.prima = prima;
    }

    public String getPerioadaPlata() {
        return perioadaPlata;
    }

    public void setPerioadaPlata(String perioadaPlata) {
        this.perioadaPlata = perioadaPlata;
    }

    public Date getDataAsigurare() {
        return dataAsigurare;
    }

    public void setDataAsigurare(Date dataAsigurare) {
        this.dataAsigurare = dataAsigurare;
    }

    public Client getClient() {
        return client;
    }

    public void setClient(Client client) {
        this.client = client;
    }
}
