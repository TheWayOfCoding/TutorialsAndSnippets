package thewayofcoding.com.simplegamedrawloop;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.graphics.Point;
import android.os.Build;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.ViewTreeObserver;
import android.widget.LinearLayout;

/*
By: Scott Waldron
TheWayOfCoding.com

- All resources retain copyright (bitmaps, etc).
- You are not allowed to use these projects 
as tutorials or samples distributed with your own branding.
Basically, any use with the same theme as they were 
intended for with TheWayOfCoding.com is not allowed.

Source code License (excluding the above restrictions):
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
public class ActivityMain extends Activity {
    LinearLayout mLinearLayout;
    ViewMain mainView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //set the main layout to our xml file with empty layouts
        setContentView(R.layout.activity_activity_main);

        //get a reference to the interior layout we want to draw on
        mLinearLayout = (LinearLayout)findViewById(R.id.layoutlinearmain);

        //instantiate our custom view
        mainView = new ViewMain(this, this);

        //clean up the layout if needed and push our custom view into it
        mLinearLayout.removeAllViews();
        mLinearLayout.addView(mainView);

        //once the view is ready the layout listener will be called
        mLinearLayout.getViewTreeObserver().addOnGlobalLayoutListener(mainViewLayoutListener);
    }

    /***
     * this is used so we can get screen size before everything is finished loading
     * and visible on the screen
     */
    ViewTreeObserver.OnGlobalLayoutListener mainViewLayoutListener
            = new ViewTreeObserver.OnGlobalLayoutListener() {

        @SuppressWarnings("deprecation")
        @SuppressLint("NewApi")
        @Override
        public void onGlobalLayout() {
            ViewTreeObserver obs = mLinearLayout.getViewTreeObserver();

            //deal with the view tree differently based on Android SDK version
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN) {
                obs.removeOnGlobalLayoutListener(this);
            } else {
                obs.removeGlobalOnLayoutListener(this);
            }

            //save what will be the full screen size
            //finish the view startup process now that we know the size
            mainView.initialize(new Point(mLinearLayout.getWidth(), mLinearLayout.getHeight()));
        }
    };
}
