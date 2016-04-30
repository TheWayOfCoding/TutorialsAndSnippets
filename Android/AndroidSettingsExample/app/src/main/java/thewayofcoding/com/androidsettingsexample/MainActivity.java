package thewayofcoding.com.androidsettingsexample;

import android.app.Activity;
import android.content.res.Resources;
import android.os.Vibrator;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.Spinner;

public class MainActivity extends Activity {
    Resources resourcePointer;
    Settings settings;
    String[] arrayOnOff;
    Spinner vibrationSpinner;
    Button quitButton;
    Activity thisActivity;

    Vibrator vibrator;
    boolean vibrationOn = true;

    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        thisActivity = this;

        //instantiate our custom settings object
        settings = new Settings(this);

        //initialize a variable that we can use to access project resources
        resourcePointer = getResources();

        //fetch needed array lists for the options controls
        arrayOnOff = resourcePointer.getStringArray(R.array.array_onoff);

        vibrationSpinner = (Spinner)findViewById(R.id.options_vibration_spinner);
        vibrationSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onNothingSelected(AdapterView<?> parentView) {}
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                settings.setSettingValue(Settings.KEY_STATE_VIBRATION, arrayOnOff[position]);
                vibrationOn = settings.isVibrationOn();
            }
        });

        //create a handler for the quit button
        quitButton = (Button) findViewById(R.id.main_btn_quit);
        quitButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View arg0) {
                thisActivity.finish();
            }
        });
    }

    /***
     * this happens after onCreate and after a restart
     */
    @Override
    public void onStart() {
        super.onStart();

        //attempt to select a previously saved setting if needed
        String currentVibrationSetting = settings.getSettingValue(Settings.KEY_STATE_VIBRATION);
        if(!currentVibrationSetting.trim().equals("")) {
            int spinnerPosition = getSpinnerItemPosition(vibrationSpinner, currentVibrationSetting);
            if(spinnerPosition > -1) {
                vibrationSpinner.setSelection(spinnerPosition);
            }
        }

        //initialize the vibrator service if necessary
        vibrationOn = settings.isVibrationOn();
        if(vibrationOn) {
            //use the vibration feature of the device if desired by the user
            vibrator = (Vibrator)getSystemService(VIBRATOR_SERVICE);
        }
    }

    /***
     * attempt to find an item in a spinner based on the textual value
     *
     * @param spinner control to search
     * @param stringToFind string to find
     * @return -1 or the item index
     */
    int getSpinnerItemPosition(Spinner spinner, String stringToFind) {
        int index = -1;
        for (int i = 0; i < spinner.getCount(); i++) {
            if (spinner.getItemAtPosition(i).equals(stringToFind)) {
                index = i;
                break;
            }
        }
        return index;
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN:
                if(vibrationOn && vibrator != null) {
                    vibrator.vibrate(100);
                } else if (vibrationOn) {
                    //(re)initialize the vibrator object
                    vibrator = (Vibrator)getSystemService(VIBRATOR_SERVICE);
                    vibrator.vibrate(100);
                }
                break;
        }

        return true;
    }
}
