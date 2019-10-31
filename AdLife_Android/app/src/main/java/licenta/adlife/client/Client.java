package licenta.adlife.client;

public class Client {
    public static int id;
    private int varsta;
    private String sex;
    private String status;

    public Client(int varsta, String sex, String status) {
        this.varsta = varsta;
        this.sex = sex;
        this.status = status;
    }

    public int getVarsta() {
        return varsta;
    }

    public void setVarsta(int varsta) {
        this.varsta = varsta;
    }

    public String getSex() {
        return sex;
    }

    public void setSex(String sex) {
        this.sex = sex;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
