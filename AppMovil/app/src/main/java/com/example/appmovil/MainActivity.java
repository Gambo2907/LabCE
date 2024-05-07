package com.example.appmovil;

import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Switch;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.List;

public class MainActivity extends AppCompatActivity {

    Button View_All_Bt, Add_btn;
    EditText Prof_Name,Prof_Age;
    Switch switch1;
    ListView ListView_Prof;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        View_All_Bt = findViewById(R.id.View_All_Bt);
        Add_btn = findViewById(R.id.Add_btn);
        Prof_Name = findViewById(R.id.Prof_correo);
        Prof_Age = findViewById(R.id.Prof_Age);
        switch1 = findViewById(R.id.switch1);
        ListView_Prof = findViewById(R.id.ListView_Prof);

        View_All_Bt.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                DataBase dataBase = new DataBase(MainActivity.this);
                List<Prof_view> everyone = dataBase.getEverybody();
                //Toast.makeText(MainActivity.this, everyone.toString(), Toast.LENGTH_LONG).show();
                ArrayAdapter profArrayAdapter = new ArrayAdapter<Prof_view>(MainActivity.this, android.R.layout.simple_list_item_1, everyone);
                ListView_Prof.setAdapter(profArrayAdapter);

            }

        });

        Add_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Prof_view prof_view;
                try {
                    prof_view = new Prof_view(-1, Prof_Name.getText().toString(), Integer.parseInt(Prof_Age.getText().toString()), switch1.isChecked());
                    Toast.makeText(MainActivity.this, prof_view.toString(), Toast.LENGTH_LONG).show();
                }
                catch (Exception e){
                    prof_view = new Prof_view(-1,"Error", 0, false);
                    Toast.makeText(MainActivity.this, "Error", Toast.LENGTH_LONG).show();
                }


                DataBase dataBase = new DataBase(MainActivity.this);
                boolean success = dataBase.addOne(prof_view);
                Toast.makeText(MainActivity.this, "Sucess= " + success,Toast.LENGTH_SHORT).show();




            }
        });

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });




    }
}