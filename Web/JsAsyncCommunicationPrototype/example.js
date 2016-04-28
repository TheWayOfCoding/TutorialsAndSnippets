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

//a function to test async capability of our AsyncManager prototype
function getAsyncTimestamp() {
	//define our request to the server script
    var postDataArray = new Array(2);
    postDataArray[0] = 'action';
    postDataArray[1] = 'get-timestamp';

    //get the needed information
    new AsyncManager('statusdiv', 
		getAsyncTimestamp_callback, 
		'asyncreceiver.php', 
		postDataArray); 
}
function getAsyncTimestamp_callback(xmlDataReturned) {
	//called once getAsyncTimestamp() gets a response back from the script
	//extract the expected response tag
	if(xmlDataReturned != null) {
		var returnedValue = xmlDataReturned.getElementsByTagName('responsedata')[0].childNodes[0].nodeValue;
		if(returnedValue != null) {
		  document.getElementById('divtimestampresult').innerHTML = returnedValue;
		}
	} else {
		document.getElementById('statusdiv').innerHTML = "There was an issue with the response.";
	}
}

//a function to test async capability of our AsyncManager prototype
function getAsyncRandomNumber() {
	//define our request to the server script
	//this time we have two more parameters
    var postDataArray = new Array(6);
    postDataArray[0] = 'action';
    postDataArray[1] = 'get-random-int';
	postDataArray[2] = 'range-bottom';
    postDataArray[3] = '10';
	postDataArray[4] = 'range-top';
    postDataArray[5] = '40';

    //get the needed information
    new AsyncManager('statusdiv', 
		getAsyncRandomNumber_callback, 
		'asyncreceiver.php', 
		postDataArray); 
}
function getAsyncRandomNumber_callback(xmlDataReturned)
{
	//called once getAsyncRandomNumber() gets a response back from the script
	//extract the expected response tag
	if(xmlDataReturned != null) {
		var returnedValue = xmlDataReturned.getElementsByTagName('responsedata')[0].childNodes[0].nodeValue;
		if(returnedValue != null) {
		  document.getElementById('divrandomnumberresult').innerHTML = returnedValue;
		} else {
			document.getElementById('statusdiv').innerHTML = "There was an issue with the response.";
		}
	}
}