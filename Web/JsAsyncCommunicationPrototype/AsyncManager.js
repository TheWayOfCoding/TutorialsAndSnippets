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

-----------------------------------------

A prototype for AsyncManager communication.

htmlStatusElement: a html element like a div that will receive status info
onCompleteFunction: the function that will be called after a response is received
serverPostScript: the server side script file to call
sendDataPairs: an array that holds name/value pairs
*/
function AsyncManager(htmlStatusElement, onCompleteFunction, serverPostScript, sendDataPairs) {
    this.htmlStatusElement = htmlStatusElement;
	
    this.onCompleteFunction = onCompleteFunction || function () { };
    var thisInstance = this; //save a reference to this instance for callback 
    this.serverPostScript = serverPostScript;
    this.sendDataPairs = sendDataPairs; 

	/*
		this handles updating the status element
	*/
    this.statusIndicator = function(statusText) {
        //make sure an element reference was returned before trying to set properties
        if(this.htmlStatusElement != null && this.htmlStatusElement != "") {
            document.getElementById(this.htmlStatusElement).innerHTML = statusText;
        }
    }
    
	/*
		creates an instance of XMLHttpRequest independent of the browser type
	*/
    this.returnNewXMLhttpRequestObject = function()
    {
        var xhrObject = null; 
        
        //see what type of browser is being used
        if(window.XMLHttpRequest) {
            //non MS browsers
            xhrObject = new XMLHttpRequest();
			try {
				xhrObject.overrideMimeType('text/xml');
			} catch (error) { }
			
        } else if(window.ActiveXObject) {
            try {
				//some versions of IE need special access
                //first attempt to use the newer object
                xhrObject = new ActiveXObject('Msxml2.XMLHTTP');
            } catch (error) {
				//newer version isn't available, try older
                try {
                    //if the newer object doesn't exist, use the older one
                    xhrObject = new ActiveXObject('Microsoft.XMLHTTP');
                } catch (error) {
                    //this IE browser is too old to work, notify the user
					thisInstance.statusIndicator('Your version of IE is too old.');
                }
            }
        }
        
        return xhrObject;
    }
    
	/*
		send data to a server using the attached xml http request object
	*/
    this.sendData = function() {
        var constructedPostString = '';
        var sendDataLength = this.sendDataPairs.length;

        thisInstance.httpRequest = this.returnNewXMLhttpRequestObject();
        
        //make sure an instance was returned before doing anything else
        if(thisInstance.httpRequest != null) {
            //assign the event handler so we can know how return data should be processed
            //if the caller wants to use a custom event handler, let them
            thisInstance.httpRequest.onreadystatechange = function() {
                //the response from the server is complete, continue processing
                if(thisInstance.httpRequest.readyState == 4) {
                    //everything is good, the response is received
                    if (thisInstance.httpRequest.status == 200) {
                        //call the function the instance creater defined and pass the resulting xml
                        thisInstance.onCompleteFunction(thisInstance.httpRequest.responseXML.documentElement);
                        thisInstance.statusIndicator('');
						
						//the object is no longer needed, so allow it to be destroyed
                        thisInstance.httpRequest = null; 
                    } else {
                        //there was a problem with the request,
                        //for example the response may be a 404 (Not Found)
                        //or 500 (Internal Server Error) response codes
                        thisInstance.statusIndicator('There is an issue connecting to the server...');
                    }
                } else {
                    //still not ready, indicate that to the user
                    thisInstance.statusIndicator('Loading...');
                }
            }
        } else {
            this.statusIndicator('There was an issue connecting to the server...');
        }

        //make sure the connection instance exists before trying to send data
        if(thisInstance.httpRequest != null) {
            //open the connection so we can stream post data to a page
            thisInstance.httpRequest.open('POST', serverPostScript, true);
            thisInstance.httpRequest.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            
            //make sure the array holds enough values to process it
            if(sendDataLength > 1) {
                //loop through the array by two (name of post variable + related post data, ...)
                for(var i = 0; i < sendDataLength - 2; i += 2) {
                    constructedPostString += this.sendDataPairs[i] 
						+ '=' + escape(this.sendDataPairs[i + 1]) + '&';
                }
				
                constructedPostString += this.sendDataPairs[sendDataLength - 2] 
					+ '=' + escape(this.sendDataPairs[sendDataLength - 1]);
            }
            
            //send the data to the script page for processing
            thisInstance.httpRequest.send(constructedPostString);
        }
    }
    
    //now that the instance is initialized, perform the send/receieve function
    this.sendData();
}
