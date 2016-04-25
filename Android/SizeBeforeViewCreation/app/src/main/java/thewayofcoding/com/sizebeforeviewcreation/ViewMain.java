package thewayofcoding.com.sizebeforeviewcreation;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.Point;
import android.graphics.Rect;
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
    Paint textPaint = new Paint();
    Rect messageBounds = new Rect();
    Path lineText = new Path();

    String viewWidthMessage = "";
    String viewHeightMessage = "";

    public ViewMain(Context parentActivityContext) {
        super(parentActivityContext);
    }

    /***
     * this function is called by the parent activity once screen bounds are calculated
     * this is where all code to initialize something like a game would go
     */
    @SuppressWarnings("deprecation")
    @SuppressLint("NewApi")
    public void initialize(Point screenBounds) {
        viewWidthMessage = "View Width: " + String.valueOf(screenBounds.x);
        viewHeightMessage = "View Height: " + String.valueOf(screenBounds.y);

        //make a paint instance that will be used when drawing text
        textPaint.setColor(Color.GREEN);
        textPaint.setAntiAlias(true);
        textPaint.setSubpixelText(true);
        textPaint.setTextSize((float) screenBounds.x / 30f);
    }


    @Override
    public void draw(Canvas canvas) {
        super.draw(canvas);

        //fill the entire view with black as a base color
        canvas.drawColor(Color.BLACK);

        //draw text on the view

        //get the size of the first line of text we will display on screen
        textPaint.getTextBounds(viewWidthMessage, 0, viewWidthMessage.length(), messageBounds);

        int xPosition = 1;
        int yPosition = 1;

        int currentLineHeight = yPosition + messageBounds.height() * 2;

        lineText.moveTo(xPosition, currentLineHeight);
        lineText.lineTo(xPosition + messageBounds.width(), currentLineHeight);

        canvas.drawTextOnPath(viewWidthMessage, lineText, 0, 0, textPaint);

        lineText.reset();

        currentLineHeight += messageBounds.height() * 2;

        lineText.moveTo(xPosition, currentLineHeight);

        textPaint.getTextBounds(viewHeightMessage, 0, viewHeightMessage.length(), messageBounds);

        lineText.lineTo(xPosition + messageBounds.width(), currentLineHeight);

        canvas.drawTextOnPath(viewHeightMessage, lineText, 0, 0, textPaint);
    }
}
