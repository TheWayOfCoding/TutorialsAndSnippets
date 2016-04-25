package thewayofcoding.com.simplegamedrawloop;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Point;
import android.graphics.Rect;
import android.os.SystemClock;
import android.view.View;

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
public class ViewMain extends View {
    Activity parentActivity = null;

    //monitors game time per round
    long currentDrawRefreshInterval = 50;
    long lastFrameTimeInMilliseconds = 0;

    Point screenBounds = null;

    Bitmap cloudsBitmap = null;
    Rect cloudSourceDimensions = new Rect();
    Rect cloudDestinationDimensions = new Rect();

    Bitmap jetBitmap = null;
    Rect jetSourceDimensions = new Rect();
    Rect jetDestinationDimensions = new Rect();
    int jetScaledDestinationWidth = 0;

    int jetScaledMovementSpeed = 0;

    public ViewMain(Context parentActivityContext) {
        super(parentActivityContext);
    }

    public ViewMain(Context parentActivityContext, Activity parentActivity) {
        super(parentActivityContext);
        this.parentActivity = parentActivity;
    }

    /***
     * this function is called by the parent activity once screen bounds are calculated
     * this is where all code to initialize something like a game would go
     */
    public void initialize(Point screenBounds) {
        this.screenBounds = screenBounds;

        //load the cloud bitmap so we can display it on screen (drawable-nodpi)
        cloudsBitmap = BitmapFactory.decodeResource(
                parentActivity.getResources(),
                R.drawable.clouds);

        //save the width and height of the bitmap because we need that
        //as a rect every it will be drawn to the screen
        cloudSourceDimensions.right = cloudsBitmap.getWidth();
        cloudSourceDimensions.bottom = cloudsBitmap.getHeight();

        //have out bitmap scaled to the entire screen when drawn
        cloudDestinationDimensions.right = screenBounds.x;
        cloudDestinationDimensions.bottom = screenBounds.y;

        //load the jet bitmap so we can display it on screen (drawable-nodpi)
        jetBitmap = BitmapFactory.decodeResource(
                parentActivity.getResources(),
                R.drawable.jet);

        jetSourceDimensions.right = jetBitmap.getWidth();
        jetSourceDimensions.bottom = jetBitmap.getHeight();

        //we want a full sized jet graphic to take up 1/5th of the screen when fully visible
        jetScaledDestinationWidth = screenBounds.x / 5;

        //get the scaled height by taking the original image ratio and multiplying it
        //by the new scaled image width we just calculated
        int jetScaledDestinationHeight = 0;
        jetScaledDestinationHeight = (int)((float)jetScaledDestinationWidth
                * ((float)jetSourceDimensions.height() / (float)jetSourceDimensions.width()));

        //set a starter position of the jet off-screen to the right
        jetDestinationDimensions.left = screenBounds.x;
        jetDestinationDimensions.right = jetDestinationDimensions.left + jetScaledDestinationWidth;

        //have the jet fly in the middle of the screen
        //so you get the screens midpoint minus half of the scaled jet's height
        jetDestinationDimensions.top = (int)(screenBounds.y / 2d - jetScaledDestinationHeight / 2d);
        jetDestinationDimensions.bottom = jetDestinationDimensions.top + jetScaledDestinationHeight;

        //have the jet move 1/50th of the screen every update tick
        jetScaledMovementSpeed = screenBounds.x / 50;
        if(jetScaledMovementSpeed <= 0) {
            jetScaledMovementSpeed = 1;
        }
    }

    /***
     * the function where you draw to this view
     * this is called automatically as well as when you call invalidate
     * in our case we call invalidate at specific intervals
     *
     * @param canvas the object that you use to draw with
     */
    @Override
    public void draw(Canvas canvas) {
        super.draw(canvas);

        //get the current system up-time used in getting an average
        //refresh rate and counting down on game time
        long currentMilliseconds = SystemClock.uptimeMillis();

        //see what the difference is between the last update and right now
        long pastCurrentCanvasDrawDifference = currentMilliseconds - lastFrameTimeInMilliseconds;

        //get ready for the next screen update
        lastFrameTimeInMilliseconds = currentMilliseconds;

        //-----------------------------------------------------------------------------------------

        //clear the screen
        canvas.drawColor(Color.BLACK);

        //draw our clouds graphic, filling the entire view
        canvas.drawBitmap(cloudsBitmap, cloudSourceDimensions, cloudDestinationDimensions, null);

        //have the jet bitmap move across the screen
        jetDestinationDimensions.left -= jetScaledMovementSpeed;
        jetDestinationDimensions.right -= jetScaledMovementSpeed;
        canvas.drawBitmap(jetBitmap, jetSourceDimensions, jetDestinationDimensions, null);

        //see if the jet is fully off-screen on the left
        if(jetDestinationDimensions.left < -jetScaledDestinationWidth) {
            //reset the jet's position to be off-screen on the right
            jetDestinationDimensions.left = screenBounds.x;
            jetDestinationDimensions.right = jetDestinationDimensions.left
                    + jetScaledDestinationWidth;
        }

        //-----------------------------------------------------------------------------------------

        //see if this view is visible to the user
        if(this.getVisibility() == View.VISIBLE) {
            //see if we need to wait to update, or just initiate it now for processing
            if(pastCurrentCanvasDrawDifference == 0) {
                //refresh the screen on a fixed interval
                postDelayed (
                        new Runnable() {
                            @Override public void run() {
                                invalidate();
                            }
                        }, currentDrawRefreshInterval
                );
            } else {
                //see how long we have to wait to refresh
                if(pastCurrentCanvasDrawDifference < currentDrawRefreshInterval) {
                    postDelayed (
                            new Runnable() {
                                @Override public void run() {
                                    invalidate();
                                }
                            }, currentDrawRefreshInterval - pastCurrentCanvasDrawDifference
                    );
                } else {
                    //we don't need to wait, so refresh now
                    post (
                        new Runnable() {
                            @Override public void run() {
                                invalidate();
                            }
                        }
                    );
                }
            }
        }
    }
}