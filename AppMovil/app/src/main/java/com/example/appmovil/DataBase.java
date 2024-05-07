package com.example.appmovil;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import androidx.annotation.Nullable;

import java.util.ArrayList;
import java.util.List;

public class DataBase extends SQLiteOpenHelper {


    public static final String PROF_TABLE = "PROF_TABLE";
    public static final String COLUMN_PROF_NAME = "PROF_NAME";
    public static final String COLUMN_PROF_AGE = "PROF_AGE";
    public static final String COLUMN_ID = "ID";
    public static final String COLUMN_ACTIVE_PROF = "ACTIVE_PROF";

    public DataBase(@Nullable Context context) {
        super(context, "prof.db", null, 1);
    }

    @Override
    public void onCreate(SQLiteDatabase sqLiteDatabase) {
        String crateTable = "CREATE TABLE " + PROF_TABLE + " (" + COLUMN_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " + COLUMN_PROF_NAME + " TEXT, " + COLUMN_PROF_AGE + " INT, " + COLUMN_ACTIVE_PROF + " BOOL)";

        sqLiteDatabase.execSQL(crateTable);

    }

    @Override
    public void onUpgrade(SQLiteDatabase sqLiteDatabase, int i, int i1) {

    }

    public boolean addOne(Prof_view prof_view){
        SQLiteDatabase sqLiteDatabase = this.getWritableDatabase();
        ContentValues cv = new ContentValues();
        cv.put(COLUMN_PROF_NAME, prof_view.getName());
        cv.put(COLUMN_PROF_AGE, prof_view.getName());
        cv.put(COLUMN_ACTIVE_PROF, prof_view.getName());
        long insert = sqLiteDatabase.insert(PROF_TABLE, null, cv);
        if (insert == -1){
            return false;
        }else{
            return true;
        }
    }

    public List<Prof_view> getEverybody(){
        List<Prof_view> returnList = new ArrayList<>();

        String queryString = "SELECT * FROM " + PROF_TABLE;

        SQLiteDatabase db = this.getReadableDatabase();
        Cursor cursor = db.rawQuery(queryString, null);
        if (cursor.moveToFirst()){
            do {
                int profID = cursor.getInt(0);
                String profName = cursor.getString(1);
                int profAge = cursor.getInt(2);
                boolean profActive = cursor.getInt(3) ==1 ? true:false;

                Prof_view new_prof_view = new Prof_view(profID,profName,profAge,profActive);

                returnList.add(new_prof_view);

            } while (cursor.moveToNext());
        }
        else{


        }
        cursor.close();
        db.close();

        return returnList;

    }
}
