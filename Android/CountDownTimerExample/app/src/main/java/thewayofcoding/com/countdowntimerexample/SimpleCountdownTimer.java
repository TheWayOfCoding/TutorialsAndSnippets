package thewayofcoding.com.countdowntimerexample;

import android.os.CountDownTimer;
import android.widget.TextView;

public class SimpleCountdownTimer extends CountDownTimer {
    public static int oneSecond = 1000;
    TextView textViewTimeLeftDisplay;

    public SimpleCountdownTimer(long millisInFuture, long countDownInterval,
                                TextView textViewTimeLeftDisplay) {

        super(millisInFuture, countDownInterval);

        this.textViewTimeLeftDisplay = textViewTimeLeftDisplay;
    }

    @Override
    public void onFinish() {
        textViewTimeLeftDisplay.setText("Finished");
    }

    @Override
    public void onTick(long millisUntilFinished) {
        textViewTimeLeftDisplay.setText(String.valueOf(millisUntilFinished / oneSecond));
    }
}
