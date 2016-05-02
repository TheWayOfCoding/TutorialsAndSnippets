package thewayofcoding.com.countdowntimerexample;

import android.app.Activity;
import android.content.res.Resources;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

public class MainActivity extends Activity {
    Spinner timerValueSpinner;
    Button startTimerButton;
    Button stopTimerButton;
    TextView statusTextView;
    SimpleCountdownTimer timer;
    String[] timeValues;
    Resources resourcePointer;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        timerValueSpinner = (Spinner)this.findViewById(R.id.secondsSpinner);
        statusTextView = (TextView)this.findViewById(R.id.timerView);

        resourcePointer = getResources();
        timeValues = resourcePointer.getStringArray(R.array.seconds_list);

        //the button that will start the timer
        startTimerButton = (Button)this.findViewById(R.id.startButton);
        startTimerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(timerValueSpinner.getSelectedItemPosition() > -1) {
                    int parsedSpinnerValue = 0;
                    parsedSpinnerValue = Integer.parseInt(
                            timeValues[timerValueSpinner.getSelectedItemPosition()]);

                    if (parsedSpinnerValue > 0) {
                        //if there is a timer already, end that before making a new one
                        if(timer != null) {
                            timer.cancel();
                        }

                        //initialize a new timer instance
                        timer = new SimpleCountdownTimer(parsedSpinnerValue
                                * SimpleCountdownTimer.oneSecond,
                                SimpleCountdownTimer.oneSecond,
                                statusTextView);

                        timer.start();
                    }
                }
            }
        });

        //the button that will stop the timer
        stopTimerButton = (Button)this.findViewById(R.id.stopButton);
        stopTimerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(timer != null) {
                    timer.cancel();
                }
            }
        });
    }
}
