package licenta.adlife.client;

import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Plata extends AppCompatActivity {
    Button btnPlata;
    EditText edtNrCard,edtLuna, edtAn, edtCVC;
    SharedPreferences sharedUserPreferences;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_plata);
        btnPlata = (Button) findViewById(R.id.b_efect_plata);
        edtNrCard = (EditText) findViewById(R.id.edt_nr_card);
        edtLuna = (EditText) findViewById(R.id.edt_luna);
        edtAn = (EditText) findViewById(R.id.edt_an);
        edtCVC = (EditText) findViewById(R.id.edt_cvc);
        sharedUserPreferences = getSharedPreferences("userFile",MODE_PRIVATE);
        autoComplete();
        btnPlata.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                selectareDate();
                Toast.makeText(Plata.this,"Plata efectuata cu succes!",Toast.LENGTH_SHORT).show();
                SharedPreferences.Editor editor = sharedUserPreferences.edit();
                editor.putString("nrCard",edtNrCard.getText().toString());
                editor.commit();
                editor.putString("luna",edtLuna.getText().toString());
                editor.commit();
                editor.putString("an",edtAn.getText().toString());
                editor.commit();
                editor.putString("cvc",edtCVC.getText().toString());
                editor.commit();
                onBackPressed();
            }
        });
    }

    public void autoComplete(){
        edtNrCard.setText(sharedUserPreferences.getString("nrCard",""));
        edtLuna.setText(sharedUserPreferences.getString("luna",""));
        edtAn.setText(sharedUserPreferences.getString("an",""));
        edtCVC.setText(sharedUserPreferences.getString("cvc",""));
    }

    public void selectareDate() {
        ResultSet rs = null;
        ConnectionClass connectionClass = new ConnectionClass();
        try {

            Connection con = connectionClass.CONN();

            String query = "Select COUNT(nr_plata)" +
                    " FROM  plata " +
                    " WHERE id_client = " +Client.id ;
            Statement stmt = con.createStatement();
            rs = stmt.executeQuery(query);
            int plati=0;
            while (rs.next()) {
                plati = Integer.parseInt(rs.getString(1)) + 1;

            }


            String insert = "INSERT INTO PLATA (id_plata,id_client,nr_plata,data_plata,suma_plata) values ("+ plati + Client.id +","+ Client.id +","+ plati +",'" + new SimpleDateFormat("dd.MM.yyyy").format(new Date()) + "','" + Asigurare.sumaPlata + "')";
            PreparedStatement preparedStatement = con.prepareStatement(insert);
            preparedStatement.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
