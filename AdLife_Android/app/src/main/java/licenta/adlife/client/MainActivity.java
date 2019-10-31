package licenta.adlife.client;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;


public class MainActivity extends AppCompatActivity {

    ConnectionClass connectionClass;
    EditText edtuserid,edtpass;
    Button btnlogin;
    ProgressBar pbbar;
    SharedPreferences sharedUserPreferences;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        connectionClass = new ConnectionClass();
        edtuserid = (EditText) findViewById(R.id.edtuserid);
        edtpass = (EditText) findViewById(R.id.edtpass);
        btnlogin = (Button) findViewById(R.id.btnlogin);
        pbbar = (ProgressBar) findViewById(R.id.pbbar);
        pbbar.setVisibility(View.GONE);
        sharedUserPreferences = getSharedPreferences("userFile",MODE_PRIVATE);
        autoComplete();
       /* btnlogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                DoLogin doLogin = new DoLogin();
                doLogin.execute("");

            }
        });*/
        btnlogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(MainActivity.this, AsigurareActivity.class);
                startActivity(i);
            }
        });

    }

    public void autoComplete(){
       edtuserid.setText(sharedUserPreferences.getString("user",""));
       edtpass.setText(sharedUserPreferences.getString("pass",""));
    }

    public class DoLogin extends AsyncTask<String,String,String>
    {
        String z = "";
        Boolean isSuccess = false;


        String userid = edtuserid.getText().toString();
        String password = edtpass.getText().toString();


        @Override
        protected void onPreExecute() {
            pbbar.setVisibility(View.VISIBLE);
        }

        @Override
        protected void onPostExecute(String r) {
            pbbar.setVisibility(View.GONE);
            Toast.makeText(MainActivity.this,r,Toast.LENGTH_SHORT).show();

            if(isSuccess) {
                Intent i = new Intent(MainActivity.this, AsigurareActivity.class);
                startActivity(i);
                finish();
            }

        }


        @Override
        protected String doInBackground(String... params) {
            if(userid.trim().equals("")|| password.trim().equals(""))
                z = "Please enter User Id and Password";
            else
            {
                ResultSet rs = null;
                try {

                    Connection con = connectionClass.CONN();
                    if (con == null) {
                        z = "Error in connection with SQL server";
                    } else {
                        String query = "select id_client, utilizator, parola from client where utilizator='" + userid + "' and parola='" + password + "'";
                        Statement stmt = con.createStatement();
                         rs = stmt.executeQuery(query);
                       // Toast.makeText(MainActivity.this,rs.toString(),Toast.LENGTH_SHORT).show();
                        if(rs.next())
                        {
                            Client.id = Integer.parseInt(rs.getString(1));
                            z = "Login successfull";
                            isSuccess=true;
                            SharedPreferences.Editor editor = sharedUserPreferences.edit();
                            editor.putString("user",userid);
                            editor.commit();
                            editor.putString("pass",password);
                            editor.commit();
                        }
                        else
                        {
                            z = "Invalid Credentials";
                            isSuccess = false;
                        }

                    }
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    z = "Exceptie";
                }
            }
            return z;
        }
    }
}