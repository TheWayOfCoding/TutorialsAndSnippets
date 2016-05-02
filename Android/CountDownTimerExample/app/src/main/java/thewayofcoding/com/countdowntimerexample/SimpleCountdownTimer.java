package thewayofcoding.com.countdowntimerexample;

import android.os.CountDownTimer;
import android.widget.TextView;

/*
By: Scott Waldron
TheWayOfCoding.com

- All resources retain copyright (bitmaps, etc).
- You are not allowed to use these projects 
as tutorials or samples distributed with your own branding.
Basically, any use with the same theme as they were 
intended for with TheWayOfCoding.com is not allowed.

The MIT License (MIT)

Copyright (c) 2016 

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
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
