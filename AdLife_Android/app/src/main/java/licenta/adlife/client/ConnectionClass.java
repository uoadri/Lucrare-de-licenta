package licenta.adlife.client;

import android.annotation.SuppressLint;
import android.os.StrictMode;
import android.util.Log;
import java.sql.SQLException;
import java.sql.Connection;
import java.sql.DriverManager;


public class ConnectionClass {
    String ip = "SQL6004.site4now.net";
    String classs = "net.sourceforge.jtds.jdbc.Driver";
    String db = "DB_A495DC_licenta";
    String un = "DB_A495DC_licenta_admin";
    String password = "licenta1234";

    @SuppressLint("NewApi")
    public Connection CONN() {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder()
                .permitAll().build();
        StrictMode.setThreadPolicy(policy);
        Connection conn = null;
        String ConnURL = null;
        try {

            Class.forName(classs).newInstance();
            ConnURL = "jdbc:jtds:sqlserver://" + ip + ":1433;"
                    + "databaseName=" + db + ";user=" + un + ";password="
                    + password + ";";
            conn = DriverManager.getConnection(ConnURL);
        } catch (SQLException se) {
            Log.e("ERRO1", se.getMessage());
        } catch (ClassNotFoundException e) {
            Log.e("ERRO2", e.getMessage());
        } catch (Exception e) {
            Log.e("ERRO3", e.getMessage());
        }
        return conn;
    }
}

